/***********************************************************************************************
 * 
 *  FileName: ProcessorStatusRegister.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System;

namespace Emulators.Mso6502
{
    public class ProcessorStatusRegister
        : CpuRegister<Byte>
    {
        #region Fields

        public const Byte NegativeBit = 0x80;
        public const Byte OverflowBit = 0x40;
        public const Byte BrkBit = 0x10;
        public const Byte DecimalModeBit = 0x08;
        public const Byte IrqDisabledBit = 0x04;
        public const Byte ZeroBit = 0x02;
        public const Byte CarryBit = 0x01;

        #endregion

        #region Properties

        public bool NegativeFlag 
        { 
            get { return (Value & NegativeBit) == NegativeBit; }

            set
            {
                if (value)
                {
                    Value |= NegativeBit;
                }
                else if (NegativeFlag)
                {
                    Value ^= NegativeBit;
                }
            }
        }

        public bool OverflowFlag 
        { 
            get { return (Value & OverflowBit) == OverflowBit; }

            set
            {
                if (value)
                {
                    Value |= OverflowBit;
                }
                else if (OverflowFlag)
                {
                    Value ^= OverflowBit;
                }
            }
        }

        public bool BrkFlag 
        { 
            get { return (Value & BrkBit) == BrkBit; }

            set
            {
                if (value)
                {
                    Value |= BrkBit;
                }
                else if (BrkFlag)
                {
                    Value ^= BrkBit;
                }
            }
        }

        public bool DecimalFlag 
        { 
            get { return (Value & DecimalModeBit) == DecimalModeBit; }

            set
            {
                if (value)
                {
                    Value |= DecimalModeBit;
                }
                else if (DecimalFlag)
                {
                    Value ^= DecimalModeBit;
                }
            }
        }

        public bool IrqDisabledFlag 
        { 
            get { return (Value & IrqDisabledBit) == IrqDisabledBit; }

            set
            {
                if (value)
                {
                    Value |= IrqDisabledBit;
                }
                else if (IrqDisabledFlag)
                {
                    Value ^= IrqDisabledBit;
                }
            }
        }

        public bool ZeroFlag 
        { 
            get { return (Value & ZeroBit) == ZeroBit; }
            set
            {
                if (value)
                {
                    Value |= ZeroBit;
                }
                else if (ZeroFlag)
                {
                    Value ^= ZeroBit;
                }
            }
        }

        public bool CarryFlag 
        { 
            get { return (Value & CarryBit) == CarryBit; }
            set
            {
                if (value)
                {
                    Value |= CarryBit;
                }
                else if (CarryFlag)
                {
                    Value ^= CarryBit;
                }
            }
        }

        #endregion
    }
}