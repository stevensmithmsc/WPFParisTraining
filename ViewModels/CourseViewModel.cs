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
    class CourseViewModel : ViewModel
    {
        private StaffEntities db;

        private ObservableCollection<Course> _courseList;
        public ObservableCollection<Course> CourseList { get { return _courseList; } set { _courseList = value; NotifyPropertyChanged(); } }

        private Course _selectedCourse;
        public Course SelectedCourse { get { return _selectedCourse; } set { _selectedCourse = value; NotifyPropertyChanged(); } }

        public CourseViewModel()
        {
            db = new StaffEntities();
            db.Courses.Where(c => c.External == false && c.Obselete == false).OrderBy(c => c.CourseName).Load();
            CourseList = db.Courses.Local;
            
        }
    }
}
