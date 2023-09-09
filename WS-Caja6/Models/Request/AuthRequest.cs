using System.ComponentModel.DataAnnotations;

namespace WS_Caja6.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
