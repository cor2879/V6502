/***********************************************************************************************
 * 
 *  FileName: EventArgs.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
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