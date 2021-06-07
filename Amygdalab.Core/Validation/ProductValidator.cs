using Amygdalab.Core.Contracts.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Core.Validation
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(login => login.Name).NotNull().MinimumLength(3);
            RuleFor(login => login.Quantity).NotNull().GreaterThan(0);
            RuleFor(login => login.CostPrice).NotNull().GreaterThan(0);
            RuleFor(login => login.SellingPrice).NotNull().GreaterThan(0);
        }
    }
}
