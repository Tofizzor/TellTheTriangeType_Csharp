using System;
using System.Collections.Generic;

/*Created by Justas Karalevicius
 * This small program checks the lines inputed to see if they form a triangle.
 * As this program is very simple it only detects the lines connecting in a sequence,
 * that means if user tries to connect first line end point with third line start, it will not recognise the connection
 * as there was no instructions in specification of the task I did not implement this feature, but the program can be easily extendable.
 */

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            //Option 1
            /* Scalene */
            var input = "[[10,22], [230,35]]," +
                " [[230,35], [78,85]], [[78,85], [10,22]]";
            /* Isosceles */
            //var input = "[[40,40], [40,60]]," +
            //    " [[40,60], [20,50]], [[20,50], [40,40]]";
            /* Equilateral */
            //var input = "[[1,1], [2,0]]," +
            //    " [[2,0], [2,1]], [[2,1], [1,1]]";

            /* Option 2: Uncomment code below to have user enter line coordinates into the console */
            //Console.WriteLine("Welcome to a program that finds out if your coordinates contains a triangle \n" +
            //    "Please enter input as follows(You can also enter it without []): [[X,Y],[X,Y]],[[X,Y],[X,Y]],[[X,Y],[X,Y]] \n" +
            //    "Fill the X and Y coordinates with starting point of the line and with the ending point of the line.");
            //input = Console.ReadLine();

            CheckForTriagle(input);
        }


        /// <summary>
        /// Input is formated and transformed into coordinates that are added into the dictionary
        /// </summary>
        /// <param name ="input"> A string containing coordinates</param>
        static void CheckForTriagle(string input)
        {
            try
            {
                var seperated = input.Replace("[", "").Replace("],", "").Replace("]", "");
                string[] coordinates = seperated.Split(" ");
                Dictionary<String, int> lines = new Dictionary<string, int>();
                // Line counter
                var lineCount = 1;
                if(coordinates.Length != 6)
                {
                    throw new Exception("6 Coordinates are required.");
                }
                for (int i = 0; i < coordinates.Length; i++)
                {
                    string[] split = coordinates[i].Split(",");
                    if(split[0].Equals("") || split[1].Equals(""))
                    {
                        throw new Exception("You have left one of the fields blank.");
                    }
                    //Adds to the 'lines' list either start of the line or end
                    if ((i % 2) == 0)
                    {
                        lines.Add( lineCount + "startX", int.Parse(split[0]));
                        lines.Add( lineCount + "startY", int.Parse(split[1]));
                    }
                    else
                    {
                        lines.Add( lineCount + "endX", int.Parse(split[0]));
                        lines.Add( lineCount + "endY", int.Parse(split[1]));
                        lineCount++;
                    }
                }
                // Checks if the coordinates connects and makes a triangle
                if (IsConnected(lines))
                {
                    var aLineLength = GetLineLength(lines["1startX"], lines["1endX"], lines["1startY"], lines["1endY"]);
                    var bLineLength = GetLineLength(lines["2startX"], lines["2endX"], lines["2startY"], lines["2endY"]);
                    var cLineLength = GetLineLength(lines["3startX"], lines["3endX"], lines["3startY"], lines["3endY"]);
                    CheckTriangle(aLineLength, bLineLength, cLineLength);
                }
                else
                    Console.WriteLine("It is not a triangle");
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
            }
        }

        /// <summary>
        /// Checks if coordinates in specified dictionary has equal values
        /// </summary>
        /// <param name ="cords"> A dictionary that contains 3 line start and end coordinates</param>
        static bool IsConnected(Dictionary<String, int> cords)
        {

            if (cords["1startX"] == cords["3endX"] && cords["1startY"] == cords["3endY"] && cords["1endX"] == cords["2startX"] 
                && cords["1endY"] == cords["2startY"] && cords["2endX"] == cords["3startX"] && cords["2endY"] == cords["3startY"])
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Counts length of the line given specified coordinates
        /// </summary>
        /// <param name ="startX"> The start of the coordinate X</param>
        /// <param name ="endX"> The end of the coordinate X</param>
        /// <param name ="startY"> The start of the coordinate Y</param>
        /// <param name ="endY"> The end of the coordinate Y</param>
        static double GetLineLength(int startX, int endX, int startY, int endY)
        {
            var theLength = Math.Sqrt(Math.Pow((endX - startX), 2) + Math.Pow((endY - startY), 2));
            return theLength;
        }

        /// <summary>
        /// Checks lines length and calculates the triangle angles with specified doubles
        /// </summary>
        /// <param name ="aLength"> Calculated triangle line A length</param>
        /// <param name ="bLength"> Calculated triangle line B length</param>
        /// <param name ="cLength"> Calculated triangle line C length</param>
        static void CheckTriangle(double aLength, double bLength, double cLength)
        {
            if (aLength + bLength > cLength || aLength + cLength > bLength || bLength + cLength > aLength)
            {
                // Finding triangle angles
                var aAngle = Math.Acos((cLength * cLength + aLength * aLength - bLength * bLength) 
                    / (2.0 * cLength * aLength)) * (180.0 / Math.PI);
                var bAngle = Math.Acos((bLength * bLength + aLength * aLength - cLength * cLength) 
                    / (2.0 * bLength * aLength)) * (180.0 / Math.PI);
                var cAngle = 180 - aAngle - bAngle;
                // Rules that define the type of triangle
                if(aAngle < 90 && bAngle < 90 && cAngle < 90)
                    Console.Write("Acute ");
                else if(aAngle == 90 || bAngle == 90 || cAngle == 90)
                    Console.Write("Right ");
                else
                    Console.Write("Obtuse ");
                if (aLength == bLength && bLength == cLength)
                    Console.WriteLine("Equilateral Triangle.");
                else if (aLength == bLength || aLength == cLength || bLength == cLength)
                    Console.WriteLine("Isosceles Triangle.");
                else
                    Console.WriteLine("Scalene Triangle.");
            }
            else
                Console.WriteLine("Lines connect but triangle is invalid");
        }

    }
}
