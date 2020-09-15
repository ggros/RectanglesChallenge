using System.Collections.Generic;
using System.IO;
using System;
using intersecting_rectangles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RectangleIntersectionsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPrintInput()
        {
            //setup
            var rects = new List<RectangleDTO>();
            rects.Add(new RectangleDTO(1, 2, 3, 4));
            var ms = new MemoryStream();
            var output = new StreamWriter(ms);

            //test
            var calculator = new RectanglesIntersectionsCalculator(rects, output);
            calculator.PrintInput();


            output.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            var str = (new StreamReader(ms)).ReadToEnd();
            var template = $"Input:{Environment.NewLine}\t0: Rectangle at (1,2, delta_x=3, delta_y=4){Environment.NewLine}";
            Assert.AreEqual(template, str);
        }

        [TestMethod]
        public void TestPrintNegative()
        {
            //setup
            var rects = new List<RectangleDTO>();
            rects.Add(new RectangleDTO(10, 10, -10, -20));
            var ms = new MemoryStream();
            var output = new StreamWriter(ms);

            //test
            var calculator = new RectanglesIntersectionsCalculator(rects, output);
            calculator.PrintInput();


            output.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            var str = (new StreamReader(ms)).ReadToEnd();
            var template = $"Input:{Environment.NewLine}\t0: Rectangle at (0,-10, delta_x=10, delta_y=20){Environment.NewLine}";
            Assert.AreEqual(template, str);
        }

        [TestMethod]
        public void TestSampleOutput()
        {
            //setup
            var rects = new List<RectangleDTO>();

            rects.Add(new RectangleDTO(100, 100, 250, 80));
            rects.Add(new RectangleDTO(120, 200, 250, 150));
            rects.Add(new RectangleDTO(140, 160, 250, 100));
            rects.Add(new RectangleDTO(160, 140, 350, 190));

            var ms = new MemoryStream();
            var output = new StreamWriter(ms);

            //test
            var calculator = new RectanglesIntersectionsCalculator(rects, output);
            calculator.TestCollision();
            var intersections = calculator.CalculatedIntersections;

            //verify
            Assert.AreEqual(7,intersections.Count);

            Assert.AreEqual("1-3",intersections[0].Id);
            Assert.AreEqual("(140,160), delta_x=210, delta_y=20",intersections[0].inter.PrintForOutput());

            Assert.AreEqual("1-4",intersections[1].Id);
            Assert.AreEqual("(160,140), delta_x=190, delta_y=40",intersections[1].inter.PrintForOutput());

            Assert.AreEqual("2-3",intersections[2].Id);
            Assert.AreEqual("(140,200), delta_x=230, delta_y=60",intersections[2].inter.PrintForOutput());
            //Assert.AreEqual failed. 
            //Expected: <(140,200), delta_x=230, delta_y=60>.
            //Actual:   <(160,140), delta_x=190, delta_y=40>.

            Assert.AreEqual("2-4",intersections[3].Id);
            Assert.AreEqual("(160,200), delta_x=210, delta_y=130",intersections[3].inter.PrintForOutput());

            Assert.AreEqual("3-4",intersections[4].Id);
            Assert.AreEqual("(160,160), delta_x=230, delta_y=100",intersections[4].inter.PrintForOutput());

            Assert.AreEqual("1-3-4",intersections[5].Id);
            Assert.AreEqual("(160,160), delta_x=190, delta_y=20",intersections[5].inter.PrintForOutput());

            Assert.AreEqual("2-3-4",intersections[6].Id);
            Assert.AreEqual("(160,200), delta_x=210, delta_y=60",intersections[6].inter.PrintForOutput());


            calculator.PrintOutput();
            output.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            var str = (new StreamReader(ms)).ReadToEnd();
            var template = $"Intersections:{Environment.NewLine}";
            template += $"\t1: Between rectangle 1 and 3 at (140,160), delta_x=210, delta_y=20.{Environment.NewLine}";
            template += $"\t2: Between rectangle 1 and 4 at (160,140), delta_x=190, delta_y=40.{Environment.NewLine}";
            template += $"\t3: Between rectangle 2 and 3 at (140,200), delta_x=230, delta_y=60.{Environment.NewLine}";
            template += $"\t4: Between rectangle 2 and 4 at (160,200), delta_x=210, delta_y=130.{Environment.NewLine}";
            template += $"\t5: Between rectangle 3 and 4 at (160,160), delta_x=230, delta_y=100.{Environment.NewLine}";
            template += $"\t6: Between rectangle 1, 3 and 4 at (160,160), delta_x=190, delta_y=20.{Environment.NewLine}";
            template += $"\t7: Between rectangle 2, 3 and 4 at (160,200), delta_x=210, delta_y=60.{Environment.NewLine}";
            Assert.AreEqual(template, str);
        }
    }
}
