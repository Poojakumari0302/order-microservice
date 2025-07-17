using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Shared.Entities;

namespace Infrastructure.Shared;

public class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : DomainEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }
}