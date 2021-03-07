using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;

namespace Map
{
    public class RBTree<TValue>
    {
        public Node<TValue> root;
        
        public RBTree()
        {
            root = null;
        }

        public RBTree(int key, TValue data)
        {
            root = new Node<TValue>(key, data, Color.Black);
        }

        public virtual void Insert(int key, TValue data)
        {
            if (root == null)
            {
                root = new Node<TValue>(key, data, Color.Black);
                
                return;
            }
            
            Node<TValue> node = new Node<TValue>(key, data, Color.Red);
            Node<TValue> curr = root;
            Node<TValue> parent = null;
            
            while (curr != null)
            {
                parent = curr;
                
                if (node.key < curr.key)
                {
                    curr = curr.leftChild;
                }
                else
                {
                    curr = curr.rightChild;
                }
            }

            node.parent = parent;

            if (parent == null)
            {
                root = node;
            }
            else if (node.key < parent.key)
            {
                node.parent.leftChild = node;
            }
            else
            {
                node.parent.rightChild = node;
            }

            node.color = Color.Red;
            node.leftChild = null;
            node.rightChild = null; 

            RecoveryInsert(node);
            root.color = Color.Black;
            root.parent = null;
        }

        private void RecoveryInsert(Node<TValue> node)
        {
            if (node.parent == null || node.parent.color == Color.Black)
            {
                return;
            }

            if (Uncle(node) != null && Uncle(node).color == Color.Red)
            {
                node.parent.color = Color.Black;

                Uncle(node).color = Color.Black;

                if (GrandFather(node) != root)
                {
                    GrandFather(node).color = Color.Red;
                }
                
                RecoveryInsert(GrandFather(node));
            }
            else
            {
                if (GrandFather(node).rightChild == node.parent)
                {
                    if (node.parent.leftChild == node)
                    {
                        RightRotate(node.parent);

                        node.color = Color.Black;

                        node.parent.color = Color.Red;
                        
                        LeftRotate(node.parent);
                    }
                    else
                    {
                        node.parent.color = Color.Black;

                        GrandFather(node).color = Color.Red;
                        
                        LeftRotate(GrandFather(node));   
                    }
                }
                else
                {
                    if (node.parent.rightChild == node)
                    {
                        LeftRotate(node.parent);
                        
                        node.color = Color.Black;

                        node.parent.color = Color.Red;
                        
                        RightRotate(node.parent);
                    }
                    else
                    {
                        node.parent.color = Color.Black;

                        GrandFather(node).color = Color.Red;
                        
                        RightRotate(GrandFather(node));
                    }
                }
            }
            
            
        }

        public void Remove(int key)
        {
            Node<TValue> node;

            try
            {
                node = Find(key);
            }
            catch (Exception e)
            {
                throw;
            }

            if (node == root && node.leftChild == null && node.rightChild == null)
            {
                root = null;
                return;
            }
            
            if (node.leftChild != null && node.rightChild != null)
            {
                Node<TValue> closestNode = ClosestNode(node);

                Node<TValue> temp = closestNode;
                closestNode = node;
                node = temp;

                Swap(node, closestNode);
            }

            if (node.rightChild != null || node.leftChild != null)
            {
                Swap(node, node.rightChild != null ? node.rightChild : node.leftChild);

                node.leftChild = null;

                node.rightChild = null;
            }
            else
            {
                RecoveryRemove(node);
                
                if (node.parent.leftChild == node)
                {
                    node.parent.leftChild = null;
                }
                else
                {
                    node.parent.rightChild = null;
                }
            }
        }

