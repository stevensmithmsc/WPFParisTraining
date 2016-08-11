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
    class CourseViewModel : DBViewModel
    {
        private IEnumerable<Course> _courseList;
        public IEnumerable<Course> CourseList { get { return _courseList; } set { _courseList = value; NotifyPropertyChanged(); } }

        private Course _selectedCourse;
        public Course SelectedCourse { get { return _selectedCourse; } set { _selectedCourse = value; NotifyPropertyChanged(); UpdateLinked(); } }

        private IEnumerable<CourseReq> _preReqs;
        public IEnumerable<CourseReq> PreReqs { get { return _preReqs; } set { _preReqs = value;  NotifyPropertyChanged(); } }

        private CourseReq _selectedPreReq;
        public CourseReq SelectedPreReq { get { return _selectedPreReq; } set { _selectedPreReq = value;  NotifyPropertyChanged(); NotifyPropertyChanged("Changed"); } }

        private Course _preReqToAdd;
        public Course PreReqToAdd { get { return _preReqToAdd; } set { _preReqToAdd = value; NotifyPropertyChanged(); } }

        private IEnumerable<Sess> _sessionList;
        public IEnumerable<Sess> SessionList { get { return _sessionList; } set { _sessionList = value; NotifyPropertyChanged(); } }

        private Sess _selectedSession;
        public Sess SelectedSession { get { return _selectedSession; }
            set { if (_selectedSession!= null && _selectedSession.Endt == null && _selectedSession.Strt != null) { _selectedSession.Endt = ((DateTime)_selectedSession.Strt).AddMinutes((double)_selectedSession.Course.Length); }
                if (_selectedSession != null && _selectedSession.ID <= 0 && _selectedSession.MaxP == 0 && _selectedSession.Location != null) { _selectedSession.MaxP = (short)_selectedSession.Location.MaxP; }
                _selectedSession = value;  NotifyPropertyChanged(); NotifyPropertyChanged("Changed"); } }

        private IEnumerable<Req> _staffList;
        public IEnumerable<Req> StaffList { get { return _staffList; } set { _staffList = value; NotifyPropertyChanged(); } }

        public IEnumerable<Staff> Trainers { get; private set; }
        public IEnumerable<Location> Locations { get; private set; }
        public IEnumerable<Status> RequirementStatuses { get; private set; }
        public IEnumerable<Course> AllCourses { get; private set; }
        public IEnumerable<mail_temp> Templates { get; private set; }

        //Search Fields
        private string _searchName;
        private bool? _searchParis;
        private bool? _searchCH;
        private bool? _searchExternal;
        private bool? _searchObselete;
        public string SearchName { get { return _searchName; } set { _searchName = value; NotifyPropertyChanged(); } }
        public bool? SearchParis { get { return _searchParis; } set { _searchParis = value; NotifyPropertyChanged(); } }
        public bool? SearchChildHealth { get { return _searchCH; } set { _searchCH = value; NotifyPropertyChanged(); } }
        public bool? SearchExternal { get { return _searchExternal; } set { _searchExternal = value;  NotifyPropertyChanged(); } }
        public bool? SearchObselete { get { return _searchObselete; } set { _searchObselete = value;  NotifyPropertyChanged(); } }

        public ICommand SearchCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand AddPreReqCommand { get; private set; }
        public ICommand RemovePreReqCommand { get; private set; }
        public ICommand AddSessCommand { get; private set; }
        public ICommand RemoveSessCommand { get; private set; }

        //Control Display Settings
        private Visibility _addCourseButtonVis;
        private Visibility _removeCourseButtonVis;
        private Visibility _addPreReqButtonVis;
        private Visibility _removePreReqButtonVis;
        private Visibility _addSessButtonVis;
        private Visibility _removeSessButtonVis;

        public Visibility AddCourseButtonVis { get { return _addCourseButtonVis; } set { if (value != _addCourseButtonVis) { _addCourseButtonVis = value; NotifyPropertyChanged(); } } }
        public Visibility RemoveCourseButtonVis { get { return _removeCourseButtonVis; } set { if (value != _removeCourseButtonVis) { _removeCourseButtonVis = value; NotifyPropertyChanged(); } } }
        public Visibility AddPreReqButtonVis { get { return _addPreReqButtonVis; } set { if (value != _addPreReqButtonVis) { _addPreReqButtonVis = value; NotifyPropertyChanged(); } } }
        public Visibility RemovePreReqButtonVis { get { return _removePreReqButtonVis; } set { if (value != _removePreReqButtonVis) { _removePreReqButtonVis = value; NotifyPropertyChanged(); } } }
        public Visibility AddSessButtonVis { get { return _addSessButtonVis; } set { if (value != _addSessButtonVis) { _addSessButtonVis = value; NotifyPropertyChanged(); } } }
        public Visibility RemoveSessButtonVis { get { return _removeSessButtonVis; } set { if (value != _removeSessButtonVis) { _removeSessButtonVis = value; NotifyPropertyChanged(); } } }


        protected override void LoadRefData()
        {
            db.Staffs.Where(s => s.Trainer == true).Load();
            Trainers = db.Staffs.Local.Where(s => s.Trainer == true && s.External == false).OrderBy(s => s.Sname).ToList();
            NotifyPropertyChanged("Trainers");
            db.Locations.Where(l => l.TLoc).Load();
            Locations = db.Locations.Local.OrderBy(l => l.LocationName).ToList();
            NotifyPropertyChanged("Locations");
            db.Statuses.Where(s => s.Requirement).Load();
            RequirementStatuses = db.Statuses.Local.ToList();
            NotifyPropertyChanged("RequirementStatuses");
            db.Courses.Load();
            AllCourses = db.Courses.Where(c => c.External == false && c.Obselete == false).OrderBy(c => c.CourseName).ToList();
            NotifyPropertyChanged("AllCourses");
            Templates = db.mail_temp.ToList();
            NotifyPropertyChanged("Templates");
        }

        protected override void LoadInitalData()
        {
            db.Courses.Where(c => c.External == false && c.Obselete == false).OrderBy(c => c.CourseName).Load();
            CourseList = db.Courses.Local.Where(c => c.External == false && c.Obselete == false).OrderBy(c => c.CourseName).ToList();
            SelectedCourse = CourseList.First();

            ResetSearch(null);
        }

        protected override void AssignCommands()
        {
            SearchCommand = new DelegateCommand<object>(Search);
            ResetCommand = new DelegateCommand<object>(ResetSearch);
            AddCommand = new DelegateCommand<object>(AddCourse);
            RemoveCommand = new DelegateCommand<object>(RemoveCourse);
            AddPreReqCommand = new DelegateCommand<object>(AddPreReq);
            RemovePreReqCommand = new DelegateCommand<object>(RemovePreReq);
            AddSessCommand = new DelegateCommand<object>(AddSession);
            RemoveSessCommand = new DelegateCommand<object>(RemoveSession);
            SaveCommand = new DelegateCommand<object>(SaveCourseChanges);
        }

        protected override void InitalDisplayState()
        {
            AddCourseButtonVis = Visibility.Visible;
            RemoveCourseButtonVis = Visibility.Visible;
            AddPreReqButtonVis = Visibility.Visible;
            RemovePreReqButtonVis = Visibility.Visible;
            AddSessButtonVis = Visibility.Visible;
            RemoveSessButtonVis = Visibility.Visible;
        }

        private void UpdateLinked()
        {
            if (SelectedCourse != null)
            {
                db.CourseReqs.Where(r => r.CourseID == SelectedCourse.ID).Load();
                PreReqs = db.CourseReqs.Local.Where(p => p.CourseID == SelectedCourse.ID).OrderBy(p => p.PreReq.CourseName).ToList();
                db.Sesses.Where(s => s.CourseID == SelectedCourse.ID).Load();
                SessionList = db.Sesses.Local.Where(s => s.CourseID == SelectedCourse.ID).OrderBy(s => s.Strt).ToList();
                db.Reqs.Where(r => r.CourseID == SelectedCourse.ID && (r.StatusID == 1 || r.StatusID == 2)).Include("Staff").Load();
                StaffList = db.Reqs.Local.Where(r => r.CourseID == SelectedCourse.ID && (r.StatusID == 1 || r.StatusID == 2)).OrderBy(r => r.StatusID).ThenBy(r => r.Staff.Sname).ToList();
            }
            NotifyPropertyChanged("Changed");
        }

        private void Search(object parameter)
        {
            CourseList = db.search_course(SearchName, SearchParis, SearchChildHealth, SearchExternal, SearchObselete).OrderBy(c => c.CourseName).ToList();
            SelectedCourse = CourseList.First();
        }

        private void ResetSearch(object parameter)
        {
            SearchName = null;
            SearchParis = null;
            SearchChildHealth = null;
            SearchExternal = false;
            SearchObselete = false;
        }

        private void AddCourse(object parameter)
        {
            BeginAddMode();
            Course newcourse = new Course();
            newcourse.External = false;
            newcourse.Obselete = false;
            newcourse.Paris = true;
            newcourse.Template = 1;

            db.Courses.Add(newcourse);
            CourseList = db.Courses.Local.Where(c => c.ID <= 0).ToList();
            SelectedCourse = newcourse;
        }

        private void RemoveCourse(object parameter)
        {
            if (SelectedCourse != null && (MessageBox.Show("Are you sure you want to delete " + SelectedCourse.CourseName, "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                IEnumerable<int> idlist = CourseList.Select(t => t.ID);
                db.Courses.Remove(SelectedCourse);
                //SaveDataChanges(null);
                CourseList = db.Courses.Local.Where(c => idlist.Contains(c.ID)).OrderBy(c => c.CourseName).ToList();
                SelectedCourse = CourseList.FirstOrDefault();
                NotifyPropertyChanged("Changed");
            }
        }

        private void AddPreReq(object parameter)
        {
            if (PreReqToAdd != null)
            {
                if (PreReqToAdd == SelectedCourse)
                {
                    MessageBox.Show("You cannot add a course as it's own prereq!", "Training Database", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    CourseReq newreq = new CourseReq();
                    newreq.CourseID = SelectedCourse.ID;
                    newreq.PreReq = PreReqToAdd;
                    db.CourseReqs.Add(newreq);
                    PreReqs = db.CourseReqs.Local.Where(p => p.CourseID == SelectedCourse.ID).ToList();
                    SelectedPreReq = newreq;
                    NotifyPropertyChanged("Changed");
                }
            }
        }

        private void RemovePreReq(object parameter)
        {
            if (SelectedPreReq != null && MessageBox.Show("Are you sure you want to remove requirement for " + SelectedPreReq.PreReq.CourseName + " to be completed before " + SelectedPreReq.Course.CourseName + "?", "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.CourseReqs.Remove(SelectedPreReq);
                SelectedPreReq = null;
                PreReqs = db.CourseReqs.Local.Where(p => p.CourseID == SelectedCourse.ID).ToList();
                NotifyPropertyChanged("Changed");
            }
        }

        private void AddSession(object parameter)
        {
            Sess newsess = new Sess();
            newsess.CourseID = SelectedCourse.ID;
            db.Sesses.Add(newsess);
            SessionList = db.Sesses.Local.Where(s => s.CourseID == SelectedCourse.ID).OrderBy(s => s.Strt).ToList();
            SelectedSession = newsess;
            NotifyPropertyChanged("Changed");
        }

        private void RemoveSession(object parameter)
        {
            if (SelectedSession != null && MessageBox.Show("Are you sure you want to remove session on " + ((SelectedSession.Strt == null) ? "" : ((DateTime)SelectedSession.Strt).ToShortDateString()) + "?", "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.Sesses.Remove(SelectedSession);
                SelectedSession = null;
                SessionList = db.Sesses.Local.Where(s => s.CourseID == SelectedCourse.ID).OrderBy(s => s.Strt).ToList();
                NotifyPropertyChanged("Changed");
            }
        }

        private void BeginAddMode()
        {
            _addMode = true;
            AddCourseButtonVis = Visibility.Hidden;
            RemoveCourseButtonVis = Visibility.Hidden;
            AddPreReqButtonVis = Visibility.Hidden;
            RemovePreReqButtonVis = Visibility.Hidden;
            AddSessButtonVis = Visibility.Hidden;
            RemoveSessButtonVis = Visibility.Hidden;
        }

        private void SaveCourseChanges(object Parameter)
        {
            SaveDataChanges(Parameter);
            if (_addMode)
            {
                InitalDisplayState();
                _addMode = false;
            }
        }
    }
}
