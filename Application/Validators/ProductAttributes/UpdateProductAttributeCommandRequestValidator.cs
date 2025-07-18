using Application.Features.CQRS.ProductAttributes.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.ProductAttributes
{
    public class UpdateProductAttributeCommandRequestValidator:ProductAttributeBaseValidator<UpdateProductAttributeCommandRequest>
    {
        public UpdateProductAttributeCommandRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Ürün niteliği ID'si geçerli olmalıdır.");
        }
    }
}
