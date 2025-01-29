/***********************************************************************************************
 * 
 *  FileName: UInt16ExtensionMethods.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Extensions
{
    public static class UInt16ExtensionMethods
    {
        public static UInt16 RotateLeft(this UInt16 num, int count)
        {
            var rotations = count % 16;
            return (UInt16)((num << rotations) | (num >> (16 - rotations)));
        }

        public static UInt16 RotateRight(this UInt16 num, int count)
        {
            var rotations = count % 16;
            return (UInt16)((num >> rotations) | (num << (16 - rotations)));
        }
    }
}