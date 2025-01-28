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

namespace Emulators.Cpu6502
{
    public class VirtualConsole
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