using ApiDataSharp.Models;
using ApiDataSharp.Services;
using ApiDataSharp.Tests.Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ApiDataSharp.Tests.Unit
{
    public class Filtering
    {
        private readonly DataSharpener<User> dataSharpener = new DataSharpener<User>();

        [Fact]
        public void ShouldFilterByStringEquals()
        {
            var filterRequest = new FilterRequest<User> { Filter = "equals(name:AUser)" };

            var filtered = dataSharpener.Filter(UserLists.Unsorted, filterRequest).ToList();
            var expected = UserLists.Unsorted.Where(a => a.Name == "AUser").ToList();

            UserAssert.ListsEqual(expected, filtered);
        }

        [Fact]
        public void ShouldFilterByStringContains()
        {
            var filterRequest = new FilterRequest<User> { Filter = "contains(name:ser)" };

            var filtered = dataSharpener.Filter(UserLists.Unsorted, filterRequest).ToList();
            var expected = UserLists.Unsorted.Where(a => a.Name.Contains("ser")).ToList();

            UserAssert.ListsEqual(expected, filtered);
        }

        [Fact]
        public void ShouldFilterByStringStartsWith()
        {
            var filterRequest = new FilterRequest<User> { Filter = "starts_with(name:ser)" };

            var filtered = dataSharpener.Filter(UserLists.Unsorted, filterRequest).ToList();
            var expected = UserLists.Unsorted.Where(a => a.Name.StartsWith("ser")).ToList();

            UserAssert.ListsEqual(expected, filtered);
        }

        [Fact]
        public void ShouldFilterByStringEndsWith()
        {
            var filterRequest = new FilterRequest<User> { Filter = "ends_with(name:ser)" };

            var filtered = dataSharpener.Filter(UserLists.Unsorted, filterRequest).ToList();
            var expected = UserLists.Unsorted.Where(a => a.Name.EndsWith("ser")).ToList();

            UserAssert.ListsEqual(expected, filtered);
        }

        [Fact]
        public void ShouldFilterByIntegerGreaterThan()
        {
            var filterRequest = new FilterRequest<User> { Filter = "gt(id:3)" };

            var filtered = dataSharpener.Filter(UserLists.Unsorted, filterRequest).ToList();
            var expected = UserLists.Unsorted.Where(a => a.Id > 3).ToList();

            UserAssert.ListsEqual(expected, filtered);
        }

        [Fact]
        public void ShouldFilterByIntegerLessThan()
        {
            var filterRequest = new FilterRequest<User> { Filter = "lt(id:3)" };

            var filtered = dataSharpener.Filter(UserLists.Unsorted, filterRequest).ToList();
            var expected = UserLists.Unsorted.Where(a => a.Id < 3).ToList();

            UserAssert.ListsEqual(expected, filtered);
        }

        [Fact]
        public void ShouldFilterByIntegerGreaterThanOrEqual()
        {
            var filterRequest = new FilterRequest<User> { Filter = "gte(id:3)" };

            var filtered = dataSharpener.Filter(UserLists.Unsorted, filterRequest).ToList();
            var expected = UserLists.Unsorted.Where(a => a.Id >= 3).ToList();

            UserAssert.ListsEqual(expected, filtered);
        }

        [Fact]
        public void ShouldFilterByIntegerLessThanOrEqual()
        {
            var filterRequest = new FilterRequest<User> { Filter = "lte(id:3)" };

            var filtered = dataSharpener.Filter(UserLists.Unsorted, filterRequest).ToList();
            var expected = UserLists.Unsorted.Where(a => a.Id <= 3).ToList();

            UserAssert.ListsEqual(expected, filtered);
        }

        [Fact]
        public void ShouldFilterByIntegerLessThanThenStringEquals()
        {
            var filterRequest = new FilterRequest<User> { Filter = "lt(id:4), equals(name:BUser)" };

            var filtered = dataSharpener.Filter(UserLists.Unsorted, filterRequest).ToList();
            var expected = UserLists.Unsorted.Where(a => a.Id < 4 && a.Name == "BUser").ToList();

            UserAssert.ListsEqual(expected, filtered);
        }
        // ?filter=lte(id:20), starts_with(name:vlado)&sort_by=asc(id), desc(name), desc(date_of_birth)
        //
    }
}
