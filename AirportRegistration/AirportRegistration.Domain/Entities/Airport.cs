using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportRegistration.Domain.Entities
{
    public class Airport
    {
        //Airport code is unique
        public string Code { get; set; } = default!;  // MAD, BCN
        
        public string Name { get; set; } = default!;

        // Navigation to list of people
        public ICollection<Person> People { get; set; } = new List<Person>();
    }
}
