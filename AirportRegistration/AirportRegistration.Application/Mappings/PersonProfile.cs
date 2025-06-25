using AirportRegistration.Application.DTOs;
using AirportRegistration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace AirportRegistration.Application.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            // Map from entity to DTO
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.AirportName, opt => opt.MapFrom(src => src.Airport.Name));

            // Map from DTO to entity (for Create)
            CreateMap<PersonCreateDto, Person>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Airport, opt => opt.Ignore());

            // Map from DTO to entity (for Update)
            CreateMap<PersonUpdateDto, Person>()
                .ForMember(dest => dest.Airport, opt => opt.Ignore());
        }
    }
}
