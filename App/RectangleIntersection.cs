using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace intersecting_rectangles
{
    public class RectangleIntersection
    {
        public RectangleDTO inter;
        public List<RectangleDTO> input;

        public string Id;
        public RectangleIntersection(List<RectangleDTO> input, RectangleDTO inter)
        {
            this.input = input;
            this.inter = inter;
            this.Id = calcIdentifier();
        }
        private string calcIdentifier()
        {
            return string.Join("-", this.input.OrderBy(r => r.Id).Select(r => r.Id).ToArray());
        }

        public string getName()
        {
            var arr = this.input.OrderBy(r => r.Id).Select(r => r.Id).ToArray();
            var sb = new StringBuilder();
            for (var i = 0; i < arr.Length; i++)
            {
                if (i == arr.Length - 1)
                {
                    sb.AppendFormat(" and {0}",arr[i]);
                }
                else if (i == 0)
                {
                    sb.Append(arr[i]);
                }
                else {
                    sb.AppendFormat(", {0}",arr[i]);
                }
            }
            return sb.ToString();
        }

        // public void Print()
        // {
        //     //return $"{x},{y}, delta_x={delta_x}, delta_y={delta_y}";
        // }

    }
}