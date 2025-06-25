using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportRegistration.Application.DTOs
{
    public class PersonCreateDto
    {
        public string FirstName { get; set; } = default!;
       
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
       
        public string phone { get; set; } = default!;
        public string PassportNumber { get; set; } = default!;
        public string AirportCode { get; set; } = default!;
    }
}
