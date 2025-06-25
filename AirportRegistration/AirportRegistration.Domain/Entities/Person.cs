using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportRegistration.Domain.Entities
{
 
    public class Person
    {
        // Unique ID
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!; // Optional
        public string Phone { get; set; } = default!; // Optional
        public string PassportNumber { get; set; } = default!;
        // Foreign key to airport
        public string AirportCode { get; set; } = default!;
        // Navigation to airport
        public Airport Airport { get; set; } = default!;
    }
}