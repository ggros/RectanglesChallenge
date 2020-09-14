using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
namespace intersecting_rectangles
{
    public class RectangleDTO
    {
        public string Name;
        public int Id;
        public RectangleDTO(int x,int y,int delta_x,int delta_y){
            this.x = x;
            this.y = y;
            this.delta_x = delta_x;
            this.delta_y = delta_y;
        }
        public string Print(){
            return $"{x},{y}, delta_x={delta_x}, delta_y={delta_y}";
        }
        [JsonProperty("x", Required = Required.Always)]
        public int x;

        [JsonProperty("y", Required = Required.Always)]
        public int y;

        [JsonProperty("delta_x", Required = Required.Always)]
        public int delta_x;

        [JsonProperty("delta_y", Required = Required.Always)]
        public int delta_y;

        public static JSchema getJsonSchema()
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(RectangleDTO[]));
            return schema;
        }
    }
}