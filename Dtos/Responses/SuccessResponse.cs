namespace Dtos.Responses
{
    public sealed class SuccessResponse : Response
    {
        public new bool Success => base.Success;

        public SuccessResponse(string message = "Thành công") : base(true, message)
        {
        }
    }

    public class SuccessResponse<T> : Response<T>
    {
        public new bool Success => base.Success;

        public SuccessResponse(string message = "Thành công") : base(true, message)
        {
        }
    }
}