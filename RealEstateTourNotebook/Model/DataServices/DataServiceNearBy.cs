using RealEstateTourNotebook.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateTourNotebook.Model.DataServices
{
    public class DataServiceNearBy : IDataServiceNearBy
    {
        private DatabaseDataContext db;

        public DataServiceNearBy()
        {
            db = App.db;
        }

        public void addNearBy(NearBy newNearBy)
        {
            db.nearBy.InsertOnSubmit(newNearBy);
        }

        public NearBy getNearByById(int id)
        {
            return db.nearBy.First(x => x.Id == id);
        }

        public List<NearBy> LoadNearBys()
        {
            return db.nearBy.ToList();
        }

        public void UpdateNearBy()
        {
            db.SubmitChanges();
        }

        public void DeleteNearBy(NearBy nearBy)
        {
            nearBy.Tour.NearBy.Remove(nearBy);
            db.nearBy.DeleteOnSubmit(nearBy);
        }
    }
}
