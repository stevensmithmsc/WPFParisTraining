using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFParisTraining.Entity;

namespace WPFParisTraining.ViewModels
{
    class LocationsViewModel : ViewModel
    {
        private StaffEntities db;

        private ObservableCollection<Location> _locationList;
        public ObservableCollection<Location> LocationList { get { return _locationList; } set { _locationList = value; NotifyPropertyChanged(); } }


        public LocationsViewModel()
        {
            db = new StaffEntities();
            db.Locations.OrderBy(l => l.LocationName).Load();
            LocationList = db.Locations.Local;
        }
    }
}
