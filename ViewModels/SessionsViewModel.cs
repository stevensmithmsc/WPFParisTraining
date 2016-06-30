using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFParisTraining.Entity;

namespace WPFParisTraining.ViewModels
{
    class SessionsViewModel : ViewModel
    {
        private StaffEntities db;

        private List<Sess> _sessionList;
        public List<Sess> SessionList { get { return _sessionList; } set { _sessionList = value; NotifyPropertyChanged(); } }

        private Sess _selectedSession;
        public Sess SelectedSession { get { return _selectedSession; } set { _selectedSession = value; NotifyPropertyChanged(); } }

        public IEnumerable<Staff> Trainers { get; private set; }

        public IEnumerable<Course> Courses { get; private set; }

        public IEnumerable<Location> Locations { get; private set; }

        private Course _searchCourse;
        private Staff _searchTrainer;
        private Location _searchLocation;
        private DateTime? _searchFrom;
        private DateTime? _searchTo;
        private bool? _searchExternal;
        private bool? _searchObselete;
        private bool? _searchParis;
        private bool? _searchAvailable;

        public Course SearchCourse { get { return _searchCourse; } set { _searchCourse = value; NotifyPropertyChanged(); } }
        public Staff SearchTrainer { get { return _searchTrainer; } set { _searchTrainer = value;  NotifyPropertyChanged(); } }
        public Location SearchLocation { get { return _searchLocation; }  set { _searchLocation = value;  NotifyPropertyChanged(); } }
        public DateTime? SearchFrom { get { return _searchFrom; } set { _searchFrom = value; NotifyPropertyChanged(); } }
        public DateTime? SearchTo { get { return _searchTo; } set { _searchTo = value; NotifyPropertyChanged(); } }
        public bool? SearchExternal { get { return _searchExternal; } set { _searchExternal = value;  NotifyPropertyChanged(); } }
        public bool? SearchObselete { get { return _searchObselete; } set { _searchObselete = value;  NotifyPropertyChanged(); } }
        public bool? SearchParis { get { return _searchParis; } set { _searchParis = value;  NotifyPropertyChanged(); } }
        public bool? SearchAvailable { get { return _searchAvailable; } set { _searchAvailable = value; NotifyPropertyChanged(); } }

        public ICommand SearchCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }

        public SessionsViewModel()
        {
            db = new StaffEntities();
            db.Sesses.Where(s => s.Strt > DateTime.Now).OrderBy(s => s.Strt).Load();
            SessionList = db.Sesses.Local.ToList();
            db.Staffs.Where(s => s.Trainer == true).Load();
            Trainers = db.Staffs.Local.Where(s => s.Trainer == true && s.External == false).OrderBy(s => s.Sname).ToList();
            NotifyPropertyChanged("Trainers");
            db.Courses.Load();
            Courses = db.Courses.Local.Where(c => c.Obselete == false && c.External == false).OrderBy(c => c.CourseName).ToList();
            NotifyPropertyChanged("Courses");
            db.Locations.Where(l => l.TLoc).Load();
            Locations = db.Locations.Local.OrderBy(l => l.LocationName).ToList();
            NotifyPropertyChanged("Locations");

            ResetSearch(null);

            SearchCommand = new DelegateCommand<object>(Search);
            ResetCommand = new DelegateCommand<object>(ResetSearch);
        }

        private void Search(object parameter)
        {
            int? SearchCourseID = (SearchCourse == null) ? 0 : SearchCourse.ID;
            int? SearchTrainerID = (SearchTrainer == null) ? 0 : SearchTrainer.ID;
            int? SearchLocationID = (SearchLocation == null) ? 0 : SearchLocation.ID;
            SessionList = db.search_session(SearchCourseID, SearchTrainerID, SearchLocationID, SearchFrom, SearchTo, SearchExternal, SearchObselete, SearchParis, SearchAvailable).ToList();
            SelectedSession = SessionList.FirstOrDefault();
        }

        private void ResetSearch(object parameter)
        {
            SearchCourse = null;
            SearchTrainer = null;
            SearchLocation = null;
            SearchFrom = null;
            SearchTo = null;
            SearchExternal = false;
            SearchObselete = null;
            SearchParis = null;
            SearchAvailable = null;
        }
    }
}
