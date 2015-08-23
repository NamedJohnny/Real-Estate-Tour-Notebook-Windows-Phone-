using RealEstateTourNotebook.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstateTourNotebook.Model.DataServices
{
    public interface IDataServiceTour
    {
        void addTour(Tour newTour);
        Tour getTourById(int id);
        List<Tour> LoadTours();
        void UpdateTour();
        void DeleteTour(Tour tour);

        void RefreshTour(Tour tour);
    }

    public interface IDataServiceRepair
    {
        void addRepair(Repair newRepair);
        Repair getRepairById(int id);
        List<Repair> LoadRepairs();
        void UpdateRepair();
        void DeleteRepair(Repair repair);
    }


    public interface IDataServicePictureTour
    {
        void addPictureTour(PictureTour newPictureTour);
        PictureTour getPictureTourById(int id);
        List<PictureTour> LoadPictureTours();
        void UpdatePictureTour();
        void DeletePictureTour(PictureTour picTour);
    }

    public interface IDataServiceNearBy
    {
        void addNearBy(NearBy newNearBy);
        NearBy getNearByById(int id);
        List<NearBy> LoadNearBys();
        void UpdateNearBy();
        void DeleteNearBy(NearBy nearBy);
    }

    public interface IDataServiceCommon
    {
        void DeleteTourInCascade(Tour tour);
    }
}
