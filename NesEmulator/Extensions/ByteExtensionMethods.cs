/***********************************************************************************************
 * 
 *  FileName: ByteExtensionMethods.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Extensions
{
    public static class ByteExtensionMethods
    {
        public static Byte RotateLeft(this Byte num, int count)
        {
            var rotations = count % 8;
            return (Byte)((num << rotations) | (num >> (8 - rotations)));
        }

        public static Byte RotateRight(this Byte num, int count)
        {
            var rotations = count % 8;
            return (Byte)((num >> rotations) | (num << (8 - rotations)));
        }

        public static Byte ToByte(Boolean value)
        {
            if (value)
            {
                return 0x01;
            }
            else
            {
                return 0x00;
            }
        }

        public static Int32 ToInt32(Boolean value)
        {
            if (value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}