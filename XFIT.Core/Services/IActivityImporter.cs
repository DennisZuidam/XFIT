using System.Collections.Generic;
using XFIT.Core.Entities;

namespace XFIT.Core.Services;

public interface IActivityImporter
{
    IEnumerable<Activity> Import(string path);
}
