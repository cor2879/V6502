/***********************************************************************************************
 * 
 *  FileName: DWord6502.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives
{
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public struct DWord6502
        : IComparable<DWord6502>, IComparable<ushort>
    {
        #region Fields

        /// <summary>
        /// The full 16-bit value
        /// </summary>
        [FieldOffset(0)]
        public ushort Value;

        [FieldOffset(0)]
        public short SignedValue;

        /// <summary>
        /// The Low Order Byte.
        /// </summary>
        [FieldOffset(0)]
        public byte LowPart;

        /// <summary>
        /// The High Order Byte.
        /// </summary>
        [FieldOffset(1)]
        public byte HighPart;

        #endregion

        #region Constructors

        public DWord6502()
        {
            this.Value = 0;
        }

        public DWord6502(ushort value)
        {
            this.Value = value;
        }

        public DWord6502(byte lowByte, byte highByte)
        {
            this.LowPart = lowByte;
            this.HighPart = highByte;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"0x{Value:X4}";
        }
        public int CompareTo(object obj)
        {
            if (obj is DWord6502)
            {
                return CompareTo((DWord6502)obj);
            }
            else if (obj is ushort)
            {
                return CompareTo((ushort)obj);
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

        public int CompareTo(ushort other)
        {
            return Value - other;
        }

        public string DebuggerDisplay()
        {
            return $"0x{Value:X2}";
        }

        #endregion

        #region Operators

        public static implicit operator DWord6502(ushort value)
        {
            var programCounter = new DWord6502()
            {
                Value = value
            };

            return programCounter;
        }

        public static implicit operator ushort(DWord6502 value)
        {
            return value.Value;
        }

        public static DWord6502 operator +(DWord6502 left, sbyte right)
        {
            return new DWord6502((ushort)(left.Value + right));
        }

        #endregion
    }
}