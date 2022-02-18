using AppStore.App.Core.Commands;
using FluentValidation;

namespace AppStore.App.Core.Validators
{
    public sealed class ApplicationValidation : AbstractValidator<CreateApplicationCommand>
    {
        public ApplicationValidation()
        {
            RuleFor(_ => _.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("To register an application it is necessary to enter a name");

            RuleFor(_ => _.Price)
                .GreaterThan(-1)
                .WithMessage("The price of an app must be equal to or greater than 0.");

            RuleFor(_ => _.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("To register an application it is necessary to inform a description");
        }
    }
}
