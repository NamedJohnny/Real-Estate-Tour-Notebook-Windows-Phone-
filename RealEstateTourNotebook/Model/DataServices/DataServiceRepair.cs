using RealEstateTourNotebook.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateTourNotebook.Model.DataServices
{
    public class DataServiceRepair : IDataServiceRepair
    {
        private DatabaseDataContext db;

        public DataServiceRepair()
        {
            db = App.db;
        }

        public void addRepair(Tables.Repair newRepair)
        {
            db.repairs.InsertOnSubmit(newRepair);
        }

        public Tables.Repair getRepairById(int id)
        {
            return db.repairs.First(x => x.Id == id);
        }

        public List<Tables.Repair> LoadRepairs()
        {
            return db.repairs.ToList();
        }

        public void UpdateRepair()
        {
            db.SubmitChanges();
        }

        public void DeleteRepair(Tables.Repair Repair)
        {
            Repair.Tour.Repair.Remove(Repair);
            db.repairs.DeleteOnSubmit(Repair);
        }


    }
}
