﻿using System;

namespace Emulators.Mso6502
{
    public class StackOverflow6502Exception
        : Exception
    {
        #region Fields

        private Byte[] _callStack6502;

        #endregion

        #region Constructors

        public StackOverflow6502Exception(Byte[] stack)
        {
            _callStack6502 = stack;
        }

        #endregion

        #region Properties

        public Byte[] CallStack6502
        {
            get { return _callStack6502; }
        }

        #endregion
    }
}