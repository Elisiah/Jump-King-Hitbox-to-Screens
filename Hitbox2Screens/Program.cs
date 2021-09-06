using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System;

namespace Hitbox2Screens
{
    class Program
    {
        private static void ScaleImage(string pathStr)
        {
            Image image = Image.FromFile(pathStr + "\\level.png");
            var newWidth = (int)(image.Width * 8);
            var newHeight = (int)(image.Height * 8);
            var scaledBitmap = new Bitmap(newWidth, newHeight);

            var scaledGraph = Graphics.FromImage(scaledBitmap);
            scaledGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
            scaledGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            scaledGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            scaledGraph.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            scaledGraph.DrawImage(image, imageRectangle);

            pathStr = pathStr + ("\\{0}.png");
            int screenNo = 0;
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    screenNo++;
                    var cropArea = new Rectangle((480 * i), (360 * j), 480, 360);
                    scaledBitmap.Clone(cropArea, scaledBitmap.PixelFormat).Save(string.Format(pathStr, screenNo));
                }
            }

            scaledGraph.Dispose();
            scaledBitmap.Dispose();
            image.Dispose();

            Console.WriteLine("\nImage objects removed from memory");
            Console.WriteLine("\nProgram has finished running...");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            var codeBaseUri = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            var pathStr = Path.GetDirectoryName(codeBaseUri.AbsolutePath);
            Console.WriteLine("Path Selected For Use");
            try { ScaleImage(pathStr); }
            catch (System.IO.FileNotFoundException)
            {
                System.Console.WriteLine("There was an error opening the level file. " +
                    "Please check the path.");
                System.Console.ReadLine();
            }
        }
    }
}
