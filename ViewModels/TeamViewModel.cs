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
    class TeamViewModel : ViewModel
    {
        private StaffEntities db;

        private ObservableCollection<Team> _teamList;
        public ObservableCollection<Team> TeamList { get { return _teamList; } set { _teamList = value; NotifyPropertyChanged(); } }

        private Team _selectedTeam;
        public Team SelectedTeam { get { return _selectedTeam; } set { _selectedTeam = value; NotifyPropertyChanged(); UpdateLinkedStuff(); } }

        public IEnumerable<Cohort> CohortList { get; private set; }

        public IEnumerable<Cost_Centres> CostCentres { get; private set; }

        public IEnumerable<Staff> Members { get; private set; }

        public TeamViewModel()
        {
            db = new StaffEntities();
            db.Teams.Load();
            TeamList = db.Teams.Local;
            SelectedTeam = TeamList.First();
            db.Cohorts.Load();
            CohortList = db.Cohorts.Local.OrderBy(c => c.Number).ToList();
            NotifyPropertyChanged("CohortList");
            db.Cost_Centres.Where(cc => cc.Enbld).Load();
            CostCentres = db.Cost_Centres.Local.ToList();
            NotifyPropertyChanged("CostCentres");
        }

        private void UpdateLinkedStuff()
        {
              //db.Staffs.Where(s => s.TeamMems.Where(m => m.TeamID == SelectedTeam.ID)).Load();
        }
    }
}
