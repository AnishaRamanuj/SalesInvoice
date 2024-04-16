using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using SalesInvoice.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace SalesInvoice.Controllers
{
    public class SalesInvoiceController : Controller

    {
        private readonly IConfiguration _configuration;
        public SalesInvoiceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //[HttpGet]
        //public IActionResult CreateInvoice()
        //{
        //    ViewBag.Party = new List<string> { "Party 1", "Party 2", "Party 3", "Party 4", "Party 5" };
        //    ViewBag.Item = new List<string> { "Item-1", "Item-2", "Item-3", "Item-", "Item-5" };
        //    SalesInvoiceModel model = new SalesInvoiceModel();
        //    if (ViewBag.Party.Count > 0)
        //    {
        //        model.Party = ViewBag.Party[0];
        //    }
        //    if (ViewBag.Item.Count > 0)
        //    {
        //        model.Item = ViewBag.Item[0];
        //    }
        //    return View(model);
        //}
        //[HttpPost]
        //public IActionResult CreateInvoice(SalesInvoiceModel model)
        //{
        //    try
        //    {
        //        // Create connection and command objects
        //        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
        //        {
        //            using (SqlCommand command = new SqlCommand("sp_AddInvoiceDetails", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@Id", model.Id);
        //                command.Parameters.AddWithValue("@Code", model.Code);
        //                command.Parameters.AddWithValue("@Date", model.Date);
        //                command.Parameters.AddWithValue("@DueDays", model.DueDays);
        //                command.Parameters.AddWithValue("@DueDate", model.DueDate);
        //                command.Parameters.AddWithValue("@Party", model.Party);
        //                command.Parameters.AddWithValue("@Item", model.Item);
        //                command.Parameters.AddWithValue("@Qty", model.Qty);
        //                command.Parameters.AddWithValue("@Rate", model.Rate);
        //                command.Parameters.AddWithValue("@Amount", model.Amount);
        //                command.Parameters.AddWithValue("@Discount", model.Discount);
        //                command.Parameters.AddWithValue("@TotalAmount", model.TotalAmount);

        //                // Open the connection
        //                connection.Open();
        //                // Execute the stored procedure
        //                int userId = Convert.ToInt32(command.ExecuteScalar());

        //                // successful
        //                TempData["SuccessMessage"] = "Invoice created successfully.";

        //                // Optionally, you can return a JSON response
                       
        //                return RedirectToAction("InvoiceList", "SalesInvoice");

        //                //return RedirectToAction("InvoiceSuccess", new { userId = userId });
        //                //TempData["InvoiceSuccess"] = "Invoice added successfully!";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any errors
        //        ModelState.AddModelError("", "An error occurred while registering the user: " + ex.Message);
        //        return View(model);
        //    }
        //}
        public IActionResult InvoiceSuccess(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public IActionResult InvoiceList()
        {
            try
            {
                List<SalesInvoiceModel> invoiceList = new List<SalesInvoiceModel>();
                // Create connection and command objects
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
                {
                    using (SqlCommand command = new SqlCommand("sp_InvoiceList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Open the connection
                        connection.Open();

                        // Execute the stored procedure
                        SqlDataReader reader = command.ExecuteReader();

                        // Read the data and populate the userList
                        while (reader.Read())
                        {
                            SalesInvoiceModel invoice = new SalesInvoiceModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Code = reader["Code"].ToString(),
                                Date = Convert.ToDateTime(reader["Date"]),
                                DueDays = Convert.ToInt32(reader["DueDays"]),
                                DueDate = Convert.ToDateTime(reader["DueDate"]),
                                Party = reader["Party"].ToString(),
                                Item = reader["Item"].ToString(),
                                Qty = Convert.ToInt32(reader["Qty"]),
                                Rate = Convert.ToDecimal(reader["Rate"]),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                Discount = Convert.ToDouble(reader["Discount"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"])
                            };

                            invoiceList.Add(invoice);
                        }
                    }
                }
                return View(invoiceList);

            }
            catch (Exception ex)
            {
                // Handle any errors
                ModelState.AddModelError("", "An error occurred while fetching data: " + ex.Message);
                return View(new List<SalesInvoiceModel>());
            }
        }
        [HttpGet]
        public IActionResult DeleteInvoice(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
                {
                    using (SqlCommand command = new SqlCommand("sp_DeleteInvoice", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        TempData["DeleteSuccess"] = "Invoice deleted successfully.";
                    }
                }
                return RedirectToAction("InvoiceList");
            }
            catch (Exception ex)
            {
                // Handle any errors
                TempData["DeleteError"] = "Invoice not found or could not be deleted.";
                ModelState.AddModelError("", "An error occurred while deleting invoice data: " + ex.Message);
                return RedirectToAction("InvoiceList");
            }
        }
    }
}
