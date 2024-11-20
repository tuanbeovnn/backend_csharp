namespace Dtos.Responses
{
    public sealed class ErrorResponse : Response
    {
        public new bool Success => base.Success;

        public ErrorResponse(string message = "Thất bại") : base(false, message)
        {
        }

        public ErrorResponse(string format, params object[] arguments) : base(false, string.Format(format, arguments))
        {
        }
    }

    public sealed class ErrorResponse<T> : Response<T>
    {
        public new bool Success => base.Success;

        public ErrorResponse(string message = "Thất bại") : base(false, message)
        {
        }
    }
}