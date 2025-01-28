/***********************************************************************************************
 * 
 *  FileName: ICpuRegister.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System;

namespace Emulators.Mso6502
{
    public interface ICpuRegister<TValue>
        where TValue : struct, IComparable
    {
        #region Properties

        TValue Value { get; set; }

        #endregion

        #region Events

        event EventHandler<EventArgs<TValue>> ValueChange;

        #endregion
    }
}