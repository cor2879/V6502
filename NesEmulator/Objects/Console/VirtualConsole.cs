/***********************************************************************************************
 * 
 *  FileName: VirtualConsole.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
#pragma warning disable CS8618
using System;
using System.Collections.Generic;

using Emulators.Mso6502;

namespace Emulators.Nes
{
    public class VirtualConsole
    {
        #region Fields

        private Cpu6502 _cpu;
        private PictureProcessingUnit _ppu;

        #endregion

        #region Constructors

        public VirtualConsole()
        {
            Initialize();
        }

        #endregion

        #region Properties

        public Cpu6502 Cpu
        {
            get { return _cpu; }
        }

        public PictureProcessingUnit Ppu
        {
            get { return _ppu; }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _cpu = new Cpu6502(this);
            _ppu = new PictureProcessingUnit(this);
        }

        #endregion
    }
}