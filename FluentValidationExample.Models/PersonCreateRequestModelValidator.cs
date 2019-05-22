using FluentValidation;

namespace FluentValidationExample.Models
{
    public class PersonCreateRequestModelValidator : AbstractValidator<PersonCreateRequestModel>
    {
        //Simple validator that checks for values in First and Lastname
        public PersonCreateRequestModelValidator()
        {
            RuleFor(r => r.Firstname).NotEmpty();
            RuleFor(r => r.Lastname).NotEmpty();
        }
    }
}
