/***********************************************************************************************
 * 
 *  FileName: PictureProcessingUnit.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
#pragma warning disable CS8618
using System;

using Emulators.Mso6502;

namespace Emulators.Nes
{
    public class PictureProcessingUnit
    {
        #region Fields

        private const UInt16 IMAGE_PALETTE = 0x3F00;
        private const UInt16 SPRITE_PALETTE = 0x3F10;

        private static readonly ReadOnlyArray<UInt16> NAME_TABLE_ADDRESSES = new UInt16[] { 0x2000, 0x2400, 0x2800, 0x2C00 };
        private static readonly ReadOnlyArray<UInt16> ATTRIBUTE_TABLE_ADDRESSES = new UInt16[] { 0x23C0, 0x27C0, 0x2BC0, 0x2FC0 };
        private static readonly ReadOnlyArray<Byte> READ_WRITE_INCREMENT_TABLE = new Byte[] { 1, 32 };
        private static readonly ReadOnlyArray<UInt16> SPRITE_PATTERN_TABLE_ADDRESSES = new UInt16[] { 0x0000, 0x1000 };
        private static readonly ReadOnlyArray<UInt16> SCREEN_PATTERN_TABLE_ADDRESSES = new UInt16[] { 0x0000, 0x1000 };

        private VirtualConsole _console;
        private Memory _memory;

        #endregion

        #region Constructors

        public PictureProcessingUnit(VirtualConsole console)
        {
            _console = new VirtualConsole();
            Initialize();            
        }

        #endregion

        #region Properties

        public Memory Memory
        {
            get { return _memory; }
        }

        private Memory CpuMemory
        {
            get { return _console.Cpu.Memory; }
        }

        public Byte Ppu1
        {
            get { return CpuMemory[Cpu6502.PPU_CONTROL_REGISTER_1_ADDRESS]; }
            set { CpuMemory[Cpu6502.PPU_CONTROL_REGISTER_1_ADDRESS] = value; }
        }

        public Byte Ppu2
        {
            get { return CpuMemory[Cpu6502.PPU_CONTROL_REGISTER_2_ADDRESS]; }
            set { CpuMemory[Cpu6502.PPU_CONTROL_REGISTER_2_ADDRESS] = value; }
        }

        public UInt16 NameTableSelect
        {
            get { return NAME_TABLE_ADDRESSES[Ppu1 & 0x03]; }
        }

        public Byte AddressReadWriteIncrement
        {
            get { return READ_WRITE_INCREMENT_TABLE[(Ppu1 >> 2) & 0x01]; } 
        }

        public UInt16 SpritePatternTableAddress
        {
            get { return SPRITE_PATTERN_TABLE_ADDRESSES[(Ppu1 >> 3) & 0x01]; }
        }

        public UInt16 ScreenPatternTableAddress
        {
            get { return SCREEN_PATTERN_TABLE_ADDRESSES[(Ppu1 >> 4) & 0x01]; }
        }

        public SpriteSize SpriteSize
        {
            get { return (SpriteSize)((Ppu1 >> 5) & 0x01); }
        }

        public bool ExecuteNmiOnSpriteHitEnabled
        {
            get { return ((Ppu1 >> 6) & 0x01) != 0; }
        }

        public bool ExcuteNmiOnVblankEnabled
        {
            get { return ((Ppu1 >> 7) & 0x01) != 0; }
        }

        public ColorDisplay ColorDisplay
        {
            get { return (ColorDisplay)(Ppu2 & 0x01); }
        }

        public ImageClip ImageClip
        {
            get { return (ImageClip)((Ppu2 >> 1) & 0x01); }
        }

        public SpriteClip SpriteClip
        {
            get { return (SpriteClip)((Ppu2 >> 2) & 0x01); }
        }

        public ScreenDisplay ScreenDisplay
        {
            get { return (ScreenDisplay)((Ppu2 >> 3) & 0x01); }
        }

        public SpriteDisplay SpriteDisplay
        {
            get { return (SpriteDisplay)((Ppu2 >> 4) & 0x01); }
        }

        public FullBackgroundColor FullBackgroundColor
        {
            get { return (FullBackgroundColor)((Ppu2 >> 5) & 0x07); }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
             _memory = new Memory();
        }

        #endregion

        #region Nested Classes


        #endregion
    }
}