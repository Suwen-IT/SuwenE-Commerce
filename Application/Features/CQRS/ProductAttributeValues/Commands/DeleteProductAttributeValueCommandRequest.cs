using Application.Common.Models;
using MediatR;


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
