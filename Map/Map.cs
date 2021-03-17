using System;
using System.Collections;
using System.Runtime.CompilerServices;

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
                FindNode(key);
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
            string output = PrintToString();
            
            Console.WriteLine(output);
        }

        public string PrintToString()
        {
            
            Queue<TValue> values = GetValues();
            
            Queue<TKey> keys = GetKeys();

            string output = "";

            if (keys == null)
            {
                return output;
            }
            
            while (!keys.IsEmpty())
            {
                output += $"Key: {keys.Pop()} | Data: {values.Pop()}\n";
            }

            return output;
        }
    }
}