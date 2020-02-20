

using FluentValidation;
using KBCFoodAndGo.Shared.Models;

namespace KBCFoodAndGo.Validators
{

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("Vul je voornaam in!");
            RuleFor(user => user.LastName).NotEmpty().WithMessage("Vul je achternaam in!");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Vul een wachtwoord in!");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Vul een wachtwoord in!");
            RuleFor(user => user.Base64Image).NotEmpty().WithMessage("Voeg een afbeelding toe!");
        }
    }
}
