using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace intersecting_rectangles
{
    public class MyApplication
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;


        public MyApplication(ILogger<MyApplication> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;

            //var tt = _config["File"]; key/value config, if from command line: Key=value or /Key value or --key value

        }

        internal async Task Run(string fileName)
        {
            string jsonString;
            RectanglesIntersectionsCalculator calculator;
            try
            {
                jsonString = File.ReadAllText(fileName);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot read or open the file");
                return;
            }
            //var obj = JsonConvert.DeserializeObject(jsonString);

            try
            {
                /*
                RectanglesDTO data;
                using (var sr = new StreamReader(fileName))
                {
                    var reader = new RectanglesFileReader(sr);
                    reader.ReadContent
                    data = reader.ReadContent();
                }
                calculator = new RectanglesIntersectionsCalculator(data.rects, Console.Out);
                */
                IList<Newtonsoft.Json.Schema.ValidationError> errors;
                var rects = RectanglesFileReader.ReadContentFromString(jsonString, out errors);
                if (rects == null)
                {
                    _logger.LogError("Cannot decode json: ");
                    foreach (var err in errors)
                    {
                        _logger.LogError(err.Message);
                    }
                    return;
                }
                calculator = new RectanglesIntersectionsCalculator(rects.ToList(), Console.Out);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Cannot decode json: "+ex.Message);
                return;
            }
            calculator.PrintInput();
            calculator.TestCollision();
            calculator.PrintOutput();
        }
    }
}