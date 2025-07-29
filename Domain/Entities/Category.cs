﻿using Domain.Entities.Base;
using Domain.Entities.Products;

namespace Domain.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        
        public ICollection<Category>SubCategories { get; set; }=new List<Category>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
