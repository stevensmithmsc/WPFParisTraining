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

        private IEnumerable<Sess> _sessionList;
        public IEnumerable<Sess> SessionList { get { return _sessionList; } set { _sessionList = value; NotifyPropertyChanged(); } }

        private IEnumerable<Req> _staffList;
        public IEnumerable<Req> StaffList { get { return _staffList; } set { _staffList = value; NotifyPropertyChanged(); } }

        public IEnumerable<Staff> Trainers { get; private set; }
        public IEnumerable<Location> Locations { get; private set; }
        public IEnumerable<Status> RequirementStatuses { get; private set; }

        //Search Fields
        private string _searchName;
        private bool? _searchParis;
        private bool? _searchExternal;
        private bool? _searchObselete;
        public string SearchName { get { return _searchName; } set { _searchName = value; NotifyPropertyChanged(); } }
        public bool? SearchParis { get { return _searchParis; } set { _searchParis = value; NotifyPropertyChanged(); } }
        public bool? SearchExternal { get { return _searchExternal; } set { _searchExternal = value;  NotifyPropertyChanged(); } }
        public bool? SearchObselete { get { return _searchObselete; } set { _searchObselete = value;  NotifyPropertyChanged(); } }

        public ICommand SearchCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }

        public CourseViewModel()
        {
            db = new StaffEntities();
            db.Courses.Where(c => c.External == false && c.Obselete == false).OrderBy(c => c.CourseName).Load();
            CourseList = db.Courses.Local.ToList();
            SelectedCourse = CourseList.First();
            db.Staffs.Where(s => s.Trainer == true).Load();
            Trainers = db.Staffs.Local.Where(s => s.Trainer == true && s.External == false).OrderBy(s => s.Sname).ToList();
            NotifyPropertyChanged("Trainers");
            db.Locations.Where(l => l.TLoc).Load();
            Locations = db.Locations.Local.OrderBy(l => l.LocationName).ToList();
            NotifyPropertyChanged("Locations");
            db.Statuses.Where(s => s.Requirement).Load();
            RequirementStatuses = db.Statuses.Local.ToList();
            NotifyPropertyChanged("RequirementStatuses");

            ResetSearch(null);

            SearchCommand = new DelegateCommand<object>(Search);
            ResetCommand = new DelegateCommand<object>(ResetSearch);
            AddCommand = new DelegateCommand<object>(AddCourse);
            RemoveCommand = new DelegateCommand<object>(RemoveCourse);
        }

        private void UpdateLinked()
        {
            if (SelectedCourse != null)
            {
                db.Sesses.Where(s => s.CourseID == SelectedCourse.ID).Load();
                SessionList = db.Sesses.Local.Where(s => s.CourseID == SelectedCourse.ID).OrderBy(s => s.Strt).ToList();
                db.Reqs.Where(r => r.CourseID == SelectedCourse.ID && (r.StatusID == 1 || r.StatusID == 2)).Include("Staff").Load();
                StaffList = db.Reqs.Local.Where(r => r.CourseID == SelectedCourse.ID && (r.StatusID == 1 || r.StatusID == 2)).OrderBy(r => r.StatusID).ThenBy(r => r.Staff.Sname).ToList();
            }
            NotifyPropertyChanged("Changed");
        }

        private void Search(object parameter)
        {
            CourseList = db.search_course(SearchName, SearchParis, SearchExternal, SearchObselete).OrderBy(c => c.CourseName).ToList();
            SelectedCourse = CourseList.First();
        }

        private void ResetSearch(object parameter)
        {
            SearchName = null;
            SearchParis = null;
            SearchExternal = false;
            SearchObselete = false;
        }

        private void AddCourse(object parameter)
        {
            _addMode = true;
            Course newcourse = new Course();
            newcourse.External = false;
            newcourse.Obselete = false;
            newcourse.Paris = true;
            newcourse.Template = 1;

            db.Courses.Add(newcourse);
            CourseList = db.Courses.Local.Where(c => c.ID <= 0).ToList();
            SelectedCourse = newcourse;

            _addMode = false;
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
            }
        }
    }
}
