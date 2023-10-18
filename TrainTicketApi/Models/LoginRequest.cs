/******************************************************************************
 * LoginRequest.cs
 * 
 * Description: This file contains the definition of the LoginRequest model class,
 * which represents the request object used for login APIs.
 * 
 * 
 *****************************************************************************/

namespace TrainTicketApi.Models
{
    public class LoginRequest
    {
        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}
