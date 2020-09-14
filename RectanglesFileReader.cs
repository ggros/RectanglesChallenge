using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
namespace intersecting_rectangles
{
    public class RectanglesFileReader
    {
        private StreamReader streamReader;
        public RectanglesFileReader(StreamReader sr){
            this.streamReader = sr;
        }

        public RectanglesDTO ReadContent(){
            JsonTextReader reader = new JsonTextReader(this.streamReader);

            JSchemaValidatingReader validatingReader = new JSchemaValidatingReader(reader);
            validatingReader.Schema = RectanglesDTO.getJsonSchema();

            JsonSerializer serializer = new JsonSerializer();
            RectanglesDTO data = serializer.Deserialize<RectanglesDTO>(reader);

            return data;
        }

        /*
            alernate method with no stream
        */
        public static RectangleDTO[] ReadContentFromString(string jsonString, out IList<ValidationError> errors){
            if (string.IsNullOrEmpty(jsonString))
            {
                throw new System.ArgumentException($"'{nameof(jsonString)}' cannot be null or empty", nameof(jsonString));
            }

            JObject rectangles = JObject.Parse(jsonString);
            //IList<ValidationError> errors;
            bool valid = rectangles.IsValid(RectanglesDTO.getJsonSchema(), out errors);
            if(!valid){
                return null;
            }

            var arr = rectangles["rects"];

            return arr.ToObject<RectangleDTO[]>();
        }
    }
}