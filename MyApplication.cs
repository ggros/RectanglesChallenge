using System;
using System.IO;
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
            var jsonString = File.ReadAllText(fileName);
            //var obj = JsonConvert.DeserializeObject(jsonString);

            RectanglesDTO data;

            using (var sr = new StreamReader(fileName))
            {
                var reader = new RectanglesFileReader(sr);
                data = reader.ReadContent();
            }
            var calculator = new RectanglesIntersectionsCalculator(data.rects);
            calculator.PrintInput();
            calculator.TestCollision();
        }
    }
}