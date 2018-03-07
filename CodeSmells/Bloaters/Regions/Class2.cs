using System;
using System.Linq;

namespace CodeSmells.Bloaters.Regions
{
    class Class2 : IDisposable
    {
        #region Constructors
        public Class2()
        {
        }
        #endregion

        #region Fields
        private int _something;
        #endregion

        #region Properties
        public string Name { get; set; }
        #endregion

        #region Public Methods
        public void Foo()
        {
        }
        #endregion

        #region Static Methods
        public static void Bar()
        { 
        }
        #endregion

        #region Private Methods
        private void Baz()
        { 
        }
        #endregion

        #region IDisposable Implementation
        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
