using RealEstateTourNotebook.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateTourNotebook.Model.DataServices
{
    public class DataServicePictureTour : IDataServicePictureTour
    {
        private DatabaseDataContext db;

        public DataServicePictureTour()
        {
            db = App.db;
        }

        public void addPictureTour(PictureTour newPictureTour)
        {
            db.picturesTour.InsertOnSubmit(newPictureTour);
        }

        public PictureTour getPictureTourById(int id)
        {
            return db.picturesTour.First(x => x.Id == id);
        }

        public List<PictureTour> LoadPictureTours()
        {
            return db.picturesTour.ToList();
        }

        public void UpdatePictureTour()
        {
            db.SubmitChanges();
        }

        public void DeletePictureTour(PictureTour picTour)
        {
            picTour.Tour.Pictures.Remove(picTour);
            db.picturesTour.DeleteOnSubmit(picTour);
        }
    }
}
