namespace storehouse_backend.Models
{
    public class ResponseObject
    {
        public ResponseObject(bool IsSuccess, string text, Exception? errorMessage = null)
        {
            Succees = IsSuccess;
            message = text;
            this.errorMessage = errorMessage;

        }
        public bool Succees { get; set; }
        public string message { get; set; }
        public Exception? errorMessage { get; set; }
    }
}
