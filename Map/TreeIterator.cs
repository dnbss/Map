namespace Map
{
    public class RBTreeIterator<TKey, TValue> : Iterator<Node<TKey, TValue>>
    {
        private Stack<Node<TKey,TValue>> stack;

        public RBTreeIterator(Node<TKey, TValue> start)
        {
            stack = new Stack<Node<TKey,TValue>>();
                
            stack.Push(start);
        }

        public override bool HasNext()
        {
            return !(stack.IsEmpty());
        }

        public override Node<TKey, TValue> Next()
        {
            Node<TKey, TValue> cur = stack.Top.node;

            while (cur.leftChild != null && !cur.leftChild.isProcessed)
            {
                cur = cur.leftChild;
                
                stack.Push(cur);
            }

            stack.Pop();

            if (cur.rightChild != null && !cur.rightChild.isProcessed)
            {
                stack.Push(cur.rightChild);
            }

            cur.isProcessed = true;
            
            return cur;
        }
    }
}