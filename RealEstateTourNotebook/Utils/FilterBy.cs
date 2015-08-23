using RealEstateTourNotebook.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateTourNotebook.Utils
{
    public class FilterBy
    {
        private static FilterBy instance;

        private FilterBy()  {}

        public static FilterBy Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FilterBy();
                }
                return instance;
            }
        }


        public static EstateType CurrentEstate
        {
            get;
            set;
        }

        public static TourType CurrentTourType { get; set; }
    }
}
