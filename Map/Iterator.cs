using System.ComponentModel.Design.Serialization;

namespace Map
{
    public class Iterator<T>
    {
        public virtual bool HasNext()
        {
            return false;
        }

        public virtual T Next()
        {
            return default(T);
        }
    }
}