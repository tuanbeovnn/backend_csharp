using Dtos.Responses;
using Dtos.Tags;
using FluentValidation;

namespace Business.Validation;

public class ValidationFactory : IValidationFactory
{
    private readonly Dictionary<Type, Type> _validators = new Dictionary<Type, Type>
    {
        { typeof(UpsertTag), typeof(UpsertTagValidator) }
    };

    private IValidator<T> GetValidator<T>(T data) where T : class
    {
        if (_validators.TryGetValue(typeof(T), out Type validatorType))
        {
            return (IValidator<T>)Activator.CreateInstance(validatorType);
        }

        throw new NotImplementedException(string.Format("Validator for type {0} was not implement", nameof(T)));
    }


    public Response Validate<T>(T data) where T : class
    {
        var validator = GetValidator(data);
        var result = validator.Validate(data);
        return result.IsValid
            ? new SuccessResponse()
            : new ErrorResponse(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
    }

    public async Task<Response> ValidateAsync<T>(T data) where T : class
    {
        var validator = GetValidator(data);
        var result = await validator.ValidateAsync(data);
        return result.IsValid
            ? new SuccessResponse()
            : new ErrorResponse(string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
    }
}