namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return statusCode switch
            {
                200 => "OK",
                400 => "You have made a bad request.",
                401 => "You are not authorized.",
                404 => "It was not resource found.",
                500 => "Internal Server Error!",
                _ => null
            };
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}