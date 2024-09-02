using FluentValidation;
using MedFutureAPI.Entities;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.NickName)
            .NotEmpty().WithMessage("NickName is required.")
            .Length(3, 32).WithMessage("NickName must be between 3 and 32 characters.");

        RuleFor(x => x.Birth)
            .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");

        RuleForEach(x => x.Stack)
           .NotEmpty().WithMessage("Each stack must have a value.")
           .Length(2, 32).WithMessage("Each stack must be between 2 and 30 characters.");
    }
}