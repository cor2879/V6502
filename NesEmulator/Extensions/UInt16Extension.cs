using System;

namespace Emulators.Mso6502
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