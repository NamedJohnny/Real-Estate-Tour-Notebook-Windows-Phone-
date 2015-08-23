using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Toolkit;
using RealEstateTourNotebook.Resources;
using RealEstateTourNotebook.ViewModel;
using RealEstateTourNotebook.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RealEstateTourNotebook.Utils
{
    public class Utility
    {
        public static string ImageSourceFromEnum(Enum type)
        {
            return "/Images/TourType/" + type.ToString().ToLower() + ".png";
        }
        public static string Display(Enum type)
        {
            return AppResources.ResourceManager.GetString(type.ToString(), AppResources.Culture);
        }

        public static BitmapImage ByteArrayToImage(byte[] imageByteArray, bool compress)
        {
            BitmapImage img = new BitmapImage();
            if (compress)
            {
                img.DecodePixelHeight = 100;
                img.DecodePixelType = DecodePixelType.Logical;
                img.CreateOptions = BitmapCreateOptions.DelayCreation;
            }
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.SetSource(memStream);
            }

            return img;
        }

        public static byte[] ConvertToBytes(BitmapImage bitmapImage)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                WriteableBitmap btmMap = new WriteableBitmap
                    (bitmapImage.PixelWidth, bitmapImage.PixelHeight);

                // write an image into the stream
                Extensions.SaveJpeg(btmMap, ms,
                    bitmapImage.PixelWidth, bitmapImage.PixelHeight, 0, 100);

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Transforme le stream d'une photo en tableau de byte
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            if (input == null)
                return null;

            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string Currency
        {
            get
            {
                return CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
            }
        }

        public static string UnitDistance
        {
            get
            {
                if (RegionInfo.CurrentRegion.IsMetric)
                {
                    return "km";
                }
                else
                {
                    return "miles";
                }
            }
        }

        private static string getRegion(MapAddress mapLocation)
        {
            string region = "";
            region = string.IsNullOrEmpty(region) ? mapLocation.Province : region;
            // region = string.IsNullOrEmpty(region) ? mapLocation.County : region;
            region = string.IsNullOrEmpty(region) ? mapLocation.State : region;
            region = string.IsNullOrEmpty(region) ? mapLocation.District : region;
            return region;
        }

        public static string getCompleteAddress(MapAddress mapLocation)
        {
            string houseNumber = mapLocation.HouseNumber;
            string street = mapLocation.Street;
            string Address = "";
            string region = getRegion(mapLocation);
            string city = mapLocation.City;
            string country = mapLocation.Country;

            if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(country))
                Address = "";
            else if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(city))
                Address = country;
            else if (string.IsNullOrEmpty(region) && string.IsNullOrEmpty(country))
                Address = city;
            else if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(country))
                Address = region;
            else if (string.IsNullOrEmpty(region))
                Address = string.Format("{0}, {1} ", city, country);
            else if (string.IsNullOrEmpty(city))
                Address = string.Format("{0}, {1} ", region, country);
            else if (string.IsNullOrEmpty(country))
                Address = string.Format("{0}, {1} ", city, region);
            else if (string.IsNullOrEmpty(houseNumber) && string.IsNullOrEmpty(street))
                Address = string.Format("{0}, {1}, {2} ", city, region, country);
            else if (string.IsNullOrEmpty(houseNumber))
                Address = string.Format("{0}, {1}, {2}, {3}", street, city, region, country);
            else if (string.IsNullOrEmpty(street))
                Address = string.Format("{0}, {1}, {2}, {3}", houseNumber, city, region, country);
            else
                Address = string.Format("{0}, {1}, {2}, {3}, {4}", houseNumber, street, city, region, country);

            return Address;
        }

        public static event EventHandler<QueryCompletedEventArgs<IList<MapLocation>>> QueryComplete;

        public static void AddToMap(Map myMap, GeoCoordinate coord, TextBox textBox, bool reverse)
        {
            Query<System.Collections.Generic.IList<MapLocation>> query = null;

            if (reverse)
            {
                query = new ReverseGeocodeQuery()
                {
                    GeoCoordinate = coord,
                };
            }
            else
            {
                query = new GeocodeQuery()
                {
                    GeoCoordinate = new GeoCoordinate(0, 0),
                    SearchTerm = textBox.Text
                };
            }
            query.QueryCompleted += query_QueryCompleted;
            query.QueryAsync();
        }

        static void query_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {


            if (QueryComplete != null)
            {
                QueryComplete(sender, e);
            }
        }

        public static List<string> StringToList(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return new List<string>();
            }
            return text.Split(';').ToList();
        }

        public static string ListToString(List<string> list)
        {
            list.RemoveAll(str => String.IsNullOrEmpty(str));

            if (list == null || list.Count ==0)
                return string.Empty;

            string value = null;
            foreach (string item in list)
            {
                if (list.IndexOf(item) == list.Count - 1)
                {
                    value += item;
                }
                else
                {
                    value += item + ";";
                }
            }

            return value;
        }

    }
}
