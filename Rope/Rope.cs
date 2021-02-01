using System;
using System.Collections.Generic;
using System.Text;

namespace RopeDataStructure
{
    public class Rope
    {
        internal RopeNode Root;
        public int Length { get; private set; }

        public Rope(string text)
        {
            Root = new RopeNode(text);
            Length = text.Length;
        }

        internal Rope(RopeNode root)
        {
            Root = root;
            Length = getLength(root);
            Root.Weight = getLength(Root.Left);
        }

        public char this[int i]
        {
            get
            {
                if (i < 0 || i > Length) throw new IndexOutOfRangeException();

                (RopeNode node, int index) = nodeAtIndex(Root, i);
                return node.Text[index];
            }
        }

        public Rope Split(int index)
        {
            if (index < 0 || index > Length) throw new IndexOutOfRangeException();

            (RopeNode current, int i) = nodeAtIndex(Root, index);

            if (i != 0) // If split location is not at the start of the string
            {
                splitNode(current, i);
                current = current.Left;
            }

            if (current.Parent != null && current == current.Parent.Right) // if the node is a right node, then start at its parent
            {
                current = current.Parent;
            }

            Queue<RopeNode> orphans = new Queue<RopeNode>();

            while (current.Parent != null)
            {
                if (current == current.Parent.Right) break; // break if right child

                if (current.Parent.Right != null)
                {
                    orphans.Enqueue(current.Parent.Right);

                    current.Parent.Right.Parent = null;
                    current.Parent.Right = null;
                }

                current = current.Parent;
            }

            if (orphans.Count == 0) return new Rope("");

            Rope rope = new Rope(orphans.Dequeue());

            while (orphans.Count > 0) // Concat all the orphans together
            {
                rope.Concat(new Rope(orphans.Dequeue()));
            }

            Length -= rope.Length;

            return rope;
        }

        public string Report()
        {
            StringBuilder builder = new StringBuilder(Length);

            reportNode(Root, builder);

            return builder.ToString();
        }

        public List<string> ReportNodeValues()
        {
            List<string> values = new List<string>();

            reportNodeValues(Root, values);

            return values;
        }

        public void Concat(Rope other)
        {
            RopeNode newNode = new RopeNode("");

            newNode.Left = Root;
            newNode.Right = other.Root;

            Root.Parent = newNode;
            other.Root.Parent = newNode;

            newNode.Weight = getLength(newNode.Left);

            Length += other.Length;
            Root = newNode;
        }

        public void Delete(int start, int end)
        {
            if (start > end) throw new Exception("Start position is greater than end position.");
            if (start > Length || end > Length) throw new IndexOutOfRangeException();

            if (start == 0)
            {
                Rope temp = Split(end);

                Root = temp.Root;
                Length = temp.Length;
            }
            else
            {
                Rope temp = Split(start);
                Rope other = temp.Split(end - start);

                Concat(other);
            }
        }

        private (RopeNode node, int indexOfCharInNode) nodeAtIndex(RopeNode current, int index)
        {
            if (current.Weight <= index && current.Right != null)
            {
                return nodeAtIndex(current.Right, index - current.Weight);
            }
            else if (current.Left != null)
            {
                return nodeAtIndex(current.Left, index);
            }
            else
            {
                return (current, index);
            }
        }

        private int getLength(RopeNode current)
        {
            if (current == null) return 0;

            return current.Text.Length + getLength(current.Left) + getLength(current.Right);
        }

        private void splitNode(RopeNode current, int index)
        {
            string left = current.Text.Substring(0, index);
            string right = current.Text.Substring(index);

            current.Left = new RopeNode(left);
            current.Right = new RopeNode(right);

            current.Left.Parent = current;
            current.Right.Parent = current;

            current.Text = "";
        }

        private void reportNode(RopeNode current, StringBuilder builder)
        {
            if (current == null) return;

            builder.Append(current.Text);

            reportNode(current.Left, builder);
            reportNode(current.Right, builder);
        }

        private void reportNodeValues(RopeNode current, List<string> values)
        {
            if (current == null) return;

            if (current.Text != "") // Only leaf nodes have non-empty text
            {
                values.Add(current.Text);
            }
            else
            {
                reportNodeValues(current.Left, values);
                reportNodeValues(current.Right, values);
            }
        }
    }
}
