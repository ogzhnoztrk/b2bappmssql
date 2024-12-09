namespace Core.Models.Concrete
{
    public class Result<T>
    {

        public Result()
        {

        }
        /// <summary>
        /// hepsini el ile atama yapackasan
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
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
