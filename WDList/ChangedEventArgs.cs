using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDList
{
    public enum ChangedType
    {
        AddOrInsert,
        Remove
    }

    public class ChangedEventArgs<T> : EventArgs
    {
        public ChangedType Type;
        public int OldIndex = -1, NewIndex = -1;
        public T Item;
    }
}
