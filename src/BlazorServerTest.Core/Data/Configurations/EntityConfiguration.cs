using BlazorServerTest.Core.Data.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorServerTest.Core.Data.Configurations;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual bool SetDefaultConfigurations => true;

    public void Configure(EntityTypeBuilder<T> builder)
    {
        if (SetDefaultConfigurations)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Created).IsRequired();
        }

        AppendConfiguration(builder);
    }

    public abstract void AppendConfiguration(EntityTypeBuilder<T> builder);
}
