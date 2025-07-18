using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.ProductAttributeValues.Commands
{
    public class DeleteProductAttributeValueCommandRequest:IRequest<ResponseModel<NoContent>>
    {
        public int Id { get; set; }
        public DeleteProductAttributeValueCommandRequest(int id)
        {
            Id = id;
        }
    }
}
