
using ECommerceAPP.Models;
using FluentValidation;

namespace ECommerceAPP.FluentValidation
{
    public class ShipperValidator : AbstractValidator<Shipper>
    {
        public ShipperValidator()
        {

            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Please Enter CompanyName");

            RuleFor(x => x.Phone)

                   .NotEmpty()

                   .WithMessage("Phone is required.")



                   .Length(10)

                   .WithMessage("Phone number is not valid.");



        }
    }
}
