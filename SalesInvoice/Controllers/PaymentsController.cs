
using System.Collections.Generic;
using System.Data;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SalesInvoice.Models;
using Stripe;
using Stripe.Checkout;

namespace SalesInvoice.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IConfiguration _configuration;

        public PaymentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult CreateCheckoutSession()
        {
            ViewBag.Party = new List<string> { "Party 1", "Party 2", "Party 3", "Party 4", "Party 5" };
            ViewBag.Item = new List<string> { "Item-1", "Item-2", "Item-3", "Item-", "Item-5" };
            SalesInvoiceModel model = new SalesInvoiceModel();
            if (ViewBag.Party.Count > 0)
            {
                model.Party = ViewBag.Party[0];
            }
            if (ViewBag.Item.Count > 0)
            {
                model.Item = ViewBag.Item[0];
            }
            return View(model);
        }
        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession(SalesInvoiceModel model)
        {
            try
            {
                // Create connection and command objects
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
                {
                    using (SqlCommand command = new SqlCommand("sp_AddInvoiceDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", model.Id);
                        command.Parameters.AddWithValue("@Code", model.Code);
                        command.Parameters.AddWithValue("@Date", model.Date);
                        command.Parameters.AddWithValue("@DueDays", model.DueDays);
                        command.Parameters.AddWithValue("@DueDate", model.DueDate);
                        command.Parameters.AddWithValue("@Party", model.Party);
                        command.Parameters.AddWithValue("@Item", model.Item);
                        command.Parameters.AddWithValue("@Qty", model.Qty);
                        command.Parameters.AddWithValue("@Rate", model.Rate);
                        command.Parameters.AddWithValue("@Amount", model.Amount);
                        command.Parameters.AddWithValue("@Discount", model.Discount);
                        command.Parameters.AddWithValue("@TotalAmount", model.TotalAmount);

                        // Open the connection
                        connection.Open();
                        // Execute the stored procedure
                        int userId = Convert.ToInt32(command.ExecuteScalar());

                        // successful
                        TempData["SuccessMessage"] = "Invoice created successfully.";

                        var options = new SessionCreateOptions
                        {
                            LineItems = new List<SessionLineItemOptions>
        {
          new SessionLineItemOptions
          {
             
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmountDecimal = model.TotalAmount,
              Currency = "inr",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = model.Code,
              },
            },
            Quantity = 1,
          },
        },
                            Mode = "payment",
                            SuccessUrl = "https://localhost:7129/SalesInvoice/InvoiceList",
                            CancelUrl = "https://localhost:7129/cancel",
                        };

                        var service = new SessionService();
                        Session session = service.Create(options);

                        Response.Headers.Add("Location", session.Url);

                        return new StatusCodeResult(303);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                ModelState.AddModelError("", "An error occurred while registering the user: " + ex.Message);
                return View(model);
            }
        }
    }
}
