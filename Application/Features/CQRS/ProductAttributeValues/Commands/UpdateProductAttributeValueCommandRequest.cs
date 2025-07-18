using Application.Common.Models;
using Application.Features.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.ProductAttributeValues.Commands
{
    public class UpdateProductAttributeValueCommandRequest : IRequest<ResponseModel<ProductAttributeValueDto>>
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
    }
}
