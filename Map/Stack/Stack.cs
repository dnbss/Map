namespace Map
{
    public class Stack<T>
    {
        public StackNode<T> Top { get; private set; }

        public Stack()
        {
            Top = null;
        }

        public void Push(T node)
        {
            Top = new StackNode<T>(node, Top);
        }

        public T Pop()
        {
            T node = Top.node;

            Top = Top.next;

            return node;
        }

        public bool IsEmpty()
        {
            return Top == null;
        }
    }
}