using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace intersecting_rectangles
{
    public class RectanglesIntersectionsCalculator
    {
        private List<RectangleDTO> input;
        private TextWriter output;

        private List<RectangleIntersection> intersections;

        public RectanglesIntersectionsCalculator(List<RectangleDTO> p_input, System.IO.TextWriter p_output)
        {
            this.input = p_input;
            output = p_output;
            //init names and id
            for (var i = 0; i < input.Count; i++)
            {
                input[i].Name = $"{i + 1}";
                input[i].Id = i + 1;
            }
            intersections = new List<RectangleIntersection>();
        }

        public List<RectangleIntersection> CalculatedIntersections {
            get{
                return intersections;
            }
        }

        public void PrintInput()
        {
            output.WriteLine("Input:");
            for (var i = 0; i < input.Count; i++)
            {
                var r = input[i];
                output.WriteLine($"\t{i}: Rectangle at ({r.PrintForInput()})");
            }
        }
        public void PrintOutput()
        {
            //print
            output.WriteLine("Intersections:");
            for (var i = 0; i < intersections.Count; i++)
            {
                var inter = intersections[i];
                
                output.WriteLine($"\t{i+1}: Between rectangle {inter.getName()} at {inter.inter.PrintForOutput()}.");
            }
        }

        public void TestCollision()
        {
            intersections = new List<RectangleIntersection>();
            var currentIntersections = new List<RectangleIntersection>();
            for (var i = 0; i < input.Count - 1; i++)
            {
                for (var j = i + 1; j < input.Count; j++)
                {
                    RectangleDTO inter;
                    if (Collision(input[i], input[j], out inter))
                    {
                        inter.Name = $"{input[i].Name}, {input[j].Name}";

                        //output.WriteLine($"Colision between {input[i].Name} and {input[j].Name} at {inter.Print()}");
                        var rSource = new List<RectangleDTO>();
                        rSource.Add(input[i]);
                        rSource.Add(input[j]);
                        intersections.Add(new RectangleIntersection(rSource, inter));
                    }
                    else
                    {

                        //Console.WriteLine($"No Colision between {input[i].Name} and {input[j].Name}");
                    }
                }
                //input.AddRange(currentIntersections);
            }
            //check inter with previous intersections
            currentIntersections = new List<RectangleIntersection>();
            for (var i = 0; i < input.Count - 1; i++)
            {
                var r = input[i];
                RectangleDTO inter2;
                foreach (var inter in intersections.FindAll(me => me.input.Contains(r) == false))
                {
                    if (Collision(inter.inter, r, out inter2))
                    {
                        inter2.Name = $"{inter.inter.Name}, {r.Name}";

                        var rSource = new List<RectangleDTO>();
                        rSource.AddRange(inter.input);
                        rSource.Add(r);
                        var curInter = new RectangleIntersection(rSource, inter2);
                        //Console.WriteLine($"Inter Id: {curInter.Id}");
                        //check if dup. TODO: we could also check before testing the collision
                        if (currentIntersections.Exists(item => item.Id == curInter.Id) == false)
                        {
                            //output.WriteLine($"Colision between {inter.inter.Name} and {r.Name} at {inter2.Print()}");
                            currentIntersections.Add(curInter);
                        }
                        else
                        {
                            //Console.WriteLine("Duplicate. Skipping.");
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"No Colision between {inter.inter.Name} and {r.Name}");
                    }
                }
            }
            intersections.AddRange(currentIntersections);
            //TODO: check intersections again if inter with 4 or more rectangles

            
        }

        private bool Collision(RectangleDTO rect1, RectangleDTO rect2, out RectangleDTO rinter)
        {
            if (rect1.x < rect2.x + rect2.delta_x &&
                rect1.x + rect1.delta_x > rect2.x &&
                rect1.y < rect2.y + rect2.delta_y &&
                rect1.y + rect1.delta_y > rect2.y)
            {
                var xinter = Math.Max(rect1.x, rect2.x);
                var yinter = Math.Max(rect1.y, rect2.y);
                var x2inter = Math.Min(rect1.x + rect1.delta_x, rect2.x + rect2.delta_x);
                var y2inter = Math.Min(rect1.y + rect1.delta_y, rect2.y + rect2.delta_y);
                rinter = new RectangleDTO(xinter, yinter, x2inter - xinter, y2inter - yinter);
                return true;
            }
            rinter = new RectangleDTO(0, 0, 0, 0);//empty intersection
            return false;
            // collision detected!
        }
    }
}