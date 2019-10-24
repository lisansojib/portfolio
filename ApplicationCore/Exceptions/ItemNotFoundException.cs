using System;

namespace ApplicationCore.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(int itemId) : base($"No item found with id {itemId}")
        {
        }

        protected ItemNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public ItemNotFoundException(string message) : base(message)
        {
        }

        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
