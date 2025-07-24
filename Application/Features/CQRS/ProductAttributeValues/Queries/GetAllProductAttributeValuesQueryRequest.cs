using Application.Common.Models;
using Application.Features.DTOs.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.ProductAttributeValues.Queries
{
    public class GetAllProductAttributeValuesQueryRequest : IRequest<ResponseModel<List<ProductAttributeValueDto>>>
    {
    }
}
