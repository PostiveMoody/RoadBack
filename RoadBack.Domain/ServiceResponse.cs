namespace RoadBack.Domain
{
    public class ServiceResponse
    {
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }

        public static ServiceResponse Failed(string errorMessage)
        {
            return new ServiceResponse
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        public static ServiceResponse Succeeded() 
        {
            return new ServiceResponse
            {
                IsSuccess = true
            };
        }
    }
}
