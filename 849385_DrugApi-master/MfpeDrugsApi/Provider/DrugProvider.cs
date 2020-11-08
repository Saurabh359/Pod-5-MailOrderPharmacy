using MfpeDrugsApi.Models;
using MfpeDrugsApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MfpeDrugsApi.Provider
{
    public class DrugProvider : IProvider
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(DrugProvider));
        IRepository _item;
        public DrugProvider(IRepository drugrepo)
        {
            _item = drugrepo;

        }

        public bool getDispatchableDrugStock(int id, string location)
        {
            // DrugRepository obj1 = new DrugRepository();
            // return obj1.getDispatchableDrugStock(DrugId,Location);
            try
            {
                _log4net.Info("getDispatchableDrugstock from provider is called with DrugId & Location: " + id + location);
                return _item.getDispatchableDrugStock(id, location);
            }
            catch(Exception e)
            {
                _log4net.Error("Error occured from " + nameof(DrugProvider.getDispatchableDrugStock) + " Error Message " + e.Message);
                return false;
            }
        }

        public List<LocationWiseDrug> searchDrugsByID(int id)
        {
            try
            {
                _log4net.Info("searchDrugsById from provider is called with DrugId: " + id);
                return _item.searchDrugsByID(id);
            }
            catch(Exception e)
            {
                _log4net.Error("Error occured from " + nameof(DrugProvider.searchDrugsByID) + " Error Message " + e.Message);
                return null;
            }
        }

        public List<LocationWiseDrug> searchDrugsByName(string name)
        {
            try
            {
                _log4net.Info("searchDrugsByName from provider is called with DrugName: " + name);
                return _item.searchDrugsByName(name);
            }
            catch(Exception e)
            {
                _log4net.Error("Error occured from " + nameof(DrugProvider.searchDrugsByName) + " Error Message " + e.Message);
                return null;
            }

        }
    }
}
