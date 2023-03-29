using System.Collections.Generic;
using System.Threading.Tasks;
using XFIT.Core.Entities;

namespace XFIT.Core.Services;

public interface IActivityImporter
{
    Task<IEnumerable<Activity>> ImportAsync(string path);
}
