using System;

namespace Emulators.Mso6502
{
    public class CpuRegister<TValue>
        : ICpuRegister<TValue>
        where TValue : struct, IComparable 
    {
        #region Fields

        private TValue _value;

        #endregion

        #region Constructors

        public CpuRegister()
        { }

        #endregion

        #region Properties

        public TValue Value
        {
            get { return _value; }
            internal set 
            { 
                _value = value;
                OnValueChange(new EventArgs<TValue>(value));
            }
        }

        TValue ICpuRegister<TValue>.Value
        {
            get { return _value; }
            set 
            { 
                _value = value;
                OnValueChange(new EventArgs<TValue>(value));
            }
        }

        #endregion

        #region Methods

        private void OnValueChange(EventArgs<TValue> e)
        {
            if (ValueChange != null)
            {
                ValueChange.Invoke(this, e);
            }
        }

        #endregion

        #region Events

        public event EventHandler<EventArgs<TValue>> ValueChange;

        #endregion
    }
}