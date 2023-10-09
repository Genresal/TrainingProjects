using BlazorServerTest.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorServerTest.Core.Data.Configurations;

public class RecipeMarkConfiguration : EntityConfiguration<RecipeMark>
{
    public override void AppendConfiguration(EntityTypeBuilder<RecipeMark> builder)
    {
        builder.HasOne(x => x.Recipe)
            .WithMany(x => x.Marks)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
