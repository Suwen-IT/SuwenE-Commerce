﻿using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product:BaseEntity
    {
        public  string Name { get; set; }
        public  string Description { get; set; }
        public  string ImageUrl { get; set; }
        public  decimal Price { get; set; }
        public  decimal Discount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
