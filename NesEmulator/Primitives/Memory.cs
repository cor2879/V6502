/***********************************************************************************************
 * 
 *  FileName: Memory.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System.Collections;
using OldSkoolGamesAndSoftware.Emulators.Cpu6502.Interfaces;

namespace OldSkoolGamesAndSoftware.Emulators.Cpu6502.Primitives
{
    public class Memory
        : IEnumerable<byte>
    {
        #region Fields

        private const ushort SIZE = 0x10000 >> 1;
        private DWord6502[] _innerArray;

        #endregion

        #region Constructors

        public Memory()
        {
            _innerArray = new DWord6502[SIZE];
        }

        #endregion

        #region Properties

        public byte this[int address]
        {
            get
            {
                return (address & 1) == 0 ?
                        _innerArray[address == 0 ? 0 : address >> 1].LowPart :
                        _innerArray[address == 1 ? 0 : address - 1 >> 1].HighPart;
            }

            set
            {
                if ((address & 1) == 0)
                {
                    _innerArray[address == 0 ? 0 : address >> 1].LowPart = value;
                }
                else
                {
                    _innerArray[address == 1 ? 0 : address - 1 >> 1].HighPart = value;
                }
            }
        }

        #endregion

        #region Methods

        public void Clear()
        {
            for (var i = 0; i < (SIZE >> 1); i++)
            {
                _innerArray[i] = 0;
            }
        }

        private static byte GetUpperByte(ushort value)
        {
            return (byte)((value & 0xFF00) >> 8);
        }

        private static byte GetLowerByte(ushort value)
        {
            return (byte)(value & 0x00FF);
        }

        private static void SetUpperByte(ref ushort bucket, byte value)
        {
            bucket &= 0x00FF;
            bucket |= (ushort)(value << 8);
        }

        private static void SetLowerByte(ref ushort bucket, byte value)
        {
            bucket &= 0xFF00;
            bucket |= value;
        }

        #endregion

        #region IEnumerable<Byte> Members

        public IEnumerator<byte> GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion

        #region SubClasses

        public sealed class Enumerator
            : IEnumerator<byte>
        {
            #region Fields

            private DWord6502[] _innerArray;
            private int _currentIndex;
            private bool _getUpperByte;

            #endregion

            #region Constructors

            internal Enumerator(Memory ram)
            {
                _innerArray = ram._innerArray;
                Reset();
            }

            ~Enumerator()
            {
                Dispose(false);
            }

            #endregion

            #region Methods

            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _innerArray = null;
                }
            }

            #endregion

            #region IEnumerator<byte> Members

            public byte Current
            {
                get
                {
                    return _getUpperByte ?
                            _innerArray[_currentIndex].HighPart :
                            _innerArray[_currentIndex].LowPart;
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get
                {
                    return _getUpperByte ?
                        _innerArray[_currentIndex].HighPart :
                        _innerArray[_currentIndex].LowPart;
                }
            }

            public bool MoveNext()
            {
                if (_getUpperByte)
                {
                    _getUpperByte = !_getUpperByte;
                    return ++_currentIndex < _innerArray.Length;
                }
                else
                {
                    _getUpperByte = !_getUpperByte;
                    return _currentIndex < _innerArray.Length;
                }
            }

            public void Reset()
            {
                _getUpperByte = true;
                _currentIndex = 0;
            }

            #endregion
        }

        #endregion
    }
}