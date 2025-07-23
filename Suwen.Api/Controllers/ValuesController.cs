using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly IUnitOfWork unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           return Ok (await unitOfWork.GetReadRepository<Product>().GetAllAsync());
        }
          

    }
}
