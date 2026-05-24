using FluentValidation;
using Rira.Grpc;

namespace Rira.Presentation.Validators;


public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("نام الزامی است.")
            .MaximumLength(50).WithMessage("نام نمی‌تواند بیش از ۵۰ کاراکتر باشد.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("نام خانوادگی الزامی است.");

        RuleFor(x => x.NationalCode)
            .NotEmpty().WithMessage("کد ملی الزامی است.")
            .Length(10).WithMessage("کد ملی باید ۱۰ رقم باشد.")
            .Matches("^[0-9]+$").WithMessage("کد ملی فقط باید شامل اعداد باشد.");


        RuleFor(x => x.BirthDate)
            .NotNull().WithMessage("تاریخ تولد الزامی است.");
    }
}

