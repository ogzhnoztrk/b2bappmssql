namespace B2BApp.Web.Services
{
    public interface IApiService<T> 
    {
        Task<T> GetAsync(string endPoint);
        Task<T> PostAsync(string endPoint, T data);
        Task<T> PutAsync(string endPoint, T data);
        Task DeleteAsync(string endPoint);
     
    }
}
