using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.Accounts;

namespace Business.Filters
{
    public class AutoAddUserFilter(params Type[] supportedModelTypes) : IActionFilter
    {
        private readonly HashSet<Type> _supportedModelTypes = [..supportedModelTypes];

        private static readonly string[] _targetProperties =
        [
            "CurrentUser",
            "CurrentRole"
        ];

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Count == 0) return;
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                return;
            }
            
            var userClaims = GetUserClaims(context.HttpContext.User);
            if (!userClaims.IsValid())
            {
                return;
            }

            ProcessActionArguments(context.ActionArguments, userClaims);
        }

        private void ProcessActionArguments(IDictionary<string, object?> arguments, UserClaims userClaims)
        {
            foreach (var argument in arguments.Values)
            {
                var argumentType = argument.GetType();
                if (!_supportedModelTypes.Any(rootType => rootType.IsAssignableFrom(argumentType)))
                {
                    continue;
                }

                UpdateArgumentProperties(argument, argumentType, userClaims);
            }
        }

        private void UpdateArgumentProperties(object argument, Type argumentType, UserClaims userClaims)
        {
            var properties = argumentType.GetProperties()
                .Where(p => _targetProperties.Contains(p.Name))
                .ToList();

            foreach (var property in properties)
            {
                try
                {
                    SetPropertyValue(property, argument, userClaims);
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
        }

        private void SetPropertyValue(PropertyInfo property, object argument, UserClaims userClaims)
        {
            switch (property.Name)
            {
                case "CurrentUser" when property.PropertyType == typeof(string) &&
                                        !string.IsNullOrWhiteSpace(userClaims.Username):
                    property.SetValue(argument, userClaims.Username);
                    break;

                case "CurrentRole" when property.PropertyType == typeof(Role) &&
                                        !string.IsNullOrWhiteSpace(userClaims.Role):
                    property.SetValue(argument, Enum.TryParse(userClaims.Role, out Role role) ? role : Role.USER);
                    break;
            }
        }

        private static UserClaims GetUserClaims(ClaimsPrincipal user)
        {
            return new UserClaims
            {
                Username = user.Identity?.Name,
                Role = user.FindFirstValue(ClaimTypes.Role),
            };
        }
    }

    public class UserClaims
    {
        public string Username { get; set; }

        public string Role { get; set; }


        public bool IsValid() =>
            !string.IsNullOrWhiteSpace(Username);
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string FindFirstValue(this ClaimsPrincipal principal, string claimType)
        {
            return principal.Claims.FirstOrDefault(c => string.Equals(c.Type, claimType))?.Value;
        }
    }
}