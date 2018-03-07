using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CodeSmells.Obfuscators
{
    public static class Application
    {
        public static int CurrentUserCount { get; private set; }
    }

    public class Names
    {
        public static List<int> Generate(int n)
        {
            var x = new List<int>();
            for (int i = 2; n > 1; i++)
                for (; n % i == 0; n /= i)
                    x.Add(i);
            return x;
        }

        public static List<int> GeneratePrimeFactorsOf(int input)
        {
            var primeFactors = new List<int>();
            for (int candidateFactor = 2; input > 1; candidateFactor++)
                while (input % candidateFactor == 0)
                {
                    primeFactors.Add(candidateFactor);
                    input /= candidateFactor;
                }
            return primeFactors;
        }

        public string Format(string input)
        {
            int n;
            if (int.TryParse(input, out n))
            {
                if (n == 0) return "Not Started";
                if (n == 100) return "Complete";
                return n.ToString() + '%';
            }
            return input.Trim().ToUpper();
        }

        public string ListUsers()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Application.CurrentUserCount; i++)
            {
                sb.Append("User " + i + Environment.NewLine);
            }
            return sb.ToString();
        }

        private int iThsWkd;
        private int iThsRte;
        public int m_otCalc()
        {
            return iThsWkd * iThsRte +
                (int)Math.Round(0.5 * iThsRte *
                Math.Max(0, iThsWkd - 400));
        }

        private int tenthsWorked;
        private int tenthsRate;
        public int CalculateStraightPay()
        {
            return tenthsWorked * tenthsRate;
        }
        public int CalculateOverTimePay()
        {
            int overTimeTenths = Math.Max(0, tenthsWorked - 400);
            int overTimePay = CalculateOverTimeBonus(overTimeTenths);
            return CalculateStraightPay() + overTimePay;
        }
        private int CalculateOverTimeBonus(int overTimeTenths)
        {
            double bonus = 0.5 * tenthsRate * overTimeTenths;
            return (int)Math.Round(bonus);
        }

    }

    public class User
    {
        public string UserName { get; set; }

        public static int GetTotalUserCountInDatabaseTable()
        {
            throw new NotImplementedException();
        }

        public static SqlDataReader GetDataReaderWithRoles(string userName)
        {
            throw new NotImplementedException();
        }
    }

    public class Role { }
    public class User2
    {
        public string UserName { get; set; }

        public IEnumerable<Role> IsInRoles()
        {
            throw new NotImplementedException();
        }
    }

    public class SqlUserRepository
    {
        public int TotalUserCount()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> UserIsInRoles(string userName)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userName)
        {
            var user = GetUserFromDatabase(userName);

            return user ?? new User();
        }

        private User GetUserFromDatabase(string userName)
        {
            return null;
        }
    }

    public class Encodings
    {
        public void Example()
        {
            string strName;
            int iCount;
            DateTime dtStart;
            DateTime dtEnd;
            User usrOne;
            User usrTwo;
            SqlUserRepository surDataAccess;
            List<User> lstUsers;
        }
    }
}
