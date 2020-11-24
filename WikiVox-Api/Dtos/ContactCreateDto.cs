using System.ComponentModel.DataAnnotations;

namespace Wikivox_Api.Dtos
{
    public class ContactCreateDto
    {
        // API will expose the following fields for the Contact (create) class

        //public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Company { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
