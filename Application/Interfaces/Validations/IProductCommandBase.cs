using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Validations
{
    public interface IProductCommandBase
    {
        string Name { get; set; }
        string Description { get; set; }
        string ImageUrl { get; set; }
        decimal Price { get; set; }
        int Stock { get; set; }
        int CategoryId { get; set; }
    }
}
