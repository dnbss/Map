namespace Map
{
    public class QueueNode<T>
    {
        public T node;

        public QueueNode<T> next;

        public QueueNode(T node, QueueNode<T> next = null)
        {
            this.node = node;

            this.next = next;
        }
    }
}