        private void RecoveryRemove(Node<TValue> node)
        {
            if (node == Color.Red)
            {
                if (node.parent.leftChild == node)
                {
                    node.parent.leftChild = null;
                }
                else
                {
                    node.parent.rightChild = null;
                }
                return;
            }

            if (node.parent == Color.Red)
            {
                if (Brother(node)?.leftChild == Color.Black && Brother(node)?.rightChild == Color.Black)
                {
                    node.parent.color = Color.Black;

                    Brother(node).color = Color.Red;
                }
                
                else if (Brother(node)?.leftChild == Color.Red && node.parent.rightChild == node)
                {
                    node.parent.color = Color.Black;

                    Brother(node).color = Color.Red;

                    Brother(node).leftChild.color = Color.Black;
                    
                    RightRotate(node.parent);
                } 
                else if (Brother(node)?.rightChild == Color.Red && node.parent.leftChild == node) // symmetric case
                {
                    node.parent.color = Color.Black;

                    Brother(node).color = Color.Red;

                    Brother(node).rightChild.color = Color.Black;
                    
                    LeftRotate(node.parent);
                }
                
                else if (Brother(node)?.leftChild == Color.Black &&  Brother(node)?.rightChild == Color.Red && node.parent.rightChild == node)
                {
                    node.parent.color = Color.Black;

                    LeftRotate(Brother(node));
                    
                    RightRotate(node.parent);
                }
                else if (Brother(node)?.rightChild == Color.Black &&  Brother(node)?.leftChild == Color.Red && node.parent.leftChild == node) // symmetric case
                {
                    node.parent.color = Color.Black;

                    RightRotate(Brother(node));
                    
                    LeftRotate(node.parent);
                }
                return;
            }
            
            if (Brother(node) == Color.Red)
            {
                if (Brother(node).rightChild.leftChild == Color.Black && Brother(node).rightChild.rightChild == Color.Black && node.parent.rightChild == node)
                {
                    Brother(node).color = Color.Black;

                    Brother(node).rightChild.color = Color.Red;
                    
                    RightRotate(node.parent);
                }
                else if (Brother(node).leftChild.rightChild == Color.Black && Brother(node).leftChild.leftChild == Color.Black && node.parent.leftChild == node) // symmetric case
                {
                    Brother(node).color = Color.Black;

                    Brother(node).leftChild.color = Color.Red;
                    
                    LeftRotate(node.parent);
                }

                if (Brother(node).rightChild.leftChild == Color.Red && node.parent.rightChild == node)
                {
                    Brother(node).rightChild.leftChild.color = Color.Black;
                    
                    LeftRotate(Brother(node));
                    
                    RightRotate(node.parent);
                }
                else if (Brother(node).leftChild.rightChild == Color.Red && node.parent.leftChild == node) // symmetric case
                {
                    Brother(node).leftChild.rightChild.color = Color.Black;
                    
                    RightRotate(Brother(node));
                    
                    LeftRotate(node.parent);
                }
            }
            else
            {
                if (Brother(node).rightChild == Color.Red && node.parent.rightChild == node)
                {
                    Brother(node).rightChild.color = Color.Black;
                    
                    LeftRotate(Brother(node));
                    
                    RightRotate(node.parent);
                }
                else if (Brother(node).leftChild == Color.Red && node.parent.leftChild == node) // symmetric case
                {
                    Brother(node).leftChild.color = Color.Black;
                    
                    RightRotate(Brother(node));
                    
                    LeftRotate(node.parent);
                }
                
                else if (Brother(node).leftChild == Color.Red && Brother(node).rightChild == Color.Black && node.parent.rightChild == node)
                {
                    node.parent.color = Color.Red;
                    
                    RightRotate(node.parent);
                } 
                else if (Brother(node).rightChild == Color.Red && Brother(node).leftChild == Color.Black && node.parent.leftChild == node) // symmetric case
                {
                    node.parent.color = Color.Red;
                    
                    LeftRotate(node.parent);
                }
                
                else if (Brother(node).leftChild == Color.Black && Brother(node).rightChild == Color.Black)
                {
                    Brother(node).color = Color.Red;
                    
                    RecoveryRemove(node.parent);
                }
            }
        }

