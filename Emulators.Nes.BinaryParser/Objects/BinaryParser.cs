/***********************************************************************************************
 * 
 *  FileName: BinaryParser.cs
 *  Copyright © 2025 Old Skool Games and Software
 *  
 ***********************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Emulators.Nes.Binary
{
    public class NesRomParser
        : IDisposable
    {
        #region Fields

        private Byte[] _bytes;

        #endregion

        #region Constructors

        public NesRomParser(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath", "The parameter 'filePath' may not be null.");
            }

            Int32 size = (Int32)new FileInfo(filePath).Length;

            using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open), Encoding.ASCII))
            {
                _bytes = reader.ReadBytes(size);
            }
        }

        ~NesRomParser()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        public Byte this[int index]
        {
            get { return _bytes[index]; }
        }

        public int Count
        {
            get { return _bytes.Length; }
        }

        #endregion

        #region Methods

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bytes = null;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        #endregion
    }
}