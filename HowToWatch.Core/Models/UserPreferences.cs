using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToWatch.Models
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<UserServicePreference> ServicePreferences { get; set; } = new List<UserServicePreference>();
    }

    /// <summary>
    /// Represents a
    /// </summary>
    public class UserServicePreference
    {
        //fk to service
        public long ServiceId { get; set; }
        public Service Service { get; set; }

        //fk to userId
        public long UserId { get; set; }
        public User User { get; set; }

        public long MonetizationTypeId { get; set; }
        public MonetizationType MonetizationType { get; set; }

        /// <summary>
        /// lower the better, zero = not set, -1 = fully avoid... might need more control here
        /// </summary>
        public int Preference { get; set; }
    }

    public class Service : Entity
    {
        public string Name { get; set; }
        public ICollection<ServiceUrl> Urls { get; set; } = new List<ServiceUrl>();

        public static IEnumerable<Service> Defaults
        {
            get
            {
                yield return new Service
                {
                    Id = 1,
                    Name = "Netflix",
                    Urls =
                    {
                        new ServiceUrl
                        {
                            Url = "netflix"
                        }
                    }
                };
                yield return new Service
                {
                    Id = 2,
                    Name = "Hulu",
                    Urls =
                    {
                        new ServiceUrl
                        {
                            Url = "hulu"
                        }
                    }
                };
                yield return new Service
                {
                    Id = 3,
                    Name = "Amazon",
                    Urls =
                    {
                        new ServiceUrl { Url = "amazon" },
                        new ServiceUrl { Url = "amazon.com" },
                    }
                };
            }
        }
    }

    public class ServiceUrl : Entity
    {
        public string Url { get; set; }
    }

    public class MonetizationType : Entity
    {
        public const string FlatRate = "flatrate";

        public long ServiceId { get; set; }
        public Service Service { get; set; }

        //[Index(IsUnique = true)] //once the db is set up
        //FlatRate, etc
        public string TypeName { get; set; }

        public bool Matches(string type)
        {
            return type.Replace(" ", "") == TypeName.Replace(" ", "");
        }

        public static IEnumerable<MonetizationType> Defaults
        {
            get
            {
                yield return new MonetizationType
                {
                    Id = 1,
                    TypeName = "Flat Rate"
                };
            }
        }
    }
}
