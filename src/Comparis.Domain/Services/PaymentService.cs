using Comparis.Domain.Entities;
using Comparis.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Comparis.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBalanceService _balanceService;
        private readonly ILimitService _limitService;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IBalanceService balanceService,
            ILimitService limitService,
            ILogger<PaymentService> logger)
        {
            _balanceService = balanceService ?? throw new ArgumentNullException(nameof(balanceService));
            _limitService = limitService ?? throw new ArgumentNullException(nameof(limitService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Payment> ProcessAsync(Payment payment)
        {
            var hasBalance = await _balanceService.ValidateBalanceAsync(payment.AccountFrom);

            if (!hasBalance)
            {
                var hasLimit = await _limitService.ValidateLimitAsync(payment.AccountFrom);
                
                if (!hasLimit)
                {
                    _logger.LogInformation($"Payment to the account {payment.AccountTo} could not be processed, there is no limits for the account {payment.AccountFrom}");

                    return null;
                }
            }

            _logger.LogInformation($"Payment from the account {payment.AccountFrom} to the account {payment.AccountTo} has been successfully processed.");

            return payment;
        }
    }
}