using System.ComponentModel.DataAnnotations;

namespace CodePulse.API.Models.DTO
{
    public class RegisterRequestDTO
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
