namespace Todo.Infrastructure.Services;

using System;

using Todo.Application;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}