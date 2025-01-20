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


//public class ResponseModel<T> where T : class
//{
//    //all constructors are optional
//    public ResponseModel(int _code, string _message, Pagination _pagination, T _data)
//    {
//        meta.code = _code;
//        data = _data;
//        meta.pagination = _pagination;
//        meta.message = _message;
//    }

//    //without pagination
//    public ResponseModel(int _code, string _message, T _data)
//    {
//        meta.code = _code;
//        data = _data;
//        meta.pagination = null;
//        meta.message = _message;
//    }



//    public Meta meta { get; set; } = new Meta(); //cevap hakkında bilgiler
//    public T data { get; set; } //generic yapıda gelen veri

//}


//public class Meta
//{
//    public int code { get; set; } //cevabın kodu 
//    public string message { get; set; } //response kısmında gonderilecek mesaj
//    public DateTime date { get; set; } = DateTime.Now; //isteğin olşturulma zamanı
//    public Pagination pagination { get; set; } //sayfalama kullanılan endpointler için 

//}

//public class Pagination
//{
//    public Pagination()
//    {

//    }
//    public Pagination(int _current_page, int _page_size, int _total_pages, int _total_items)
//    {
//        current_page = _current_page;
//        page_size = _page_size;
//        total_pages = _total_pages;
//        total_items = _total_items;
//    }
//    public int current_page { get; set; } //şu anki sayfa
//    public int page_size { get; set; }  //her sayfada kaç eleman olacak
//    public int total_pages { get; set; } //toplam kaç sayfa olacak page size ile hesaplanır
//    public int total_items { get; set; } //toplam eleman sayısı 
//}

