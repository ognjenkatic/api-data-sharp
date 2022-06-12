using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ApiDataSharp.Tests.Infrastruture
{
    public static class UserLists
    {
        public static readonly IQueryable<User> Unsorted = (
            new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "AUser",
                    IsAdmin = false,
                    DateOfBirth = DateTime.ParseExact(
                        "12.09.1990",
                        "dd.MM.yyyy",
                        CultureInfo.InvariantCulture
                    ),
                    Rank = UserRank.Three,
                    Score = 0.1,
                    Username = "BH4X0r"
                },
                new User
                {
                    Id = 2,
                    Name = "AUser",
                    IsAdmin = true,
                    DateOfBirth = DateTime.ParseExact(
                        "13.09.1990",
                        "dd.MM.yyyy",
                        CultureInfo.InvariantCulture
                    ),
                    Rank = UserRank.Three,
                    Score = 0.4555,
                    Username = "CH4X0r"
                },
                new User
                {
                    Id = 3,
                    Name = "BUser",
                    IsAdmin = false,
                    DateOfBirth = DateTime.ParseExact(
                        "14.09.1990",
                        "dd.MM.yyyy",
                        CultureInfo.InvariantCulture
                    ),
                    Rank = UserRank.Two,
                    Score = -0.8,
                    Username = "DH4X0r"
                },
                new User
                {
                    Id = 4,
                    Name = "XUser",
                    IsAdmin = true,
                    DateOfBirth = DateTime.ParseExact(
                        "14.09.1990",
                        "dd.MM.yyyy",
                        CultureInfo.InvariantCulture
                    ),
                    Rank = UserRank.One,
                    Score = 0.1,
                    Username = "FH4X0r"
                }
            }
        ).AsQueryable();

        public static readonly List<User> ByNameDescending = Unsorted.OrderByDescending(
                a => a.Name
            )
            .ToList();

        public static readonly List<User> ByNameAscending = Unsorted.OrderBy(a => a.Name).ToList();

        public static readonly List<User> ByScoreDescending = Unsorted.OrderByDescending(
                a => a.Score
            )
            .ToList();

        public static readonly List<User> ByDateOfBirthDescending = Unsorted.OrderByDescending(
                a => a.DateOfBirth
            )
            .ToList();

        public static readonly List<User> ByRankDescending = Unsorted.OrderByDescending(
                a => a.Rank
            )
            .ToList();

        public static readonly List<User> ByIsAdminDescending = Unsorted.OrderByDescending(
                a => a.IsAdmin
            )
            .ToList();

        public static readonly List<User> ByNameAscendingThenByIdDescending = Unsorted.OrderBy(
                a => a.Name
            )
            .ThenByDescending(a => a.Id)
            .ToList();
    }
}
