using RealEstateTourNotebook.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateTourNotebook.Model.DataServices
{
    public class DataServiceCommon : IDataServiceCommon
    {
        private DataServiceRepair dsRepair;
        private DataServiceNearBy dsNearBy;
        private DataServicePictureTour dsPictureTour;

        public DataServiceCommon()
        {
            dsRepair = new DataServiceRepair();
            dsNearBy = new DataServiceNearBy();
            dsPictureTour = new DataServicePictureTour();
        }

        public void DeleteTourInCascade(Tables.Tour tour)
        {
            foreach (Repair repair in dsRepair.LoadRepairs())
            {
                dsRepair.DeleteRepair(repair);
            }
            foreach (NearBy nearBy in dsNearBy.LoadNearBys())
            {
                dsNearBy.DeleteNearBy(nearBy);

            }
            foreach (PictureTour pictureTour in dsPictureTour.LoadPictureTours())
            {
                dsPictureTour.DeletePictureTour(pictureTour);
            }
        }
    }
}
