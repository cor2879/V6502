/***********************************************************************************************
 * 
 *  FileName: Accumulator.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
#pragma warning disable CS8618, CS8622
using System;

namespace Emulators.Mso6502
{
    public sealed class Accumulator
        : CpuRegister<Byte>
    {
        #region Fields

        private Byte _lastSignBitValue = 0x00;

        #endregion

        #region Constructors

        public Accumulator()
        {
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ValueChange += this_ValueChanged;
        }

        private void OnSignBitChanged(EventArgs<Byte> e)
        {
            if (SignBitChange != null)
            {
                SignBitChange.Invoke(this, e);
            }
        }

        #endregion

        #region Events

        public event EventHandler<EventArgs<Byte>> SignBitChange;

        #endregion

        #region Event Handlers

        private void this_ValueChanged(object sender, EventArgs<Byte> e)
        {
            if ((e.Data & _lastSignBitValue) != _lastSignBitValue)
            {
                OnSignBitChanged(e);
                _lastSignBitValue = (Byte)(e.Data & 0x80);
            }
        }

        #endregion
    }
}