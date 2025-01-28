/***********************************************************************************************
 * 
 *  FileName: ReadOnlyArray.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Emulators.Nes
{
    public class ReadOnlyArray<T>
        : IEnumerable<T>
    {
        #region Fields

        private List<T> _innerList;

        #endregion

        #region Constructors

        public ReadOnlyArray(IEnumerable<T> collection)
        {
            _innerList = new List<T>(collection);
        }

        #endregion

        #region Properties

        public T this[Int32 index]
        {
            get { return _innerList[index]; }
        }

        public Int32 Length
        {
            get { return _innerList.Count; }
        }

        #endregion

        #region Methods

        public IEnumerator<T> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        #endregion

        #region Operators

        public static implicit operator ReadOnlyArray<T>(T[] foo)
        {
            return new ReadOnlyArray<T>(foo);
        }

        #endregion
    }
}