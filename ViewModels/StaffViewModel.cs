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
        public Staff SelectedStaff { get { return _selectedStaff; } set { _selectedStaff = value;  NotifyPropertyChanged(); UpdateLinkedStuff(); } }

        public IEnumerable<Title> Titles { get; private set; } 

        public IEnumerable<Genders> GenderList { get; private set; }

        private IEnumerable<Staff_List> _esr;
        public IEnumerable<Staff_List> ESR { get { return _esr; } private set { _esr = value; NotifyPropertyChanged(); } }

        public IEnumerable<Cohort> CohortList { get; private set; }

        public StaffViewModel()
        {
            db = new StaffEntities();
            db.Staffs.Load();
            StaffList = db.Staffs.Local;
            SelectedStaff = StaffList.First();
            db.Titles.Load();
            Titles = db.Titles.Local.ToList();
            NotifyPropertyChanged("Titles");
            GenderList = new List<Genders>(){ Genders.Male, Genders.Female, Genders.Not_Known };
            db.Cohorts.Load();
            CohortList = db.Cohorts.Local.OrderBy(c => c.Number).ToList();
            NotifyPropertyChanged("CohortList");
        }

        private void UpdateLinkedStuff()
        {
            db.Staff_List.Where(e => e.Employee_Number == SelectedStaff.ESRID).Load();
            ESR = db.Staff_List.Where(e => e.Employee_Number == SelectedStaff.ESRID).ToList();
        }

    }
}
