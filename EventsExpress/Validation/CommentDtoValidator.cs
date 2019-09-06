using EventsExpress.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Validation
{
    public class CommentDtoValidator : AbstractValidator<CommentDto>
    {
        public CommentDtoValidator() {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required!");
            RuleFor(x => x.UserId).NotNull().WithMessage("Error userId is null!");
            RuleFor(x => x.EventId).NotNull().WithMessage("Error eventId is null!");
           
        }
    }
}
