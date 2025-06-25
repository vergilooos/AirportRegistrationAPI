using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportRegistration.Application.DTOs
{
    public class PersonUpdateDto : PersonCreateDto
    {
        public Guid Id { get; set; }
    }
}
