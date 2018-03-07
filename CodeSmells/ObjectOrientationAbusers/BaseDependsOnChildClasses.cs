using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodeSmells.ObjectOrientationAbusers
{
    abstract class Appliance
    {
        public IEnumerable<Appliance> ListAllKinds()
        {
            return new List<Appliance>()
            {
                new Refrigerator(),
                new Oven()
            };
        }

        public IEnumerable<Appliance> ListAllKindsWithReflection()
        {
            var targetAssembly = Assembly.GetExecutingAssembly();
            return targetAssembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Appliance)))
                .Select(t => Activator.CreateInstance(t) as Appliance);
        }
    }

    class Refrigerator : Appliance
    {
    }

    class Oven : Appliance
    {
    }
}
