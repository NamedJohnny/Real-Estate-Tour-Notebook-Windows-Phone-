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
    [Table(Name = "Tour")]
    [DataContract(IsReference = true)]
    public class Tour : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Constructors

        public Tour()
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

        private byte[] _mainImage;

        [Column(DbType = "image", UpdateCheck = UpdateCheck.Never)]
        [DataMember]
        public byte[] MainImage
        {
            get { return _mainImage; }
            set
            {
                if (_mainImage != value)
                {
                    NotifyPropertyChanging("MainImage");
                    _mainImage = value;
                    NotifyPropertyChanged("MainImage");
                }
            }
        }

        private DateTime _date;

        [Column]
        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (!_date.Equals(value))
                {
                    NotifyPropertyChanging("Date");
                    _date = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }

        private TourType _type;
        [Column]
        public TourType Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    NotifyPropertyChanging("Type");
                    _type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        #region real estate


        private EstateType _estateType;
        [Column]
        public EstateType EstateType
        {
            get { return _estateType; }
            set
            {
                if (_estateType != value)
                {
                    NotifyPropertyChanging("EstateType");
                    _estateType = value;
                    NotifyPropertyChanged("EstateType");
                }
            }
        }

        private AppType? _appType;
        [Column(CanBeNull = true)]
        public AppType? AppType
        {
            get { return _appType; }
            set
            {
                if (_appType != value)
                {
                    NotifyPropertyChanging("AppType");
                    _appType = value;
                    NotifyPropertyChanged("AppType");
                }
            }
        }

        private string _ownerName;

        [Column]
        [DataMember]
        public string OwnerName
        {
            get { return _ownerName; }
            set
            {
                if (_ownerName != value)
                {
                    NotifyPropertyChanging("OwnerName");
                    _ownerName = value;
                    NotifyPropertyChanged("OwnerName");
                }
            }
        }

        private string _adressName;

        [Column]
        [DataMember]
        public string AddressName
        {
            get { return _adressName; }
            set
            {
                if (_adressName != value)
                {
                    NotifyPropertyChanging("AddressName");
                    _adressName = value;
                    NotifyPropertyChanged("AddressName");
                }
            }
        }

        private int _phoneNumber;

        [Column]
        [DataMember]
        public int PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    NotifyPropertyChanging("PhoneNumber");
                    _phoneNumber = value;
                    NotifyPropertyChanged("PhoneNumber");
                }
            }
        }

        private int _price;

        [Column]
        [DataMember]
        public int Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    NotifyPropertyChanging("Price");
                    _price = value;
                    NotifyPropertyChanged("Price");
                }
            }
        }

        private int _heatingPrice;

        [Column]
        [DataMember]
        public int HeatingPrice
        {
            get { return _heatingPrice; }
            set
            {
                if (_heatingPrice != value)
                {
                    NotifyPropertyChanging("HeatingPrice");
                    _heatingPrice = value;
                    NotifyPropertyChanged("HeatingPrice");
                }
            }
        }

        private int _land;

        [Column]
        [DataMember]
        public int Land
        {
            get { return _land; }
            set
            {
                if (_land != value)
                {
                    NotifyPropertyChanging("Land");
                    _land = value;
                    NotifyPropertyChanged("Land");
                }
            }
        }
     
        private double _longitude;

        [Column]
        [DataMember]
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                if (_longitude != value)
                {
                    NotifyPropertyChanging("Longitude");
                    _longitude = value;
                    NotifyPropertyChanged("Longitude");
                }
            }
        }

        private double _latitude;

        [Column]
        [DataMember]
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (_latitude != value)
                {
                    NotifyPropertyChanging("Latitude");
                    _latitude = value;
                    NotifyPropertyChanged("Latitude");
                }
            }
        }

        public GeoCoordinate Coordinate
        {
            get
            {
                return new GeoCoordinate(Latitude, Longitude);
            }
        }

        #endregion real estate

        #region Checklist

        private int _constructionYear;

        [Column]
        [DataMember]
        public int ConstructionYear
        {
            get { return _constructionYear; }
            set
            {
                if (_constructionYear != value)
                {
                    NotifyPropertyChanging("ConstructionYear");
                    _constructionYear = value;
                    NotifyPropertyChanged("ConstructionYear");
                }
            }
        }

        private string _notes;

        [Column]
        [DataMember]
        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    NotifyPropertyChanging("Notes");
                    _notes = value;
                    NotifyPropertyChanged("Notes");
                }
            }
        }

        private EntitySet<PictureTour> _pictures = new EntitySet<PictureTour>();

        [Association(Storage = "_pictures", OtherKey = "_tourId", ThisKey = "Id")]
        [DataMember]
        public EntitySet<PictureTour> Pictures
        {
            get
            {
                if (_pictures == null)
                    _pictures = new EntitySet<PictureTour>();
                return _pictures;
            }
            set
            {
                if (_pictures == null)
                {
                    _pictures = new EntitySet<PictureTour>();
                }

                if (_pictures != value)
                {
                    NotifyPropertyChanging("Pictures");
                    _pictures.Assign(value);
                    NotifyPropertyChanged("Pictures");
                }
            }
        }


        private EntitySet<Repair> _repair = new EntitySet<Repair>();

        [Association(Storage = "_repair", OtherKey = "_tourId", ThisKey = "Id")]
        [DataMember]
        public EntitySet<Repair> Repair
        {
            get
            {
                if (_repair == null)
                    _repair = new EntitySet<Repair>();
                return _repair;
            }
            set
            {
                if (_repair == null)
                {
                    _repair = new EntitySet<Repair>();
                }

                if (_repair != value)
                {
                    NotifyPropertyChanging("Repair");
                    _repair.Assign(value);
                    NotifyPropertyChanged("Repair");
                }
            }
        }

        private EntitySet<NearBy> _nearBy = new EntitySet<NearBy>();

        [Association(Storage = "_nearBy", OtherKey = "_tourId", ThisKey = "Id")]
        [DataMember]
        public EntitySet<NearBy> NearBy
        {
            get
            {
                if (_nearBy == null)
                    _nearBy = new EntitySet<NearBy>();
                return _nearBy;
            }
            set
            {
                if (_nearBy == null)
                {
                    _nearBy = new EntitySet<NearBy>();
                }

                if (_nearBy != value)
                {
                    NotifyPropertyChanging("NearBy");
                    _nearBy.Assign(value);
                    NotifyPropertyChanged("NearBy");
                }
            }
        }


        private double _neighborhoodQuality;

        [Column]
        [DataMember]
        public double NeighborhoodQuality
        {
            get { return _neighborhoodQuality; }
            set
            {
                if (_neighborhoodQuality != value)
                {
                    NotifyPropertyChanging("NeighborhoodQuality");
                    _neighborhoodQuality = value;
                    NotifyPropertyChanged("NeighborhoodQuality");
                }
            }
        }


        private string _neighborhoodNotes;

        [Column]
        [DataMember]
        public string NeighborhoodNotes
        {
            get { return _neighborhoodNotes; }
            set
            {
                if (_neighborhoodNotes != value)
                {
                    NotifyPropertyChanging("NeighborhoodNotes");
                    _neighborhoodNotes = value;
                    NotifyPropertyChanged("NeighborhoodNotes");
                }
            }
        }

        private double _ambience;

        [Column]
        [DataMember]
        public double Ambience
        {
            get { return _ambience; }
            set
            {
                if (_ambience != value)
                {
                    NotifyPropertyChanging("Ambience");
                    _ambience = value;
                    NotifyPropertyChanged("Ambience");
                }
            }
        }


        private string _ambienceNotes;

        [Column]
        [DataMember]
        public string AmbienceNotes
        {
            get { return _ambienceNotes; }
            set
            {
                if (_ambienceNotes != value)
                {
                    NotifyPropertyChanging("AmbienceNotes");
                    _ambienceNotes = value;
                    NotifyPropertyChanged("AmbienceNotes");
                }
            }
        }

        private string _recentRenovations;

        [Column]
        [DataMember]
        public string RecentRenovations
        {
            get { return _recentRenovations; }
            set
            {
                if (_recentRenovations != value)
                {
                    NotifyPropertyChanging("RecentRenovations");
                    _recentRenovations = value;
                    NotifyPropertyChanged("RecentRenovations");
                }
            }
        }
       

        #endregion


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
