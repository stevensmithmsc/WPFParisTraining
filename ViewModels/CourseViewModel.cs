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
    class CourseViewModel : ViewModel
    {
        private StaffEntities db;

        private List<Course> _courseList;
        public List<Course> CourseList { get { return _courseList; } set { _courseList = value; NotifyPropertyChanged(); } }

        private Course _selectedCourse;
        public Course SelectedCourse { get { return _selectedCourse; } set { _selectedCourse = value; NotifyPropertyChanged(); } }


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

            ResetSearch(null);

            SearchCommand = new DelegateCommand<object>(Search);
            ResetCommand = new DelegateCommand<object>(ResetSearch);
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
    }
}
