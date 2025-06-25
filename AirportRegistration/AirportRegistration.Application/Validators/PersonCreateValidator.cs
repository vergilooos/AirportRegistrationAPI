using AirportRegistration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AirportRegistration.Application.Validators
{
    // Validator for creating a new person using FluentValidation
    public class PersonCreateValidator : AbstractValidator<PersonCreateDto>
    {
        public PersonCreateValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            // Passport must start with P or L, followed by a letter and 7 digits
            RuleFor(x => x.PassportNumber)
                .NotEmpty().WithMessage("Passport number is required.")
                .Matches("^[PL][A-Z][0-9]{7}$")
                .WithMessage("Passport must start with P or L, followed by a letter and 7 digits.");

            // Email is optional but must be valid
            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));

            // Phone is optional but limited length
            RuleFor(x => x.Phone)
                .MaximumLength(20);

            // Airport code is required
            RuleFor(x => x.AirportCode)
                .NotEmpty().WithMessage("Airport selection is required.");
        }
    }
}