        private void Swap(Node<TValue> node1, Node<TValue> node2)
        {
            int tempKey = node1.key;
            node1.key = node2.key;
            node2.key = tempKey;

            TValue tempData = node1.data;
            node1.data = node2.data;
            node2.data = tempData;
        }

        private void LeftRotate(Node<TValue> y)
        {
            if (y == null)
            {
                return;
            }
            
            Node<TValue> x = y.rightChild;
            
            if (x == null)
            {
                return;
            }
            
            y.rightChild = x.leftChild;
            if (x.leftChild != null)
            {
                x.leftChild.parent = y;
            }
            
            if (y.parent != null)
            {
                x.parent = y.parent;

                if (y == y.parent.leftChild)
                {
                    y.parent.leftChild = x;
                }
                else
                {
                    y.parent.rightChild = x;
                }
            }
            else
            {
                root = x;
            }

            x.leftChild = y;
            y.parent = x;
        }

        private void RightRotate(Node<TValue> y)
        {
            if (y == null)
            {
                return;
            }
            
            Node<TValue> x = y.leftChild;
            
            if (x == null)
            {
                return;
            }
            
            y.leftChild = x.rightChild;
            if (x.rightChild != null)
            {
                x.rightChild.parent = y;
            }
            
            if (y.parent != null)
            {
                x.parent = y.parent;

                if (y == y.parent.rightChild)
                {
                    y.parent.rightChild = x;
                }
                else
                {
                    y.parent.leftChild = x;
                }
            }
            else
            {
                root = x;
            }

            x.rightChild = y;
            y.parent = x;
        }

        public Node<TValue> Find(int key)
        {
            Node<TValue> curr = root;
            bool isFound = false;

            while (!isFound)
            {
                if (curr == null)
                {
                    throw new Exception("Item not found!");
                }

                if (key < curr.key)
                {
                    curr = curr.leftChild;
                }
                else if (key > curr.key)
                {
                    curr = curr.rightChild;
                }
                else
                {
                    isFound = true;
                }
            }

            return curr;
        }

        public void Clear()
        {
            Stack<Node<TValue>> stack = new Stack<Node<TValue>>();
            
            stack.Push(root);

            Node<TValue> cur;

            while (!stack.IsEmpty())
            {
                cur = stack.Top.node;

                if (cur.rightChild != null)
                {
                    stack.Push(cur.rightChild);
                }

                if (cur.leftChild != null)
                {
                    stack.Push(cur.leftChild);
                }

                if (cur.leftChild == null && cur.rightChild == null)
                {
                    if (cur.parent?.leftChild == cur)
                    {
                        cur.parent.leftChild = null;
                    }
                    else if (cur.parent?.rightChild == cur)
                    {
                        cur.parent.rightChild = null;
                    }

                    stack.Pop();
                }
            }

            root = null;
        }

        protected void ResetIsProcessed(Node<TValue> cur)
        {
            if (cur == null)
            {
                return;
            }

            cur.isProcessed = false;
            
            ResetIsProcessed(cur.leftChild);
            ResetIsProcessed(cur.rightChild);
        }

        private Node<TValue> ClosestNode(Node<TValue> node)
        {
            Node<TValue> curr = node.rightChild;

            while (curr != null && curr.leftChild != null)
            {
                curr = curr.leftChild;
            }

            return curr;
        }

        private Node<TValue> GrandFather(Node<TValue> node)
        {
            return node.parent.parent;
        }
        
        private Node<TValue> Uncle(Node<TValue> node)
        {
            if (GrandFather(node) == null)
            {
                return null;
            }
            
            return GrandFather(node).leftChild != node.parent ? GrandFather(node).leftChild : GrandFather(node).rightChild;
        }

        private Node<TValue> Brother(Node<TValue> node)
        {
            return node.parent.leftChild != node ? node.parent.leftChild : node.parent.rightChild;
        }
    }
}