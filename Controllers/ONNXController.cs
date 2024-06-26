﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuildsByBrickwellNew.Models;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;



namespace BuildsByBrickwellNew.Controllers
{
    public class ONNXController : Controller
    {
        private readonly IntexProjectContext _context;
        private readonly InferenceSession _session;
        private readonly ILogger<ONNXController> _logger;
        public ONNXController(IntexProjectContext context, ILogger<ONNXController> logger)
        {
            _context = context;
            _logger = logger;

            try
            {
                _session = new InferenceSession("C:\\Users\\hyrum\\source\\repos\\BuildsByBrickwellNew\\fraud_detection.onnx");//In here we will need the path of the actual onnx file from google colab
                _logger.LogInformation("ONNX model loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading the ONNX model: {ex.Message}");
            }
        }
        public IActionResult ReviewOrders()
        {
            var records = _context.Orders
                .OrderByDescending(o => o.Date)
                .Where(o => o.Fraud == 1)
                .Take(20)
                .ToList();

            /*var transactionId = 1; // TransactionId of the order you want to retrieve

            var records = _context.Orders
                .Where(o => o.TransactionId == transactionId)
                .OrderByDescending(o => o.Date)
                .FirstOrDefault();*/

            var predictions = new List<OrderPrediction>();

            var class_type_dict = new Dictionary<int, string>
            {
                { 0, "Fraud"},
                { 1, "Fraud" }
            };

            foreach (var record in records)
            {
                // Attempt to parse the date if it's not null or empty aka this is hayden's doing
                DateTime parsedDate = record.Date != null ? DateTime.Parse(record.Date) : DateTime.MinValue;

                var input = new List<float>
                {
                    // Exclude TransactionId and CustomerId from the input features
                    (float)record.Time,
                    (float)(record.Amount ?? 0),

                     /*Date components as separate features*/
                    parsedDate.Year,
                    parsedDate.Month,
                    parsedDate.Day,


                    // One-hot encoding of categorical features
                    record.DayOfWeek == "Mon" ? 1:0,
                    record.DayOfWeek == "Sat" ? 1:0,
                    record.DayOfWeek == "Sun" ? 1:0,
                    record.DayOfWeek == "Thu" ? 1:0,
                    record.DayOfWeek == "Tue" ? 1:0,
                    record.DayOfWeek == "Wed" ? 1:0,

                    record.EntryMode == "Pin" ? 1:0,
                    record.EntryMode == "Tap" ? 1:0,

                    record.TypeOfTransaction == "Online" ? 1:0,
                    record.TypeOfTransaction == "POS" ? 1:0,

                    record.CountryOfTransaction == "India" ? 1:0,
                    record.CountryOfTransaction == "Russia" ? 1:0,
                    record.CountryOfTransaction == "USA" ? 1:0,
                    record.CountryOfTransaction == "UnitedKingdom" ? 1:0,

                    (record.ShippingAddress ?? record.CountryOfTransaction) == "India" ? 1:0,
                    (record.ShippingAddress ?? record.CountryOfTransaction) == "Russia" ? 1:0,
                    (record.ShippingAddress ?? record.CountryOfTransaction) == "USA" ? 1:0,
                    (record.ShippingAddress ?? record.CountryOfTransaction) == "UnitedKingdom" ? 1:0,

                    record.Bank == "HSBC"? 1:0,
                    record.Bank == "Halifax" ? 1:0,
                    record.Bank == "Lloyds" ? 1:0,
                    record.Bank == "Metro" ? 1:0,
                    record.Bank == "Monzo" ? 1:0,
                    record.Bank == "RBS" ? 1:0,

                    record.TypeOfCard == "Visa" ? 1:0
                };
                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

                var inputs = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
                };
                string predictionResult;
                using (var results = _session.Run(inputs))
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                    predictionResult = prediction != null && prediction.Length > 0 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown") : "Error is prediction";
                }
                predictions.Add(new OrderPrediction { Orders = record, Prediction = predictionResult });
            }

            return View(predictions);

        }
    }
}