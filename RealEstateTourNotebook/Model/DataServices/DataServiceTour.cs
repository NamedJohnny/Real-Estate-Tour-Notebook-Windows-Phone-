using RealEstateTourNotebook.Model.Tables;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateTourNotebook.Model.DataServices
{
    public class DataServiceTour : IDataServiceTour
    {
        private DatabaseDataContext db;

        public DataServiceTour()
        {
            db = App.db;
        }

        public void addTour(Tables.Tour newTour)
        {
            db.tours.InsertOnSubmit(newTour);
            db.SubmitChanges();
        }

        public Tables.Tour getTourById(int id)
        {
            return db.tours.First(x => x.Id == id);
        }

        public List<Tables.Tour> LoadTours()
        {
            return db.tours.ToList();
        }

        public void UpdateTour()
        {
            db.SubmitChanges();
        }

        public void DeleteTour(Tables.Tour tour)
        {
            DataServiceCommon dsCommon = new DataServiceCommon();
            dsCommon.DeleteTourInCascade(tour);
            db.tours.DeleteOnSubmit(tour);
            db.SubmitChanges();
        }

        public void RefreshTour(Tour tour)
        {
            DiscardPendingChanges(db.nearBy.Context);
            DiscardPendingChanges(db.picturesTour.Context);
            DiscardPendingChanges(db.repairs.Context);
        }

        public void DiscardPendingChanges(DataContext context)
        {
            RefreshPendingChanges(context, RefreshMode.OverwriteCurrentValues);
            ChangeSet changeSet = context.GetChangeSet();
            if (changeSet != null)
            {
                //Undo inserts
                foreach (object objToInsert in changeSet.Inserts)
                {
                    context.GetTable(objToInsert.GetType()).DeleteOnSubmit(objToInsert);
                    if (objToInsert is NearBy)
                    {
                        (objToInsert as NearBy).Tour.NearBy.Remove((objToInsert as NearBy));
                    }
                    else if (objToInsert is Repair)
                    {
                        (objToInsert as Repair).Tour.Repair.Remove((objToInsert as Repair));
                    }
                    else if (objToInsert is PictureTour)
                    {
                        (objToInsert as PictureTour).Tour.Pictures.Remove((objToInsert as PictureTour));
                    }
                }
                //Undo deletes
                foreach (object objToDelete in changeSet.Deletes)
                {
                    context.GetTable(objToDelete.GetType()).InsertOnSubmit(objToDelete);
                    if (objToDelete is NearBy)
                    {
                        (objToDelete as NearBy).Tour.NearBy.Add((objToDelete as NearBy));
                    }
                    else if (objToDelete is Repair)
                    {
                        (objToDelete as Repair).Tour.Repair.Add((objToDelete as Repair));
                    }
                    else if (objToDelete is PictureTour)
                    {
                        (objToDelete as PictureTour).Tour.Pictures.Add((objToDelete as PictureTour));
                    }
                }
            }
        }

        /// <summary>
        ///     Refreshes all pending Delete/Update entity objects of current DataContext according to the specified mode.
        ///     Nothing will do on Pending Insert entity objects.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="refreshMode">A value that specifies how optimistic concurrency conflicts are handled.</param>
        public void RefreshPendingChanges(DataContext context, RefreshMode refreshMode)
        {
            ChangeSet changeSet = context.GetChangeSet();
            if (changeSet != null)
            {
                context.Refresh(refreshMode, changeSet.Deletes);
                context.Refresh(refreshMode, changeSet.Updates);
            }
        }
    }
}
