using HowToWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToWatch.Core.Data
{
    public interface IRepository
    {
        ICollection<User> Users { get; set; }
        ICollection<UserServicePreference> UserServices { get; set; }
        ICollection<Service> Services { get; set; }
        ICollection<ServiceUrl> ServiceUrls { get; set; }
        ICollection<MonetizationType> MonetizationTypes { get; set; }
    }
}
