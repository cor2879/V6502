/***********************************************************************************************
 * 
 *  FileName: DWord6502.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System;
using System.Runtime.InteropServices;

namespace Emulators.Mso6502
{
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public struct DWord6502
        : IComparable<DWord6502>, IComparable<UInt16>
    {
        #region Fields

        /// <summary>
        /// The full 16-bit value
        /// </summary>
        [FieldOffset(0)]
        public UInt16 Value;

        [FieldOffset(0)]
        public Int16 SignedValue;

        /// <summary>
        /// The Low Order Byte.
        /// </summary>
        [FieldOffset(0)]
        public Byte LowPart;

        /// <summary>
        /// The High Order Byte.
        /// </summary>
        [FieldOffset(1)]
        public Byte HighPart;

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public int CompareTo(object obj)
        {
            if (obj is DWord6502)
            {
                return CompareTo((DWord6502)obj);
            }
            else if (obj is UInt16)
            {
                return CompareTo((UInt16)obj);
            }
            else
            {
                throw new InvalidOperationException("A type of 'Emulation.Nes.ProgramCounter' may only Invoke the 'CompareTo' API against other ProgramCounter instances or System.UInt16");
            }
        }

        public int CompareTo(DWord6502 other)
        {
            return Value - other.Value;
        }

        public int CompareTo(UInt16 other)
        {
            return Value - other;
        }

        #endregion

        #region Operators

        public static implicit operator DWord6502(UInt16 value)
        {
            var programCounter = new DWord6502()
            {
                Value = value
            };

            return programCounter;
        }

        public static implicit operator UInt16(DWord6502 value)
        {
            return value.Value;
        }

        #endregion
    }
}