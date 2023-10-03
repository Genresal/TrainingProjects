﻿using BlazorServerTest.Core.Data.Entities.Core;

namespace BlazorServerTest.Core.Data.Entities;
public class Category : Entity
{
    public string? Name { get; set; }
    public int? Quantity { get; set; }
    public string? Unit { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; }
}
