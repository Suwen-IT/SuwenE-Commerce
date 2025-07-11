using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Categories.Commands
{
    public class DeleteCategoryCommandRequest:IRequest<ResponseModel<int>>
    {
        public int Id { get; set; }
        
    }
}
