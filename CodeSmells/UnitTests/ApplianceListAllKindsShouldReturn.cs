using CodeSmells.ObjectOrientationAbusers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.UnitTests
{
    [TestFixture]
    public class ApplianceListAllKindsShouldReturn
    {
        [Test]
        public void TwoInstances()
        {
            var appliance = new Refrigerator();

            var result = appliance.ListAllKinds();

            Assert.AreEqual(2, result.Count());
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Appliance));
        }

        [Test]
        public void TwoInstancesWithReflection()
        {
            var appliance = new Refrigerator();

            var result = appliance.ListAllKindsWithReflection();

            Assert.AreEqual(2, result.Count());
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(Appliance));
        }
    }
}
