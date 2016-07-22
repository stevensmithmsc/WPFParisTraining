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
    class TeamViewModel : DBViewModel
    {
        private IEnumerable<Team> _teamList;
        public IEnumerable<Team> TeamList { get { return _teamList; } set { _teamList = value; NotifyPropertyChanged(); } }

        private Team _selectedTeam;
        public Team SelectedTeam { get { return _selectedTeam; } set { _selectedTeam = value; NotifyPropertyChanged(); UpdateLinkedStuff(); } }

        private IEnumerable<TeamMem> _membership;
        public IEnumerable<TeamMem> Membership { get { return _membership; } set { _membership = value; NotifyPropertyChanged(); } }

        public IEnumerable<Cohort> CohortList { get; private set; }
        public IEnumerable<Cost_Centres> CostCentres { get; private set; }
        public IEnumerable<Staff> Members { get; private set; }

        //Search Fields
        private string _searchName;
        private int? _searchMHC;
        private string _searchBorough;
        private int? _searchService;
        private int? _searchLeader;
        private Cohort _searchCohort;
        private bool? _searchNoTrain;
        private bool? _searchHasMem;

        public string SearchName { get { return _searchName; } set { _searchName = value; NotifyPropertyChanged(); } }
        public int? SearchMHC {  get { return _searchMHC; } set { _searchMHC = value; NotifyPropertyChanged(); } }
        public string SearchBorough { get { return _searchBorough; } set { _searchBorough = value;  NotifyPropertyChanged(); } }
        public int? SearchService { get { return _searchService; } set { _searchService = value;  NotifyPropertyChanged(); } }
        public int? SearchLeader { get { return _searchLeader; } set { _searchLeader = value;  NotifyPropertyChanged(); } }
        public Cohort SearchCohort { get { return _searchCohort; } set { _searchCohort = value; NotifyPropertyChanged(); } }
        public bool? SearchNoTrain { get { return _searchNoTrain; } set { _searchNoTrain = value; NotifyPropertyChanged(); } }
        public bool? SearchHasMem { get { return _searchHasMem; } set { _searchHasMem = value;  NotifyPropertyChanged(); } }

        public ICommand SearchCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }

        public TeamViewModel()
        {
            db = new StaffEntities();

            db.Cohorts.Load();
            CohortList = db.Cohorts.Local.OrderBy(c => c.Number).ToList();
            NotifyPropertyChanged("CohortList");
            db.Cost_Centres.Where(cc => cc.Enbld).Load();
            CostCentres = db.Cost_Centres.Local.ToList();
            NotifyPropertyChanged("CostCentres");

            db.Teams.Take(50).Load();
            TeamList = db.Teams.Local.OrderBy(t => t.TeamName).ToList();
            SelectedTeam = TeamList.FirstOrDefault();

            ResetSearch(null);

            SearchCommand = new DelegateCommand<object>(Search);
            ResetCommand = new DelegateCommand<object>(ResetSearch);
            AddCommand = new DelegateCommand<object>(AddTeam);
            RemoveCommand = new DelegateCommand<object>(RemoveTeam);
        }

        private void UpdateLinkedStuff()
        {
            if (SelectedTeam != null)
            {
                db.TeamMems.Where(m => m.TeamID == SelectedTeam.ID).Include("Staff").Load();
                Membership = db.TeamMems.Local.Where(m => m.TeamID == SelectedTeam.ID).OrderBy(m => m.Staff.Sname).ThenBy(m => m.Staff.Fname).ToList();
                Members = db.TeamMems.Local.Where(m => m.TeamID == SelectedTeam.ID && m.Active).Select(m => m.Staff).OrderBy(s => s.Sname).ThenBy(s => s.Fname).ToList();
                NotifyPropertyChanged("Members");
            }
            NotifyPropertyChanged("Changed");
        }

        private void Search(object parameter)
        {
            int? SearchCohortID = (SearchCohort == null) ? 0 : SearchCohort.ID;
            TeamList = db.search_team(SearchName, SearchMHC, SearchBorough, SearchService, SearchLeader, SearchCohortID, SearchNoTrain, SearchHasMem).OrderBy(t => t.TeamName).ToList();
            SelectedTeam = TeamList.FirstOrDefault();
        }

        private void ResetSearch(object parameter)
        {
            SearchName = null;
            SearchMHC = null;
            SearchBorough = null;
            SearchService = null;
            SearchLeader = null;
            SearchCohort = null;
            SearchNoTrain = null;
            SearchHasMem = true;
        }

        private void AddTeam(object parameter)
        {
            _addMode = true;
            Team newteam = new Team();
            newteam.NoTrain = false;
            newteam.Dont_Migrate = false;

            db.Teams.Add(newteam);
            TeamList = db.Teams.Local.Where(t => t.ID <= 0).ToList();
            SelectedTeam = newteam;

            _addMode = false;
        }

        private void RemoveTeam(object parameter)
        {
            if (SelectedTeam != null && (MessageBox.Show("Are you sure you want to delete " + SelectedTeam.TeamName, "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                IEnumerable<int> idlist = TeamList.Select(t => t.ID);
                db.Teams.Remove(SelectedTeam);
                //SaveDataChanges(null);
                TeamList = db.Teams.Local.Where(t => idlist.Contains(t.ID)).OrderBy(t => t.TeamName).ToList();
                SelectedTeam = TeamList.FirstOrDefault();
            }
        }
    }
}
