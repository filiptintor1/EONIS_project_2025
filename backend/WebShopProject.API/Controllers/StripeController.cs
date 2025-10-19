using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShopProject.Application.OrderItems.Queries.GetOrderItemsByOrderId;
using WebShopProject.Application.Orders.Commands.UpdateOrderCommand;
using WebShopProject.Application.Orders.Queries.GetOrderById;
using WebShopProject.Application.Products.Queries.GetProductById;

namespace WebShopProject.API.Controllers
{
    [ApiController]
    [Route("/stripe-session")]
    public class StripeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly string whSecret;
        private readonly IMediator mediator;


        public StripeController(
            IConfiguration config, IMediator mediator)
        {
            this.mediator = mediator;
            whSecret = config.GetSection("StripeSettings:WhSecret").Value;
        }

        [HttpPost("{orderId}")]
        public async Task<ActionResult<string>> Create(Guid orderId)
        {
            var domain = "http://localhost:3000";

            var order = await mediator.Send(new GetOrderByIdQuery(orderId));
            Console.WriteLine(order);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), orderId.ToString());
            }

            var lineItems = new List<SessionLineItemOptions>();
            var ois = await mediator.Send(new GetOrderItemsByOrderIdQuery(orderId));

            foreach (var orderItem in ois)
            {
                var prod = await mediator.Send(new GetProductByIdQuery(orderItem.ProductId));

                lineItems.Add(new SessionLineItemOptions
                {                                  

                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "rsd",
                        UnitAmount = (long)(prod.Price * 100), 
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = prod.Name,
                        },
                    },
                    Quantity = orderItem.Quantity,
                });
            }

            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:3000/checkout/success",
                CancelUrl = "http://localhost:3000/checkout/fail",
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        { "OrderId", orderId.ToString() }
                    }
                }
            };

            var service = new SessionService();
            Session session = service.Create(options);

            var response = session.Id;

            return Ok(new { sessionId = response });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            string endpointSecret = whSecret;
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("A successful payment for {0} was made.", paymentIntent.Amount);

                    var orderId = paymentIntent.Metadata["OrderId"]; 
                    if (Guid.TryParse(orderId, out Guid parsedOrderId))
                    {
                        var updateOrderCommand = new UpdateOrderCommand
                        {
                            OrderId = parsedOrderId,
                            IsPaid = true
                        };

                        await mediator.Send(updateOrderCommand);
                    }
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    Console.WriteLine("PAYMENT INTENT WIThhhhH ID: {0} CANCELED.");
                }
                else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("PAYMENT INTENT WITH ID: {0} failed.", paymentIntent.Id);
                }
                else if (stripeEvent.Type == Events.PaymentIntentCanceled)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("PAYMENT INTENT WITH ID: {0} CANCELED.", paymentIntent.Id);
                }
                else
                {
                    Console.WriteLine("UNHANDELED!! event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

    }
}


