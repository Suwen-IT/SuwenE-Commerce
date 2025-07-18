using Application.Common.Models;
using Application.Features.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.ProductAttributeValues.Queries
{
    public class GetProductAttributeValueByIdQueryRequest : IRequest<ResponseModel<ProductAttributeValueDto>>
    {
        public int Id { get; set; }

        public GetProductAttributeValueByIdQueryRequest(int id)
        {
            Id = id;
        }
    }
}
