using System.Text.RegularExpressions;
using System.Collections.Generic;
using RopeDataStructure;
using System;

namespace TextJustifyer
{
    public static class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter Text: ");
                string text = Console.ReadLine();

                Console.WriteLine("Enter Width: ");
                int width = int.Parse(Console.ReadLine());

                Console.WriteLine("=============================");

                Rope rope = new Rope(text);
                JustifyText(rope, width);

                PrintLines(rope.ReportNodeValues());

                Console.WriteLine("=============================");
            }
        }

        static void PrintLines(List<string> lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        static void JustifyText(Rope rope, int width)
        {
            for (int i = width; i < rope.Length; i += width)
            {
                while (!Regex.IsMatch(rope[i].ToString(), @"\s")) // Look for whitespace to split upon
                {
                    i--;
                }

                // Split then concat at i
                Rope other = rope.Split(i);
                rope.Concat(other);
            }
        }
    }
}
