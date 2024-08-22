using rhinobill.core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Extensions
{
    public static class BogusPersonExtensions
    {
        public static Email SampleEmail(this Bogus.Person person)
        {
            return new Email(person.Email);
        }
    }
}
