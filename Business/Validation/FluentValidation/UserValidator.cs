using Core.Entity.Concrete;
using Entity.Dtos.User;
using FluentValidation;

namespace Business.Validation.FluentValidation
{
    public class UserValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.FirstName).Length(2, 15);

            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.LastName).Length(2, 15);

            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();



        }
    }
}
