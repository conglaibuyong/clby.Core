using clby.Core.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace clby.Tests
{ 
    public class Test
    {
        [Fact]
        public void ReflectionHelper_GetScrubbedGenericName()
        {
            Assert.Equal("List_String", ReflectionHelper.GetScrubbedGenericName(typeof(List<string>)));
        }
    }
}
