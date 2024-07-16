namespace Todo.Infrastructure.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Todo.Domain;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

        //builder
        //    .OwnsOne(b => b.Colour);

        builder.Property(t => t.Version).IsConcurrencyToken();

        builder.HasMany(e => e.Items)
            .WithOne(e => e.List)
            .HasForeignKey(e => e.ListId);
    }
}