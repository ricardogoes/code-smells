using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security;

namespace CodeSmells.Dispensables
{
    public class DeadCode : ISeeDeadCode
    {
        public void DoStuff()
        {
            int upperBound = 100;
            if (upperBound > 50)
            {
                throw new NotImplementedException();
            }
            var fibNumbers = new List<long>();
            if (fibNumbers.Count == 0)
            {
                fibNumbers.Add(1);
                fibNumbers.Add(2);
            }
            int index = 2;
            long term = 0;
            while (term <= upperBound)
            {
                term = fibNumbers[index - 2] + fibNumbers[index - 1];
                fibNumbers.Add(term);
                index++;
            }
        }

        private void DoOtherStuff()
        {
            // does other stuff
        }
    }
  
    public interface ISeeDeadCode
    {
        void DoStuff();
    }
}
