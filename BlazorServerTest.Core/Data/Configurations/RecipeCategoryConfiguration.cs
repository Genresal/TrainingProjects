using BlazorServerTest.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorServerTest.Core.Data.Configurations;

public class RecipeCategoryConfiguration : EntityConfiguration<RecipeCategory>
{
    public override void AppendConfiguration(EntityTypeBuilder<RecipeCategory> builder)
    {
        builder.HasOne(x => x.Recipe)
            .WithMany(x => x.RecipeCategories)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.RecipeCategories)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
