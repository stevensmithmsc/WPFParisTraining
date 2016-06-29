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
    class SessionsViewModel : ViewModel
    {
        private StaffEntities db;

        private ObservableCollection<Sess> _sessionList;
        public ObservableCollection<Sess> SessionList { get { return _sessionList; } set { _sessionList = value; NotifyPropertyChanged(); } }

        private Sess _selectedSession;
        public Sess SelectedSession { get { return _selectedSession; } set { _selectedSession = value; NotifyPropertyChanged(); } }

        public IEnumerable<Staff> Trainers { get; private set; }

        public IEnumerable<Course> Courses { get; private set; }

        public IEnumerable<Location> Locations { get; private set; }

        public SessionsViewModel()
        {
            db = new StaffEntities();
            db.Sesses.Where(s => s.Strt > DateTime.Now).OrderBy(s => s.Strt).Load();
            SessionList = db.Sesses.Local;
            db.Staffs.Where(s => s.Trainer == true).Load();
            Trainers = db.Staffs.Local.Where(s => s.Trainer == true && s.External == false).OrderBy(s => s.Sname).ToList();
            NotifyPropertyChanged("Trainers");
            db.Courses.Load();
            Courses = db.Courses.Local.Where(c => c.Obselete == false && c.External == false).OrderBy(c => c.CourseName).ToList();
            NotifyPropertyChanged("Courses");
            db.Locations.Where(l => l.TLoc).Load();
            Locations = db.Locations.Local.OrderBy(l => l.LocationName).ToList();
            NotifyPropertyChanged("Locations");
        }
    }
}
