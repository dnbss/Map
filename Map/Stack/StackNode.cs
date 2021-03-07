namespace Map
{
    public class StackNode<T>
    {
        public T node;

        public StackNode<T> next;

        public StackNode(T node, StackNode<T> next = null)
        {
            this.node = node;

            this.next = next;
        }
    }
}