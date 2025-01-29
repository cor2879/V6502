/***********************************************************************************************
 * 
 *  FileName: VirtualConsole.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
#pragma warning disable CS8618
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Cpu;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;


namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Objects.Console
{
    public class VirtualConsole : IVirtualConsole
    {
        #region Fields

        private Processor _cpu;
        private PictureProcessingUnit _ppu;

        #endregion

        #region Constructors

        public VirtualConsole()
        {
            Initialize();
        }

        #endregion

        #region Properties

        public Processor Cpu
        {
            get { return _cpu; }
        }

        public PictureProcessingUnit Ppu
        {
            get { return _ppu; }
        }

        IProcessor IVirtualConsole.Cpu => throw new NotImplementedException();

        public byte ReadMemory(ushort address)
        {
            return this.Cpu.Memory[address];
        }

        public void RenderFrame()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void WriteMemory(ushort address, byte value)
        {
            this.Cpu.Memory[address] = value;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _cpu = new Processor(this);
            _ppu = new PictureProcessingUnit(this);
        }

        #endregion
    }
}