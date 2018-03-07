using System;
using System.Linq;

namespace CodeSmells.Bloaters.Regions
{
    class Class3 : IDisposable
    {
        public Class3()
        {
        }

        private int _something;

        public string Name { get; set; }

        public void Foo()
        {
        }

        public static void Bar()
        {
        }

        private void Baz()
        {
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
