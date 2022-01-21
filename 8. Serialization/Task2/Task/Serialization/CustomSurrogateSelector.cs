using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.Serialization
{
    public class CustomSurrogateSelector : ISurrogateSelector
    {
        public void ChainSelector(ISurrogateSelector selector)
        {
            throw new NotImplementedException();
        }

        public ISurrogateSelector GetNextSelector()
        {
            throw new NotImplementedException();
        }

        public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
        {
            selector = null;

            if (type.Equals(typeof(List<Order_Detail>)))
            {
                return new OrderDetailSurrogate();
            }

            return null;
        }
    }
}
