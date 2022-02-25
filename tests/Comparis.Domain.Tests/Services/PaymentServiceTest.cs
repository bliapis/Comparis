using Comparis.CrossCutting.Notification;
using Comparis.Domain.Entities;
using Comparis.Domain.Interfaces.Services;
using Comparis.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Comparis.Domain.Tests.Services
{
    public class PaymentServiceTest
    {
        private readonly Mock<IBalanceService> _mockBalanceService = new Mock<IBalanceService>();
        private readonly Mock<ILimitService> _mockLimitService = new Mock<ILimitService>();
        private readonly Mock<ILogger<PaymentService>> _mockPaymentServiceLogger = new Mock<ILogger<PaymentService>>();

        [Fact]
        public async Task ProcessPayment_HasBallance_Success()
        {
            var payment = new Payment()
            {
                AccountFrom = Guid.NewGuid(),
                AccountTo = Guid.NewGuid(),
                Amount = 10
            };

            _mockBalanceService.Setup(b => b.ValidateBalanceAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            var paymentService = GetPaymentService();

            var result = await paymentService.ProcessAsync(payment);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ProcessPayment_HasNoBallance_HasLimit_Success()
        {
            var payment = new Payment()
            {
                AccountFrom = Guid.NewGuid(),
                AccountTo = Guid.NewGuid(),
                Amount = 10
            };

            _mockBalanceService.Setup(b => b.ValidateBalanceAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            _mockLimitService.Setup(b => b.ValidateLimitAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            var paymentService = GetPaymentService();

            var result = await paymentService.ProcessAsync(payment);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ProcessPayment_HasNoLimit_Fail()
        {
            var payment = new Payment()
            {
                AccountFrom = Guid.NewGuid(),
                AccountTo = Guid.NewGuid(),
                Amount = 10
            };

            _mockBalanceService.Setup(b => b.ValidateBalanceAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            _mockLimitService.Setup(b => b.ValidateLimitAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            var paymentService = GetPaymentService();

            var result = await paymentService.ProcessAsync(payment);

            Assert.Null(result);
        }

        private PaymentService GetPaymentService()
        {
            return new PaymentService(
                        _mockBalanceService.Object,
                        _mockLimitService.Object,
                        _mockPaymentServiceLogger.Object);
        }
    }
}
