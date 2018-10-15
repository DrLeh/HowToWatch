using HowToWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToWatch.Services
{
    public interface ISourceService
    {
        SourceResponse Query(string query);
    }
}
