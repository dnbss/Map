using System;

namespace Map
{
    public class Map<TValue> : RBTree<TValue>
    {
        public Map()
        {
            
        }
        
        public Map(int key, TValue data) : base(key, data)
        {
            
        }

        public Queue<int> GetKeys()
        {
            ResetIsProcessed(root);
            
            Queue<int> queue = new Queue<int>();
            
            Iterator<Node<TValue>> iterator = new RBTreeIterator<TValue>(root);

            while (iterator.HasNext())
            {
                int key = iterator.Next().key;
                
                queue.Push(key);
            }

            return queue;
        }
        
        public Queue<TValue> GetValues()
        {
            ResetIsProcessed(root);
            
            Queue<TValue> queue = new Queue<TValue>();
            
            Iterator<Node<TValue>> iterator = new RBTreeIterator<TValue>(root);

            while (iterator.HasNext())
            {
                TValue data = iterator.Next().data;
             
                queue.Push(data);
            }

            return queue;
        }

        public override void Insert(int key, TValue data)
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

        private void Print(Node<TValue> node)
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