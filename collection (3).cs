using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj1oobjmain
{

        public interface IIterator<T>
        {
            bool MoveNext();
            bool MovePrev();
            void Reset();
            T Current { get; }
        }

        public class BinaryTreeNode<T>
        {
            public BinaryTreeNode<T> Parent { get; set; }
            public BinaryTreeNode<T> Left { get; set; }
            public BinaryTreeNode<T> Right { get; set; }
            public T Value { get; set; }

            public BinaryTreeNode(T value)
            {
                Value = value;
            }
        }

        public class BinaryTree<T> where T : IComparable<T>
        {
            private BinaryTreeNode<T> _root;

            public void Insert(T value)
            {
                if (_root == null)
                {
                    _root = new BinaryTreeNode<T>(value);
                }
                else
                {
                    Insert(_root, value);
                }
            }

            private void Insert(BinaryTreeNode<T> node, T value)
            {
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(value);
                    node.Left.Parent = node;
                }
                else if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(value);
                    node.Right.Parent = node;
                }
                else
                {
                    if (new Random().Next(2) == 0)
                    {
                        Insert(node.Left, value);
                    }
                    else
                    {
                        Insert(node.Right, value);
                    }
                }
            }

            public bool Remove(T value)
            {
                BinaryTreeNode<T> nodeToRemove = FindNode(_root, value);
                if (nodeToRemove == null)
                {
                    return false; // The value was not found in the tree
                }

                // Case 1: Node has no children
                if (nodeToRemove.Left == null && nodeToRemove.Right == null)
                {
                    ReplaceNodeInParent(nodeToRemove, null);
                }
                // Case 2: Node has only one child
                else if (nodeToRemove.Left == null || nodeToRemove.Right == null)
                {
                    BinaryTreeNode<T> childNode = nodeToRemove.Left ?? nodeToRemove.Right;
                    ReplaceNodeInParent(nodeToRemove, childNode);
                }
                // Case 3: Node has two children
                else
                {
                    BinaryTreeNode<T> inorderPredecessor = FindMaxNode(nodeToRemove.Left);
                    nodeToRemove.Value = inorderPredecessor.Value;
                    ReplaceNodeInParent(inorderPredecessor, inorderPredecessor.Left);
                }

                return true;
            }

            private BinaryTreeNode<T> FindNode(BinaryTreeNode<T> node, T value)
            {
                if (node == null)
                {
                    return null;
                }

                int compareResult = value.CompareTo(node.Value);
                if (compareResult == 0)
                {
                    return node;
                }
                else
                {

                    if (node.Left != null)
                    {
                        BinaryTreeNode<T> nodeL = FindNode(node.Left, value);
                        if (nodeL != null)
                        {
                            return nodeL;
                        }

                    }
                    if (node.Right != null)
                    {
                        BinaryTreeNode<T> nodeR = FindNode(node.Right, value);
                        if (nodeR != null)
                        {
                            return nodeR;
                        }

                    }
                return null;
                }
            }
        public void UpdateValue(T oldValue, T newValue)
        {
            BinaryTreeNode<T> nodeToUpdate = FindNode(_root, oldValue);
            if (nodeToUpdate != null)
            {
                nodeToUpdate.Value = newValue;
            }
        }

        private void ReplaceNodeInParent(BinaryTreeNode<T> node, BinaryTreeNode<T> newNode)
            {
                if (node.Parent != null)
                {
                    if (node.Parent.Left == node)
                    {
                        node.Parent.Left = newNode;
                    }
                    else
                    {
                        node.Parent.Right = newNode;
                    }
                }
                else
                {
                    _root = newNode;
                }

                if (newNode != null)
                {
                    newNode.Parent = node.Parent;
                }
            }

            private BinaryTreeNode<T> FindMaxNode(BinaryTreeNode<T> node)
            {
                while (node.Right != null)
                {
                    node = node.Right;
                }
                return node;
            }

            private class InOrderIterator : IIterator<T>
            {
                private BinaryTreeNode<T> _current;
                private Stack<BinaryTreeNode<T>> _stack;

                public InOrderIterator(BinaryTreeNode<T> root)
                {
                    _current = null;
                    _stack = new Stack<BinaryTreeNode<T>>();
                    PushLeftNodes(root);
                }

                public bool MoveNext()
                {
                    if (_stack.Count == 0)
                    {
                        return false;
                    }

                    _current = _stack.Pop();

                    if (_current.Right != null)
                    {
                        PushLeftNodes(_current.Right);
                    }

                    return true;
                }

                public bool MovePrev()
                {
                    throw new NotSupportedException("InOrderIterator does not support moving backwards. Use ReverseInOrderIterator for reverse traversal.");
                }

                public void Reset()
                {
                    _current = null;
                    _stack.Clear();
                }

                public T Current
                {
                    get
                    {
                        if (_current == null)
                        {
                            throw new InvalidOperationException("The iterator is not pointing to a valid node.");
                        }
                        return _current.Value;
                    }
                }

                private void PushLeftNodes(BinaryTreeNode<T> node)
                {
                    while (node != null)
                    {
                        _stack.Push(node);
                        node = node.Left;
                    }
                }
            }

            public IIterator<T> GetInOrderIterator()
             {
                   return new InOrderIterator(_root);
             }
            private class ReverseInOrderIterator : IIterator<T>
            {
                private BinaryTreeNode<T> _current;
                private Stack<BinaryTreeNode<T>> _stack;

                public ReverseInOrderIterator(BinaryTreeNode<T> root)
                {
                    _current = null;
                    _stack = new Stack<BinaryTreeNode<T>>();
                    PushRightNodes(root);
                }

                public bool MoveNext()
                {
                    if (_stack.Count == 0)
                    {
                        return false;
                    }

                    _current = _stack.Pop();

                    if (_current.Left != null)
                    {
                        PushRightNodes(_current.Left);
                    }

                    return true;
                }

                public bool MovePrev()
                {
                    throw new NotSupportedException("ReverseInOrderIterator does not support moving backwards. Use InOrderIterator for forward traversal.");
                }

                public void Reset()
                {
                    _current = null;
                    _stack.Clear();
                }

                public T Current
                {
                    get
                    {
                        if (_current == null)
                        {
                            throw new InvalidOperationException("The iterator is not pointing to a valid node.");
                        }
                        return _current.Value;
                    }
                }

                private void PushRightNodes(BinaryTreeNode<T> node)
                {
                    while (node != null)
                    {
                        _stack.Push(node);
                        node = node.Right;
                    }
                }
            }

            public IIterator<T> GetReverseInOrderIterator()
             {
                    return new ReverseInOrderIterator(_root);
              }

        
        }

}
