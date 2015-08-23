using RealEstateTourNotebook.Model.Tables;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateTourNotebook.Model
{
    public class DatabaseDataContext : DataContext
    {
        public DatabaseDataContext(string connectionString)
            : base(connectionString)
        { }

        public Table<Tour> tours;
        public Table<PictureTour> picturesTour;
        public Table<NearBy> nearBy;
        public Table<Repair> repairs;
    }
}
