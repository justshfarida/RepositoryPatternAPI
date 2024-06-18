namespace RepositoryPatternAPI
{
    public class ResponseModel<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
