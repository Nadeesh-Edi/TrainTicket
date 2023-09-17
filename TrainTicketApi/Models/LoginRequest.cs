// Model for the Login request object.
// The model in which Request body should come in for Login APIs

namespace TrainTicketApi.Models
{
    public class LoginRequest
    {
        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}
