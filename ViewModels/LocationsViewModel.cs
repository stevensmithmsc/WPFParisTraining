using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFParisTraining.Entity;

namespace WPFParisTraining.ViewModels
{
    class LocationsViewModel : DBViewModel
    {
        private ObservableCollection<Location> _locationList;
        public ObservableCollection<Location> LocationList { get { return _locationList; } set { _locationList = value; NotifyPropertyChanged(); } }

        private Location _selectedLocation;
        public Location SelectedLocation { get { return _selectedLocation; } set { _selectedLocation = value; NotifyPropertyChanged(); NotifyPropertyChanged("Changed"); } }

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public LocationsViewModel()
        {
            db = new StaffEntities();
            db.Locations.OrderBy(l => l.LocationName).Load();
            LocationList = db.Locations.Local;
            SelectedLocation = LocationList.FirstOrDefault();

            AddCommand = new DelegateCommand<object>(AddLocation);
            RemoveCommand = new DelegateCommand<object>(RemoveLocation);
            SaveCommand = new DelegateCommand<object>(SaveDataChanges);
        }

        private void AddLocation(object parameter)
        {
            Location newLoc = new Location();
            LocationList.Add(newLoc);
            SelectedLocation = newLoc;
        }

        private void RemoveLocation(object parameter)
        {
            if (MessageBox.Show("Are you sure you want to delete " + SelectedLocation.LocationName, "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LocationList.Remove(SelectedLocation);
                SelectedLocation = LocationList.FirstOrDefault();
            }
        }
    }
}
