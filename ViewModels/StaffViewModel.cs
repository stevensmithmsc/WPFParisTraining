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
    class StaffViewModel : ViewModel
    {
        private StaffEntities db;

        private ObservableCollection<Staff> _staffList;
        public ObservableCollection<Staff> StaffList { get { return _staffList; } set { _staffList = value;  NotifyPropertyChanged(); } }

        private Staff _selectedStaff;
        public Staff SelectedStaff { get { return _selectedStaff; } set { _selectedStaff = value;  NotifyPropertyChanged(); } }

        public List<Title> Titles { get; private set; } 

        public StaffViewModel()
        {
            db = new StaffEntities();
            db.Staffs.Load();
            StaffList = db.Staffs.Local;
            SelectedStaff = StaffList.First();
            db.Titles.Load();
            Titles = db.Titles.Local.ToList();
            NotifyPropertyChanged("Titles");
        }
    }
}
