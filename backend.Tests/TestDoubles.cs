using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Tests
{
    public sealed record LogEntry(LogLevel Level, EventId EventId, string Message, Exception? Exception);

    public sealed class TestLogger<T> : ILogger<T>
    {
        private sealed class NullScope : IDisposable
        {
            public static readonly NullScope Instance = new();
            public void Dispose() { }
        }

        public List<LogEntry> Entries { get; } = new();

        public IDisposable BeginScope<TState>(TState state) where TState : notnull => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);
            Entries.Add(new LogEntry(logLevel, eventId, message, exception));
        }
    }

    public sealed class InspectingEmailService : IEmailService
    {
        private readonly Func<ContactMessage, Task> _onSend;

        public bool Called { get; private set; }

        public InspectingEmailService(Func<ContactMessage, Task> onSend)
        {
            _onSend = onSend;
        }

        public async Task<bool> SendContactNotificationAsync(ContactMessage message)
        {
            Called = true;
            await _onSend(message);
            return true;
        }
    }

    public sealed class RecordingEmailService : IEmailService
    {
        private readonly bool _result;

        public List<ContactMessage> SentMessages { get; } = new();

        public RecordingEmailService(bool result = true)
        {
            _result = result;
        }

        public Task<bool> SendContactNotificationAsync(ContactMessage message)
        {
            SentMessages.Add(message);
            return Task.FromResult(_result);
        }
    }

    public sealed class ThrowingEmailService : IEmailService
    {
        private readonly Exception _exception;

        public ThrowingEmailService(Exception exception)
        {
            _exception = exception;
        }

        public Task<bool> SendContactNotificationAsync(ContactMessage message)
        {
            throw _exception;
        }
    }

    public sealed class ThrowingApplicationDbContext : ApplicationDbContext
    {
        private readonly Exception _exception;

        public ThrowingApplicationDbContext(DbContextOptions<ApplicationDbContext> options, Exception exception)
            : base(options)
        {
            _exception = exception;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw _exception;
        }
    }

    public sealed class FixedResultContactService : IContactService
    {
        private readonly Func<ContactRequestDto, Task<ServiceResult>> _handler;

        public FixedResultContactService(Func<ContactRequestDto, Task<ServiceResult>> handler)
        {
            _handler = handler;
        }

        public Task<ServiceResult> ProcessContactMessageAsync(ContactRequestDto request)
        {
            return _handler(request);
        }
    }
}
