using Comparis.CrossCutting.Notification;
using Comparis.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System;
using System.Threading.Tasks;

namespace Comparis.Domain.Services
{
    public class LimitService : ILimitService
    {
        private readonly ILogger<BalanceService> _logger;
        private readonly ITracer _tracer;
        private readonly IMessageManager _messageManager;

        public LimitService(ILogger<BalanceService> logger,
            ITracer tracer,
            IMessageManager messageManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            _messageManager = messageManager ?? throw new ArgumentNullException(nameof(messageManager));
        }

        public async Task<bool> ValidateLimitAsync(Guid accountId)
        {
            var span = _tracer.ActiveSpan;

            Random random = new Random();

            int number = random.Next(1, 3);

            await Task.Delay(1); // Simulate processing something async

            if (number == 1)
            {
                _logger.LogInformation($"Insufficient Limit for the account {accountId}");

                span.Log($"Insufficient Limit for the account {accountId}");

                _messageManager.Add(new Notification(nameof(LimitService), $"Insufficient Limit for the account {accountId}"));

                return false;
            }

            return true; ;
        }
    }
}