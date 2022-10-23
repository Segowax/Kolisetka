namespace Kolisetka.MVC.Services.Base
{
    public class Response
    {
        public string? Message { get; set; }
        public string? ValidationError { get; set; }
        public bool Success { get; set; }
    }
}
