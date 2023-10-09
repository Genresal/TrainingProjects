using BlazorServerTest.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorServerTest.Core.Data.Configurations;

public class RecipeIngredientConfiguration : EntityConfiguration<RecipeIngredient>
{
    public override void AppendConfiguration(EntityTypeBuilder<RecipeIngredient> builder)
    {
        builder.HasOne(x => x.Recipe)
            .WithMany(x => x.RecipeIngredients)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Ingredient)
            .WithMany(x => x.RecipeIngredients)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
