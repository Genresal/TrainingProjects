﻿using BlazorServerTest.Core.Data.Entities.Core;

namespace BlazorServerTest.Core.Data.Entities;

public class RecipeCategory : Entity
{
    public long RecipeId { get; set; }
    public long CategoryId { get; set; }

    public Recipe? Recipe { get; set; }
    public Category? Category { get; set; }
}
