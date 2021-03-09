using System;
using System.Collections;

namespace Map
{
    public class Map<TKey, TValue> : RBTree<TKey, TValue> where TKey : notnull, IComparable
    {
        public Map()
        {
            
        }
        
        public Map(TKey key, TValue data) : base(key, data)
        {
            
        }

        public Queue<TKey> GetKeys()
        {
            ResetIsProcessed(root);
            
            Queue<TKey> queue = new Queue<TKey>();
            
            Iterator<Node<TKey, TValue>> iterator = new RBTreeIterator<TKey,TValue>(root);

            while (iterator.HasNext())
            {
                TKey key = iterator.Next().key;
                
                queue.Push(key);
            }

            return queue;
        }
        
        public Queue<TValue> GetValues()
        {
            ResetIsProcessed(root);
            
            Queue<TValue> queue = new Queue<TValue>();
            
            Iterator<Node<TKey, TValue>> iterator = new RBTreeIterator<TKey, TValue>(root);

            while (iterator.HasNext())
            {
                TValue data = iterator.Next().data;
             
                queue.Push(data);
            }

            return queue;
        }

        public override void Insert(TKey key, TValue data)
        {
            bool isExist = true;
            
            try
            {
                Find(key);
            }
            catch
            {
                isExist = false;
            }

            if (isExist)
            {
                throw new Exception("An element with this key already exists");
            }
            else
            {
                base.Insert(key, data);
            }
        }
        
        public void Print()
        {
            Print(root);
        }

        private void Print(Node<TKey, TValue> node)
        {
            if (node == null)
            {
                return;
            }
            
            Print(node.leftChild);

            Console.WriteLine($"Key:{node.key} | Data:{node.data} | {node.color}");
            
            Print(node.rightChild);
        }
    }
}