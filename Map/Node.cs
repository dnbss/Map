namespace Map
{
    public class Node<T>
    {
        public Color color;

        public Node<T> parent;
        
        public Node<T> leftChild;

        public Node<T> rightChild;

        public T data;

        public int key;

        public bool isProcessed;

        public Node(int key, T data, Color color)
        {
            this.data = data;

            this.color = color;

            this.key = key;

            leftChild = null;

            rightChild = null;

            parent = null;

            isProcessed = false;
        }

        public static bool operator ==(Node<T> node, Color color)
        {
            if (node == null)
            {
                if (color == Color.Black)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return node.color == color;

        }

        public static bool operator !=(Node<T> node, Color colorNode2)
        {
            return !(node == colorNode2);
        }
    }
}