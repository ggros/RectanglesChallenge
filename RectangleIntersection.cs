using System;
using System.Collections.Generic;
using System.Linq;

namespace intersecting_rectangles
{
    public class RectangleIntersection
    {
        public RectangleDTO inter;
        public List<RectangleDTO> input;

        public string Id;
        public RectangleIntersection(List<RectangleDTO> input, RectangleDTO inter){
            this.input = input;
            this.inter = inter;
            this.Id = calcIdentifier();
        }
        private string calcIdentifier(){
            return string.Join("-",this.input.OrderBy(r => r.Id).Select(r=>r.Id).ToArray());
        }

        public void Print(){
            //return $"{x},{y}, delta_x={delta_x}, delta_y={delta_y}";
        }
        
    }
}