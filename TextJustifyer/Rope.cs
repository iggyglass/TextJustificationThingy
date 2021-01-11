using System;
using System.Collections.Generic;
using System.Text;

namespace TextJustifyer
{
    public class Rope
    {
        public RopeNode Root;
        public int Length = 0;

        public char this[int i]
        {
            get
            {
                return index(Root, i);
            }
        }

        private char index(RopeNode current, int i)
        {
            if (current.Weight <= i && current.Right != null)
            {
                return index(current.Right, i - current.Weight);
            }
            else if (current.Left != null)
            {
                return index(current.Left, i);
            }
            else
            {
                return current.Text[i];
            }
        }

        private int getWeight(RopeNode current)
        {
            int left = current.Left != null ? getWeight(current.Left) : 0;
            int right = current.Right != null ? getWeight(current.Right) : 0;

            return left + right;
        }

        

        /*public Rope Split(int i)
        {
            if (i == Root.Weight) // case 1 -- split is at the end of string
            {

            }
            else // other case
            {

            }
        }*/

        public string Report()
        {
            return reportNode(Root);
        }

        private string reportNode(RopeNode current)
        {
            if (current.Left == null && current.Right == null)
            {
                return current.Text;
            }
            else
            {
                string str = "";

                if (current.Left != null) str += reportNode(current.Left);
                if (current.Right != null) str += reportNode(current.Right);

                return str;
            }
        }

        public void Concat(Rope other)
        {
            RopeNode newNode = new RopeNode();

            newNode.Left = Root;
            newNode.Right = other.Root;

            newNode.Weight = getWeight(newNode.Left);

            Length += other.Length;
            Root = newNode;
        }
    }
}
