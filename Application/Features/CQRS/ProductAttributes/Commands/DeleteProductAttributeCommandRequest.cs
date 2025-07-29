using Application.Common.Models;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Commands
{
    public class DeleteProductAttributeCommandRequest:IRequest<ResponseModel<NoContent>>
    {
        public int Id { get; set; }
    }
}
