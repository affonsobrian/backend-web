namespace Backend_Web.Utils
{
    public class BaseResponse<T>
    {
        public Status Status { get; set; }
        public T Content { get; set; }
        public string Message { get; set; }
    }

    public enum Status
    {
        OK,
        NOTFOUND,
        ERROR
    }
}