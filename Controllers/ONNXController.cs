//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using BuildsByBrickwellNew.Models;
//using Microsoft.ML;
//using Microsoft.ML.OnnxRuntime;
//using Microsoft.ML.OnnxRuntime.Tensors;



//namespace BuildsByBrickwellNew.Controllers
//{
//    public class ONNXController : Controller
//    {
//        private readonly IntexProjectContext _context;
//        private readonly InferenceSession _session;
//        private readonly ILogger<ONNXController> _logger;
//        public ONNXController(IntexProjectContext context, ILogger<ONNXController> logger)
//        {
//            _context = context;
//            _logger = logger;

//            try
//            {
//                _session = new InferenceSession()//In here we will need the path of the actual onnx file from google colab
//                _logger.LogInformation("ONNX model loaded successfully.")
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Error loading the ONNX model: {ex.Message}");   
//            }
//        }
//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Predict()//Here we are going to need all the parameters that we are going to predict
//            {
//            var class_type_dict = new Dictionary<int, string>
//            {
//                //{ 1, "mammal" },
//                //{ 2, "bird" },
//                //{ 3, "reptile" },
//                //{ 4, "fish" },
//                //{ 5, "amphibian" },
//                //{ 6, "bug" },
//                //{ 7, "invertebrate" } This dictionary will only be two for true or false
//            };

//            try
//            {
//                var input = new List<float> {}; // Whatever we put into the params for the Predict Method above
//                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

//                var inputs = new List<NamedOnnxValue>
//                {
//                    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
//                };

//                using (var results = _session.Run(inputs)) // makes the prediction with the inputs from the form (i.e. class_type 1-7)
//                {
//                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
//                    if (prediction != null && prediction.Length > 0)
//                    {
//                        // Use the prediction to get the animal type from the dictionary
//                        var fraudType = class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown"); //POSSIBLE CHANGE OF NUMBERS HERE*****
//                        ViewBag.Prediction = fraudType;
//                    }
//                    else
//                    {
//                        ViewBag.Prediction = "Error: Unable to make a prediction.";
//                    }
//                }

//                _logger.LogInformation("Prediction executed successfully.");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Error during prediction: {ex.Message}");
//                ViewBag.Prediction = "Error during prediction.";
//            }

//            return View("Index");//Do we need to change the view?
//        }
//        public IActionResult ShowPredictions()
//        {
//            var records = _context.zoo_animals.ToList();  // zoo_animals is a sqlite, not sure what that would be in our own
//            var predictions = new List<AnimalPrediction>();  // This is a model that in the example shows the type of animal and prediction (EASY TO MAKE IF WE NEED IT)

//            // Dictionary mapping the numeric prediction to an animal type
//            var class_type_dict = new Dictionary<int, string>
//            {
//                //{ 1, "mammal" },
//                //{ 2, "bird" },
//                //{ 3, "reptile" },
//                //{ 4, "fish" },
//                //{ 5, "amphibian" },
//                //{ 6, "bug" },
//                //{ 7, "invertebrate" } AGAIN TURN TO A 0 or 1
//            };

//            foreach (var record in records)
//            {
//                var input = new List<float>
//                {
//                    record.Hair, record.Feathers, record.Eggs, record.Milk,
//                    record.Airborne, record.Aquatic, record.Predator, record.Toothed,
//                    record.Backbone, record.Breathes, record.Venomous, record.Fins,
//                    record.Legs, record.Tail, record.Domestic, record.Catsize //Need to turn this inot our params
//                };
//                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

//                var inputs = new List<NamedOnnxValue>
//                {
//                    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
//                };

//                string predictionResult;
//                using (var results = _session.Run(inputs))
//                {
//                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
//                    predictionResult = prediction != null && prediction.Length > 0 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown") : "Error in prediction";
//                }

//                predictions.Add(new AnimalPrediction { Animal = record, Prediction = predictionResult }); // Adds the animal information and prediction for that animal to AnimalPrediction viewmodel
//            }

//            return View(predictions);
//        }



//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
//}
