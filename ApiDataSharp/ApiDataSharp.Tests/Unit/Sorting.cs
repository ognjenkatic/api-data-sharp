using ApiDataSharp.Models;
using ApiDataSharp.Services;
using ApiDataSharp.Tests.Infrastruture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Newtonsoft.Json;

namespace ApiDataSharp.Tests.Unit
{
    public class Sorting
    {
        private readonly DataSharpener<User> sortService = new DataSharpener<User>(null);

        [Fact]
        public void ShouldSortByStringDescending()
        {
            var sortRequest = new SortRequest<User> { SortBy = "desc(name)" };

            var sorted = sortService.Sort(UserLists.Unsorted, sortRequest).ToList();
            var preSorted = UserLists.ByNameDescending;

            UserAssert.ListsEqual(preSorted, sorted);
        }

        [Fact]
        public void WhenSortyByNullShouldNotAffectOrder()
        {
            var sortRequest = new SortRequest<User> { SortBy = null };
            var result = sortService.Sort(UserLists.Unsorted, sortRequest).ToList();
            var unsorted = UserLists.Unsorted.ToList();

            UserAssert.ListsEqual(unsorted, result);
        }

        [Fact]
        public void ShouldThrowExceptionPropertyDoesntExist()
        {
            var sortRequest = new SortRequest<User> { SortBy = "desc(una)" };

            var exception = Assert.Throws<ArgumentException>(() => sortService.Sort(UserLists.Unsorted, sortRequest).ToList());

            Assert.Equal("Sort by parameter una does not match any of User properties", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionInvalidFormat()
        {
            var sortRequest = new SortRequest<User> { SortBy = "aa(una)" };

            var exception = Assert.Throws<ArgumentException>(() => sortService.Sort(UserLists.Unsorted, sortRequest).ToList());

            Assert.Equal(@"Sort by parameter does not match regex (desc|asc)\(([A-z0-9_]+)\)", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionDoubleArgument()
        {
            var sortRequest = new SortRequest<User> { SortBy = "desc(name),desc(name)" };

            var exception = Assert.Throws<ArgumentException>(() => sortService.Sort(UserLists.Unsorted, sortRequest).ToList());

            Assert.Equal(@"Sort by parameter name is defined more than once", exception.Message);
        }

        [Fact]
        public void ShouldSortByDateTimeDescending()
        {
            var sortRequest = new SortRequest<User> { SortBy = "desc(dateofbirth)" };

            var sorted = sortService.Sort(UserLists.Unsorted, sortRequest).ToList();
            var preSorted = UserLists.ByDateOfBirthDescending;

            UserAssert.ListsEqual(preSorted, sorted);
        }

        [Fact]
        public void ShouldSortByBoolDescending()
        {
            var sortRequest = new SortRequest<User> { SortBy = "desc(isadmin)" };

            var sorted = sortService.Sort(UserLists.Unsorted, sortRequest).ToList();
            var preSorted = UserLists.ByIsAdminDescending;

            UserAssert.ListsEqual(preSorted, sorted);
        }

        [Fact]
        public void ShouldSortByEnumDescending()
        {
            var sortRequest = new SortRequest<User> { SortBy = "desc(rank)" };

            var sorted = sortService.Sort(UserLists.Unsorted, sortRequest).ToList();
            var preSorted = UserLists.ByRankDescending;

            UserAssert.ListsEqual(preSorted, sorted);
        }

        [Fact]
        public void ShouldSortByDoubleDescending()
        {
            var sortRequest = new SortRequest<User> { SortBy = "desc(score)" };

            var sorted = sortService.Sort(UserLists.Unsorted, sortRequest).ToList();
            var preSorted = UserLists.ByScoreDescending;

            UserAssert.ListsEqual(preSorted, sorted);
        }

       
        [Fact]
        public void ShouldSortByStringThenByIntDescending()
        {
            var sortRequest = new SortRequest<User> { SortBy = "asc(name),desc(id)" };

            var sorted = sortService.Sort(UserLists.Unsorted, sortRequest).ToList();
            var preSorted = UserLists.ByNameAscendingThenByIdDescending;

            UserAssert.ListsEqual(preSorted, sorted);
        }
    }
}
