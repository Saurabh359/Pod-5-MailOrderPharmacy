using MfpeDrugsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MfpeDrugsApi.Provider
{
    public interface IProvider
    {
        List<LocationWiseDrug> searchDrugsByID(int id);
        List<LocationWiseDrug> searchDrugsByName(string name);
        bool getDispatchableDrugStock(int id, string location);
    }
}
