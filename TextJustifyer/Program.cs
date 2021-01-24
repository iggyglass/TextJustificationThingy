using System.Text.RegularExpressions;
using System;
using RopeDataStructure;

namespace TextJustifyer
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string test = "This is a test paragraph used for testing this text justification thingy.\nI am unsure where I am going with my current musings,\nbut this is probably enough of a sample if I were to wrap it up here, which is exactly what I am doing currently.";
            Rope rope = new Rope(test);

            JustifyText(rope, 20);

            Console.WriteLine(rope.Report());
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
