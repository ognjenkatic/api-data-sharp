using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ApiDataSharp.Tests.Infrastruture
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }
        public double Score { get; set; }
        public UserRank Rank { get; set; }
        public bool IsAdmin { get; set; }
    }
}
