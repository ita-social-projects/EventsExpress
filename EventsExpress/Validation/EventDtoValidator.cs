using EventsExpress.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Validation
{
    public class EventDtoValidator : AbstractValidator<EventDto>
    {
        public EventDtoValidator() {
           // RuleFor(x => x.Id).NotNull().WithMessage("Id can not be null!");
           //  RuleFor(x => x.Title).NotEmpty().WithMessage("Field is required!");
           //  RuleFor(x => x.Description).NotEmpty().WithMessage("Field is required!");
          //  RuleFor(x => x.DateFrom).NotEmpty().WithMessage("Field is required!");
          //  RuleFor(x => x.DateFrom).Must(ValidateDate).WithMessage("Date from must be older than date now!");
          //  RuleFor(x => x.DateTo).NotEmpty().WithMessage("Field is required!");
          //  RuleFor(x => x.User).NotNull().WithMessage("User does not exist!");
          //  RuleFor(x => x.Country).NotEmpty().WithMessage("Field is required!");
          //  RuleFor(x => x.City).NotEmpty().WithMessage("Field is required!");
        }
        private bool ValidateDate(DateTime Date)
        {
            DateTime Current = DateTime.Today;
      
            if (Date <= Current)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
