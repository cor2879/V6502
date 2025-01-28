using System;

namespace Emulators
{
    public class EventArgs<T>
        : EventArgs
    {
        #region Constructors

        public EventArgs(T data)
        {
            Data = data;
        }

        #endregion

        #region Properties

        public T Data { get; private set; }

        #endregion
    }
}