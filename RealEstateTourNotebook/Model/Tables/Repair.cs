using RealEstateTourNotebook.ViewModel;
using RealEstateTourNotebook.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateTourNotebook.Model.Tables
{
    [Table(Name = "Repair")]
    [DataContract(IsReference = true)]
    public class Repair : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Constructors

        public Repair()
        {
        }

        #endregion

        #region Members

        private int _id;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        [DataMember]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    NotifyPropertyChanging("Id");
                    _id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        private byte[] _image;

        [Column(DbType = "image", UpdateCheck = UpdateCheck.Never)]
        [DataMember]
        public byte[] Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    NotifyPropertyChanging("Image");
                    _image = value;
                    NotifyPropertyChanged("Image");
                }
            }
        }

        private string _name;

        [Column]
        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanging("Name");
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        [Column]
        private int? _tourId;
        private EntityRef<Tour> _tour = new EntityRef<Tour>();
        [Association(Storage = "_tour", ThisKey = "_tourId", OtherKey = "Id", IsForeignKey = true)]
        [DataMember]
        public Tour Tour
        {
            get { return _tour.Entity; }
            set
            {
                NotifyPropertyChanging("Tour");
                _tour.Entity = value;

                if (value != null)
                {
                    _tourId = value.Id;
                }

                NotifyPropertyChanging("Tour");
            }
        }

        #endregion



        // Version column improves update performance.
#pragma warning disable 169
        [Column(IsVersion = true)]
        private Binary _version;


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
