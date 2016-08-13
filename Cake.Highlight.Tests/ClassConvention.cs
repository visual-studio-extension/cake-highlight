using Fixie;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Hightlight.Tests
{
    public class ClassConvention : Convention
    {
        public ClassConvention()
        {
            var target = ConfigurationManager.AppSettings.Get("fixie");
            Classes.Where(x => x.Name == target);
        }
    }
}
