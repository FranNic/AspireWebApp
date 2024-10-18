namespace Todo.Infrastructure.Services;

using System;
using Todo.Application.Common.Interfaces;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}