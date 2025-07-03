
using ECommerceAPP.DTOS;
using FluentValidation;

namespace ECommerceApp.FluentValidation
{
    public class OrderDTOValidator : AbstractValidator<OrderDto>
    {
        public OrderDTOValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
            RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("EmployeeId is required.");
            RuleFor(x => x.ShipVia).NotEmpty().WithMessage("ShipVia is required.");
            //RuleFor(x => x.ShipVia).NotEmpty().WithMessage("ShipVia is required.");
        }
    }
}
