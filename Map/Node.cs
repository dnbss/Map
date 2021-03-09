namespace Map
{
    public class Node<TKey, TValue>
    {
        public Color color;

        public Node<TKey, TValue> parent;
        
        public Node<TKey, TValue> leftChild;

        public Node<TKey, TValue> rightChild;

        public TValue data;

        public TKey key;

        public bool isProcessed;

        public Node(TKey key, TValue data, Color color)
        {
            this.data = data;

            this.color = color;

            this.key = key;

            leftChild = null;

            rightChild = null;

            parent = null;

            isProcessed = false;
        }

        public static bool operator ==(Node<TKey, TValue> node, Color color)
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

        public static bool operator !=(Node<TKey, TValue> node, Color colorNode2)
        {
            return !(node == colorNode2);
        }
    }
}