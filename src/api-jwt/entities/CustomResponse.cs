namespace api_jwt.entities
{
    public class CustomResponse<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public CustomResponse(T data, string error=null)
        {
            this.Data = data;
            this.Error = error;
        }
    }
}
