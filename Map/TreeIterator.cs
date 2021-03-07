namespace Map
{
    public class RBTreeIterator<TValue> : Iterator<Node<TValue>>
    {
        private Stack<Node<TValue>> stack;

        public RBTreeIterator(Node<TValue> start)
        {
            stack = new Stack<Node<TValue>>();
                
            stack.Push(start);
        }

        public override bool HasNext()
        {
            return !(stack.IsEmpty());
        }

        public override Node<TValue> Next()
        {
            Node<TValue> cur = stack.Top.node;

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