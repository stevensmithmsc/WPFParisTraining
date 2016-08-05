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
    class SessionsViewModel : DBViewModel
    {
        private IEnumerable<Sess> _sessionList;
        public IEnumerable<Sess> SessionList { get { return _sessionList; } set { _sessionList = value; NotifyPropertyChanged(); } }

        private Sess _selectedSession;
        public Sess SelectedSession { get { return _selectedSession; } set { if (_selectedSession != null && _selectedSession.Endt == null && _selectedSession.Strt != null) { _selectedSession.Endt = ((DateTime)_selectedSession.Strt).AddMinutes((double)_selectedSession.Course.Length); } _selectedSession = value; NotifyPropertyChanged(); UpdateLinked(); } }

        public IEnumerable<Staff> Trainers { get; private set; }
        public IEnumerable<Course> Courses { get; private set; }
        public IEnumerable<Location> Locations { get; private set; }

        private IEnumerable<Staff> _staffRequiring;
        public IEnumerable<Staff> StaffRequiring { get { return _staffRequiring; } private set { _staffRequiring = value;  NotifyPropertyChanged(); } }
        private Staff _bookStaff;
        public Staff BookStaff { get { return _bookStaff; } set { _bookStaff = value;  NotifyPropertyChanged(); } }

        private IEnumerable<Attendance> _bookings;
        public IEnumerable<Attendance> Bookings { get { return _bookings; } set { _bookings = value;  NotifyPropertyChanged(); } }

        private string _nameFilter;
        public string NameFilter { get { return _nameFilter; } set { _nameFilter = value; NotifyPropertyChanged(); } }

        public IEnumerable<Status> Outcomes { get; private set; }

        //Search Fields
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
        public ICommand BookCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }

        //Control Display Settings
        private Visibility _addSessButtonVis;
        private Visibility _removeSessButtonVis;
        private Visibility _personSearchVis;

        public Visibility AddSessionButtonVis { get { return _addSessButtonVis; } set { if (value != _addSessButtonVis) { _addSessButtonVis = value; NotifyPropertyChanged(); } } }
        public Visibility RemoveSessionButtonVis { get { return _removeSessButtonVis; } set { if (value != _removeSessButtonVis) { _removeSessButtonVis = value; NotifyPropertyChanged(); } } }
        public Visibility PersonSearchVis { get { return _personSearchVis; } set { if (value != _personSearchVis) { _personSearchVis = value; NotifyPropertyChanged(); } } }

        protected override void LoadRefData()
        {
            db.Staffs.Where(s => s.Trainer == true).Load();
            Trainers = db.Staffs.Local.Where(s => s.Trainer == true && s.External == false).OrderBy(s => s.Sname).ToList();
            NotifyPropertyChanged("Trainers");
            db.Courses.Load();
            Courses = db.Courses.Local.Where(c => c.Obselete == false && c.External == false).OrderBy(c => c.CourseName).ToList();
            NotifyPropertyChanged("Courses");
            db.Locations.Where(l => l.TLoc).Load();
            Locations = db.Locations.Local.OrderBy(l => l.LocationName).ToList();
            NotifyPropertyChanged("Locations");
            db.Statuses.Where(s => s.Attendance).Load();
            Outcomes = db.Statuses.Local.ToList();
            NotifyPropertyChanged("Outcomes");
        }

        protected override void LoadInitalData()
        {
            db.Sesses.Where(s => s.Strt > DateTime.Now).OrderBy(s => s.Strt).Load();
            SessionList = db.Sesses.Local.ToList();

            ResetSearch(null);
        }

        protected override void AssignCommands()
        {
            SearchCommand = new DelegateCommand<object>(Search);
            ResetCommand = new DelegateCommand<object>(ResetSearch);
            BookCommand = new DelegateCommand<object>(MakeBooking);
            FilterCommand = new DelegateCommand<object>(FilterStaffList);
            AddCommand = new DelegateCommand<object>(AddSession);
            RemoveCommand = new DelegateCommand<object>(RemoveSession);
            SaveCommand = new DelegateCommand<object>(SaveSessionChanges);
        }

        protected override void InitalDisplayState()
        {
            AddSessionButtonVis = Visibility.Visible;
            RemoveSessionButtonVis = Visibility.Visible;
            PersonSearchVis = Visibility.Visible;
        }

        private void UpdateLinked()
        {
            if (SelectedSession != null)
            {
                db.Attendances.Where(a => a.SessID == SelectedSession.ID).Load();
                Bookings = db.Attendances.Local.Where(a => a.SessID == SelectedSession.ID).OrderBy(a => a.Created).ToList();
                db.Reqs.Where(r => r.CourseID == SelectedSession.CourseID && r.StatusID == 1).Include("Staff").Load();
                StaffRequiring = db.Reqs.Local.Where(r => r.CourseID == SelectedSession.CourseID && r.StatusID == 1).Select(r => r.Staff).OrderBy(s => s.Sname).ThenBy(s => s.Fname).ToList();
                BookStaff = null;
                NameFilter = null;
            }
            NotifyPropertyChanged("Changed");
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

        private void MakeBooking(object parameter)
        {
            if (BookStaff != null)
            {
                if (SelectedSession.AvailablePlaces > 0 || MessageBox.Show("This course session is already full, do you want to overbook?", "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Attendance booking = new Attendance();
                    booking.StaffID = BookStaff.ID;
                    booking.SessID = SelectedSession.ID;
                    booking.Outcome = 0;
                    db.Attendances.Add(booking);
                    Req requirement = db.Reqs.Local.FirstOrDefault(r => r.StaffID == BookStaff.ID && r.CourseID == SelectedSession.CourseID);
                    if (requirement != null) requirement.StatusID = 2;
                    Bookings = db.Attendances.Local.Where(a => a.SessID == SelectedSession.ID).OrderBy(a => a.Created).ToList();
                    StaffRequiring = db.Reqs.Local.Where(r => r.CourseID == SelectedSession.CourseID && r.StatusID == 1).Select(r => r.Staff).OrderBy(s => s.Sname).ThenBy(s => s.Fname).ToList();
                    BookStaff = null;
                    NameFilter = null;
                    NotifyPropertyChanged("Changed");
                }
            }
        }

        private void FilterStaffList(object parameter)
        {
            if (NameFilter != null)
            {
                IEnumerable<Staff> AllStaff = db.Reqs.Local.Where(r => r.CourseID == SelectedSession.CourseID && r.StatusID == 1).Select(r => r.Staff);
                StaffRequiring = AllStaff.Where(s => s.Sname.Contains(NameFilter) || s.Fname.Contains(NameFilter) || (s.PName != null && s.PName.Contains(NameFilter)) || (s.FullName.Contains(NameFilter))).OrderBy(s => s.Sname).ThenBy(s => s.Fname).ToList();
                if (StaffRequiring.Count() == 1)
                {
                    BookStaff = StaffRequiring.First();
                }
                else if (StaffRequiring.Count() == 0)
                {
                    MessageBox.Show("No matches found!", "Training Database");
                    StaffRequiring = AllStaff.OrderBy(s => s.Sname).ThenBy(s => s.Fname).ToList();
                    BookStaff = null;
                    NameFilter = null;
                }
                else
                {
                    BookStaff = null;
                }
            }
            
        }

        private void AddSession(object parameter)
        {
            BeginAddMode();
            Sess newsess = new Sess();

            db.Sesses.Add(newsess);
            SessionList = db.Sesses.Local.Where(s => s.ID <= 0).ToList();
            SelectedSession = newsess;

        }

        private void RemoveSession(object parameter)
        {
            if (SelectedSession != null && (MessageBox.Show("Are you sure you want to delete " + SelectedSession.Course.CourseName + " on " + ((DateTime)SelectedSession.Strt).ToShortDateString(), "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                IEnumerable<int> idlist = SessionList.Select(s => s.ID);
                db.Sesses.Remove(SelectedSession);
                //SaveDataChanges(null);
                SessionList = db.Sesses.Local.Where(s => idlist.Contains(s.ID)).OrderBy(s => s.Strt).ToList();
                SelectedSession = SessionList.FirstOrDefault();
                NotifyPropertyChanged("Changed");
            }
        }

        private void BeginAddMode()
        {
            _addMode = true;
            AddSessionButtonVis = Visibility.Hidden;
            RemoveSessionButtonVis = Visibility.Hidden;
            PersonSearchVis = Visibility.Collapsed;
        }

        private void SaveSessionChanges(object Parameter)
        {
            if (SelectedSession != null && SelectedSession.Endt == null && SelectedSession.Strt != null) { SelectedSession.Endt = ((DateTime)SelectedSession.Strt).AddMinutes((double)SelectedSession.Course.Length); }
            if (SelectedSession != null && SelectedSession.ID <= 0 && SelectedSession.MaxP == 0 && SelectedSession.Location != null) { SelectedSession.MaxP = (short)SelectedSession.Location.MaxP; }
            SaveDataChanges(Parameter);
            if (_addMode)
            {
                InitalDisplayState();
                _addMode = false;
            }
        }
    }
}
