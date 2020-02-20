using FluentValidation;
using KBCFoodAndGo.Shared.Models;

namespace KBCFoodAndGo.Validators
{
    public class MealValidator: AbstractValidator<Meal>
    {
        public MealValidator()
        {
            RuleFor(meal => meal.Name).NotEmpty().WithMessage("Vul de naam van de maaltijd in!");
            RuleFor(meal => meal.Price).GreaterThan(0).WithMessage("Vul een bedrag groter dan 0 euro in!");
            RuleFor(meal => meal.ShortDescription).NotNull().WithMessage("Vul een beschrijving van de maaltijd in!");
        }
    }
}
