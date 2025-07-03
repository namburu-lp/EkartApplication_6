using ECommerceAPP.Models;
using FluentValidation;

namespace ECommerceAPP.FluentValidation
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company Name is required.")
                .MaximumLength(20).WithMessage("Company Name should be maximum 20 characters.");

            RuleFor(x => x.ContactName)
                .MaximumLength(30).WithMessage("Contact Name should be maximum 30 characters.");

            RuleFor(x => x.ContactTitle)
                .MaximumLength(30).WithMessage("Contact Title should be maximum 30 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(60).WithMessage("Address should be maximum 60 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(15).WithMessage("City should be maximum 15 characters.");

            RuleFor(x => x.Region)
                .MaximumLength(15).When(x => !string.IsNullOrEmpty(x.Region));

            RuleFor(x => x.PostalCode)
                .MaximumLength(10).When(x => !string.IsNullOrEmpty(x.PostalCode));

            RuleFor(x => x.Country)
                .MaximumLength(15).When(x => !string.IsNullOrEmpty(x.Country));

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\d{3}-\d{3}-\d{4}$").WithMessage("Phone must be in the format XXX-XXX-XXXX.");

            RuleFor(x => x.Fax)
                .Matches(@"^\d{3}-\d{3}-\d{4}$").When(x => !string.IsNullOrEmpty(x.Fax));

            RuleFor(x => x.HomePage)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.HomePage));

            RuleFor(x => x.EmailId)
                .NotEmpty().WithMessage("Email ID is required.")
                .EmailAddress().WithMessage("Email ID is not a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
                .WithMessage("Password must be at least 8 characters long, and contain at least one uppercase letter, one lowercase letter, and one digit.");
        }
    }
}
