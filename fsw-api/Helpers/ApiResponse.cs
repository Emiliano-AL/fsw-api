namespace fsw_api.Helpers
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Content { get; set; }
    }
}
