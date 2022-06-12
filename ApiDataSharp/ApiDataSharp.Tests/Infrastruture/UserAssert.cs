using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace ApiDataSharp.Tests.Infrastruture
{
    public static class UserAssert
    {
        public static void ListsEqual(List<User> expected, List<User> actual)
        {
            if (expected.Count != actual.Count)
                throw new EqualException(expected, actual);

            Assert.Equal(JsonConvert.SerializeObject(expected),JsonConvert.SerializeObject(actual));
        }
    }
}
