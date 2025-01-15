namespace Core.Models.Concrete
{
    public class Result<T>
    {

        public Result()
        {

        }

        public Result(int statusCode, string message, T data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; private set; } = DateTime.Now;
        public T Data { get; set; }
    }

}
