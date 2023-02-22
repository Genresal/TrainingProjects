﻿using BlazorServerTest.Core.Data.Entities.Interfaces;

namespace BlazorServerTest.Core.Data.Entities;
public class Recipe : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? PrepTime { get; set; }
    public int? CookTime { get; set; }
    public int? Servings { get; set; }
    public byte[]? Image { get; set; }

    public ICollection<Ingredient> Ingredients { get; set; }
    public ICollection<Step> Steps { get; set; }
    public ICollection<Category> Categories { get; set; } = new List<Category>();


}
