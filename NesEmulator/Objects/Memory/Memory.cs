using System;
using System.Collections;
using System.Collections.Generic;

namespace Emulators.Mso6502
{
    public class Memory
        : IEnumerable<Byte>
    {
        #region Fields

        private const UInt16 SIZE = 0xFFFF;
        private DWord6502[] _innerArray;

        #endregion

        #region Constructors

        public Memory()
        {
            _innerArray = new DWord6502[SIZE >> 1];
        }

        #endregion

        #region Properties

        public Byte this[Int32 address]
        {
            get
            {
                return ((address & 1) == 0) ?
                        _innerArray[(address == 0) ? 0 : address >> 1].LowPart :
                        _innerArray[(address == 1) ? 0 : (address - 1) >> 1].HighPart;
            }

            set
            {
                if ((address & 1) == 0)
                {
                    _innerArray[(address == 0) ? 0 : address >> 1].LowPart = value;
                }
                else
                {
                    _innerArray[(address == 1) ? 0 : (address - 1) >> 1].HighPart = value;
                }
            }
        }

        #endregion

        #region Methods

        private static Byte GetUpperByte(UInt16 value)
        {
            return (Byte)((value & 0xFF00) >> 8);
        }

        private static Byte GetLowerByte(UInt16 value)
        {
            return (Byte)(value & 0x00FF);
        }

        private static void SetUpperByte(ref UInt16 bucket, Byte value)
        {
            bucket &= 0x00FF;
            bucket |= (UInt16)(value << 8);
        }

        private static void SetLowerByte(ref UInt16 bucket, Byte value)
        {
            bucket &= (UInt16)0xFF00;
            bucket |= (UInt16)value;
        }

        #endregion

        #region IEnumerable<Byte> Members

        public IEnumerator<Byte> GetEnumerator()
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
            : IEnumerator<Byte>
        {
            #region Fields

            private DWord6502[] _innerArray;
            private Int32 _currentIndex;
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
                    return (_getUpperByte) ?
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

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    return (_getUpperByte) ?
                        _innerArray[_currentIndex].HighPart :
                        _innerArray[_currentIndex].LowPart;
                }
            }

            public bool MoveNext()
            {
                if (_getUpperByte)
                {
                    _getUpperByte = !_getUpperByte;
                    return (++_currentIndex < _innerArray.Length);
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