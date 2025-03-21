﻿namespace Todo.Infrastructure.Persistence;

using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;

using Microsoft.EntityFrameworkCore;

using System.Reflection;
using System.Reflection.Emit;

using Todo.Application.Common.Interfaces;
using Todo.Domain;

public class TodoDbContext : DbContext, ITodoDbContext
{
    private readonly IDateTime _dateTime;
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public TodoDbContext(DbContextOptions<TodoDbContext> options, IDateTime dateTime) : base(options)
    {
        _dateTime = dateTime;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Version = Guid.NewGuid();
                    break;

                case EntityState.Modified:
                    entry.Entity.Version = Guid.NewGuid();
                    break;
            }
        }

        //var events = ChangeTracker.Entries<IHasDomainEvent>()
        //        .Select(x => x.Entity.DomainEvents)
        //        .SelectMany(x => x)
        //        .Where(domainEvent => !domainEvent.IsPublished)
        //        .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        //await DispatchEvents(events);

        return result;
    }

    //private async Task DispatchEvents(DomainEvent[] events)
    //{
    //    foreach (var @event in events)
    //    {
    //        @event.IsPublished = true;
    //        await _domainEventService.Publish(@event);
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
        builder.AddOutboxMessageEntity();
        builder.Entity<OutboxState>()
           .HasKey(o => o.OutboxId);

        builder.Entity<OutboxMessage>()
            .HasKey(o => o.MessageId);
    }
}