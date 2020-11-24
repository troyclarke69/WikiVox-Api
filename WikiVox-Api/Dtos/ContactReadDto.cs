using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Dtos
{
    public class ContactReadDto
    {
        // API will expose the following fields for the Contact (read) class

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Message { get; set; }
    }
}
