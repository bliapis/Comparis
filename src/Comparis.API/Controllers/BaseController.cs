using Comparis.CrossCutting.Notification;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System;
using System.Linq;

namespace Comparis.API.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        private readonly IMessageManager _messageManager;
        private readonly ITracer _tracer;

        protected BaseController(
            IMessageManager messageManager,
            ITracer tracer)
        {
            _messageManager = messageManager ?? throw new ArgumentNullException(nameof(messageManager));
            _tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
        }

        protected new IActionResult Response(object result = null)
        {
            var span = _tracer.ActiveSpan;

            if (IsValidOperation())
            {
                return Ok(
                    new
                    {
                        success = true,
                        data = result
                    });
            }

            span.Log("Error to validate response.");

            foreach(var notification in _messageManager.GetNotifications())
            {
                span.Log($"Validation error code: {notification.Key} message: {notification.Value}");
            }

            return Ok(new
            {
                success = false,
                errors = _messageManager.GetNotifications().Select(n => n.Value)
            });
        }

        protected bool IsValidOperation()
        {
            return !_messageManager.HasNotifications();
        }
    }
}