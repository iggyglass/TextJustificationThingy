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
                (RopeNode node, int index) = nodeAtIndex(Root, i);
                return node.Text[index];
            }
        }

        public Rope Split(int i)
        {
            (RopeNode current, int index) = nodeAtIndex(Root, i);

            if (index != 0) // If split location is not at the start of the string
            {
                splitNode(current, index);
                current = current.Left;
            }

            if (current == current.Parent.Right) // if the node is a right node, then start at its parent
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

        private (RopeNode node, int charIndexInNode) nodeAtIndex(RopeNode current, int i)
        {
            if (current.Weight <= i && current.Right != null)
            {
                return nodeAtIndex(current.Right, i - current.Weight);
            }
            else if (current.Left != null)
            {
                return nodeAtIndex(current.Left, i);
            }
            else
            {
                return (current, i);
            }
        }

        private int getLength(RopeNode current)
        {
            if (current == null) return 0;

            return current.Text.Length + getLength(current.Left) + getLength(current.Right);
        }

        private void splitNode(RopeNode current, int i)
        {
            string left = current.Text.Substring(0, i);
            string right = current.Text.Substring(i);

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
    }
}
