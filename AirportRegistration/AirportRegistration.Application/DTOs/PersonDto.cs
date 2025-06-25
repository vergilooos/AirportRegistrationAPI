using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportRegistration.Application.DTOs
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string PassportNumber { get; set; } = default!;
        public string AirportCode { get; set; } = default!;
        public string AirportName { get; set; } = default!;
    }
}
