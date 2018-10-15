using HowToWatch.Core.Data;
using HowToWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToWatch.Data
{
    public class MemoryRepository : IRepository
    {
        public MemoryRepository()
        {
            Users = new List<User>
            {
                new User
                {
                    Id = 1,
                }
            };

            var u1 = Users.First(x => x.Id == 1);
            var flatrate = MonetizationTypes.First(x => x.Id == 1);
            var netflix = Services.First(x => string.Equals(x.Name, "netflix", StringComparison.OrdinalIgnoreCase));
            var hulu = Services.First(x => string.Equals(x.Name, "hulu", StringComparison.OrdinalIgnoreCase));
            var amazon = Services.First(x => string.Equals(x.Name, "amazon", StringComparison.OrdinalIgnoreCase));

            UserServices = new List<UserServicePreference>
            {
                new UserServicePreference
                {
                    Preference = 1,
                    UserId = u1.Id,
                    User = u1,
                    MonetizationTypeId = flatrate.Id,
                    MonetizationType = flatrate,
                    Service = netflix,
                },
                new UserServicePreference
                {
                    Preference = 2,
                    UserId = u1.Id,
                    User = u1,
                    MonetizationTypeId = flatrate.Id,
                    MonetizationType = flatrate,
                    Service = hulu,
                },
                new UserServicePreference
                {
                    Preference = 3,
                    UserId = u1.Id,
                    User = u1,
                    MonetizationTypeId = flatrate.Id,
                    MonetizationType = flatrate,
                    Service = amazon,
                },
            };
        }

        public ICollection<User> Users { get; set; }
        public ICollection<UserServicePreference> UserServices { get; set; }
        public ICollection<Service> Services { get; set; } = Service.Defaults.ToList();
        public ICollection<ServiceUrl> ServiceUrls { get; set; }
        public ICollection<MonetizationType> MonetizationTypes { get; set; } = MonetizationType.Defaults.ToList();
    }
}
