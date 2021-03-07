namespace Map
{
    public class Queue<T>
    {
        public QueueNode<T> First { get; private set; }

        public QueueNode<T> Last { get; private set; }

        public Queue()
        {
            First = null;

            Last = null;
        }

        public bool IsEmpty()
        {
            return First == null;
        }

        public void Push(T node)
        {
            if (IsEmpty())
            {
                First = new QueueNode<T>(node, First);

                Last = First;
            }
            else
            {
                Last.next = new QueueNode<T>(node);

                Last = Last.next;
            }
        }

        public T Pop()
        {
            T temp = First.node;

            First = First.next;

            return temp;
        }
    }
}