
using ECommerceAPP.Models;
using FluentValidation;

namespace ECommerce_Project.Validation
{
    public class EmployeeValidators : AbstractValidator<Employee>
    {
        public EmployeeValidators()
        {
            RuleFor(x => x.LastName)
   .NotEmpty()
   .WithMessage("Last Name is required.")
   .MaximumLength(20)
   .WithMessage("Last Name should be maximum 20 characters");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required.")
                .MaximumLength(20)
                .WithMessage("First Name should be maximum 10 characters");

            RuleFor(x => x.Title)
                .MaximumLength(30)
                .WithMessage("Title should be maximum 30 characters");

            RuleFor(x => x.TitleOfCourtesy)
                .MaximumLength(25)
                .WithMessage("Title of Courtesy should be maximum 25 characters");

            /*RuleFor(x => x.BirthDate.ToString()).NotEmpty()
            .WithMessage("Please enter orderDate")
            .Matches(@"^\d{4}-\d{2}-\d{2}$")
             .WithMessage("Date field must be in the format YYYY-MM-DD.");

            RuleFor(x => x.HireDate.ToString()).NotEmpty()
           .WithMessage("Please enter orderDate")
           .Matches(@"^\d{4}-\d{2}-\d{2}$")
            .WithMessage("Date field must be in the format YYYY-MM-DD.");*/


            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .MaximumLength(60)
                .WithMessage("Address should be maximum 60 characters");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required.")
                 .MaximumLength(15)
                .WithMessage("City should be maximum 15 characters");

            RuleFor(x => x.Region)
                .MaximumLength(15)
                .WithMessage("Region should be maximum 15 characters");

            RuleFor(x => x.PostalCode)
                .MaximumLength(10)
                .WithMessage("Postal Code should be maximum 10 characters");

            RuleFor(x => x.Country)
                .MaximumLength(15)
                .WithMessage("Country should be maximum 15 characters");

            //RuleFor(x => x.HomePhone)
            //    .MaximumLength(24)
            //    .WithMessage("Home Phone should be maximum 24 characters");

            //RuleFor(x => x.Extension)
            //    .MaximumLength(4)
            //    .WithMessage("Extension should be maximum 4 characters");



            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .WithMessage("Notes should be maximum 500 characters");

            /*RuleFor(x => x.ReportsTo)
                .GreaterThan(0)
                .WithMessage("Reports To must be greater than zero");*/
        }
    }
}


