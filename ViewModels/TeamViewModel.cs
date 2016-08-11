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

        private IEnumerable<BoroughMem> _boromem;
        private IEnumerable<ServMem> _mhcmem;
        private IEnumerable<ServMem> _servmem;
        private BoroughMem _selectedBoro;
        private Borough _boroToAdd;
        private ServMem _selectedServ;
        private Service _serviceToAdd;
        private ServMem _selectedMHC;
        private Service _mhcToAdd;
        public IEnumerable<BoroughMem> BoroughMembership { get { return _boromem; } set { _boromem = value;  NotifyPropertyChanged(); } }
        public IEnumerable<ServMem> MHCMembership { get { return _mhcmem; } set { _mhcmem = value;  NotifyPropertyChanged(); } }
        public IEnumerable<ServMem> ServiceMembership { get { return _servmem; } set { _servmem = value;  NotifyPropertyChanged(); } }
        public BoroughMem SelectedBorough { get { return _selectedBoro; } set { _selectedBoro = value; NotifyPropertyChanged(); } }
        public Borough BoroughToAdd { get { return _boroToAdd; } set { _boroToAdd = value; NotifyPropertyChanged(); } }
        public ServMem SelectedService { get { return _selectedServ; } set { _selectedServ = value; NotifyPropertyChanged(); } }
        public Service ServiceToAdd { get { return _serviceToAdd; } set { _serviceToAdd = value; NotifyPropertyChanged(); } }
        public ServMem SelectedMHC { get { return _selectedMHC; } set { _selectedMHC = value; NotifyPropertyChanged(); } }
        public Service MHCToAdd { get { return _mhcToAdd; } set { _mhcToAdd = value; NotifyPropertyChanged(); } }

        public IEnumerable<Cohort> CohortList { get; private set; }
        public IEnumerable<Cost_Centres> CostCentres { get; private set; }
        public IEnumerable<Staff> Members { get; private set; }
        public IEnumerable<Borough> Boroughs { get; private set; }
        public IEnumerable<Service> Services { get; private set; }
        public IEnumerable<Service> MHCs { get; private set; }
        public IEnumerable<Staff> Leaders { get; private set; }

        //Search Fields
        private string _searchName;
        private Service _searchMHC;
        private Borough _searchBorough;
        private Service _searchService;
        private Staff _searchLeader;
        private Cohort _searchCohort;
        private bool? _searchNoTrain;
        private bool? _searchHasMem;

        public string SearchName { get { return _searchName; } set { _searchName = value; NotifyPropertyChanged(); } }
        public Service SearchMHC {  get { return _searchMHC; } set { _searchMHC = value; NotifyPropertyChanged(); } }
        public Borough SearchBorough { get { return _searchBorough; } set { _searchBorough = value;  NotifyPropertyChanged(); } }
        public Service SearchService { get { return _searchService; } set { _searchService = value;  NotifyPropertyChanged(); } }
        public Staff SearchLeader { get { return _searchLeader; } set { _searchLeader = value;  NotifyPropertyChanged(); } }
        public Cohort SearchCohort { get { return _searchCohort; } set { _searchCohort = value; NotifyPropertyChanged(); } }
        public bool? SearchNoTrain { get { return _searchNoTrain; } set { _searchNoTrain = value; NotifyPropertyChanged(); } }
        public bool? SearchHasMem { get { return _searchHasMem; } set { _searchHasMem = value;  NotifyPropertyChanged(); } }

        public ICommand SearchCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand AddBoroughCommand { get; private set; }
        public ICommand RemoveBoroughCommand { get; private set; }
        public ICommand AddServiceCommand { get; private set; }
        public ICommand AddMHCCommand { get; private set; }
        public ICommand RemoveServiceCommand { get; private set; }
        public ICommand RemoveMHCCommand { get; private set; }
        public ICommand SetPrimaryServiceCommand { get; private set; }

        //Control Display Settings
        private Visibility _addTeamButtonVis;
        private Visibility _removeTeamButtonVis;
        private bool _esrNameReadOnly;
        private Visibility _addServVis;
        private Visibility _remServVis;

        public Visibility AddTeamButtonVis { get { return _addTeamButtonVis; } set { if (value != _addTeamButtonVis) { _addTeamButtonVis = value; NotifyPropertyChanged(); } } }
        public Visibility RemoveTeamButtonVis { get { return _removeTeamButtonVis; } set { if (value != _removeTeamButtonVis) { _removeTeamButtonVis = value; NotifyPropertyChanged(); } } }
        public bool ESRNameReadOnly { get { return _esrNameReadOnly; } set { if (value != _esrNameReadOnly) { _esrNameReadOnly = value; NotifyPropertyChanged(); } } }
        public Visibility AddServicesVis { get { return _addServVis; } set { if (value != _addServVis) { _addServVis = value; NotifyPropertyChanged(); } } }
        public Visibility RemoveServicesVis { get { return _remServVis; } set { if (value != _remServVis) { _remServVis = value; NotifyPropertyChanged(); } } }

        protected override void LoadRefData()
        {
            db.Cohorts.Load();
            CohortList = db.Cohorts.Local.OrderBy(c => c.Number).ToList();
            NotifyPropertyChanged("CohortList");
            db.Cost_Centres.Where(cc => cc.Enbld).Load();
            CostCentres = db.Cost_Centres.Local.OrderBy(c => c.CCName).ToList();
            NotifyPropertyChanged("CostCentres");
            db.Boroughs.Load();
            Boroughs = db.Boroughs.Local.OrderBy(b => b.BoroughName);
            NotifyPropertyChanged("Boroughs");
            db.Services.Where(s => (s.Level == 1 || s.Level == 5) && s.Display == true).Load();
            Services = db.Services.Local.Where(s => s.Level == 5 && s.Display == true).OrderBy(s => s.ServiceName).ToList();
            NotifyPropertyChanged("Services");
            MHCs = db.Services.Local.Where(s => s.Level == 1 && s.Display == true).OrderBy(s => s.ServiceName).ToList();
            NotifyPropertyChanged("MHCs");
            Leaders = db.Teams.Where(t => t.LeaderID.HasValue).Select(t => t.Leader).OrderBy(s => s.Sname).ThenBy(s => s.Fname).ToList();
            NotifyPropertyChanged("Leaders");
        }

        protected override void LoadInitalData()
        {
            db.Teams.Take(50).Load();
            TeamList = db.Teams.Local.OrderBy(t => t.TeamName).ToList();
            SelectedTeam = TeamList.FirstOrDefault();

            ResetSearch(null);
        }

        protected override void AssignCommands()
        {
            SearchCommand = new DelegateCommand<object>(Search);
            ResetCommand = new DelegateCommand<object>(ResetSearch);
            AddCommand = new DelegateCommand<object>(AddTeam);
            RemoveCommand = new DelegateCommand<object>(RemoveTeam);
            SaveCommand = new DelegateCommand<object>(SaveTeamChanges);
            AddBoroughCommand = new DelegateCommand<object>(AddBorough);
            RemoveBoroughCommand = new DelegateCommand<object>(RemoveBorough);
            AddServiceCommand = new DelegateCommand<object>(AddService);
            AddMHCCommand = new DelegateCommand<object>(AddMHC);
            RemoveServiceCommand = new DelegateCommand<object>(RemoveService);
            RemoveMHCCommand = new DelegateCommand<object>(RemoveMHC);
            SetPrimaryServiceCommand = new DelegateCommand<object>(SetPrimaryService);
        }

        protected override void InitalDisplayState()
        {
            AddTeamButtonVis = Visibility.Visible;
            RemoveTeamButtonVis = Visibility.Visible;
            ESRNameReadOnly = false;
            AddServicesVis = Visibility.Visible;
            RemoveServicesVis = Visibility.Visible;
        }

        private void UpdateLinkedStuff()
        {
            if (SelectedTeam != null)
            {
                db.TeamMems.Where(m => m.TeamID == SelectedTeam.ID).Include("Staff").Load();
                Membership = db.TeamMems.Local.Where(m => m.TeamID == SelectedTeam.ID).OrderBy(m => m.Staff.Sname).ThenBy(m => m.Staff.Fname).ToList();
                Members = db.TeamMems.Local.Where(m => m.TeamID == SelectedTeam.ID && m.Active).Select(m => m.Staff).OrderBy(s => s.Sname).ThenBy(s => s.Fname).ToList();
                NotifyPropertyChanged("Members");
                db.BoroughMems.Where(b => b.ID == SelectedTeam.ID && b.Type == "T").Load();
                BoroughMembership = db.BoroughMems.Local.Where(b => b.ID == SelectedTeam.ID && b.Type == "T").OrderBy(b => b.Borough.BoroughName);
                db.ServMems.Where(s => s.ID == SelectedTeam.ID && s.Type == "T").Load();
                ServiceMembership = db.ServMems.Local.Where(s => s.ID == SelectedTeam.ID && s.Type == "T" && s.Main == false).OrderBy(s => s.Service.ServiceName).ToList();
                MHCMembership = db.ServMems.Local.Where(s => s.ID == SelectedTeam.ID && s.Type == "T" && s.Main == true).OrderBy(s => s.Service.ServiceName).ToList();
            }
            NotifyPropertyChanged("Changed");
        }

        private void Search(object parameter)
        {
            //Cannot get a ID from a null object so need to check if null first
            int? SearchMHCID = (SearchMHC == null) ? 0 : SearchMHC.ID;
            string SearchBoroID = (SearchBorough == null) ? null : SearchBorough.ID;
            int? SearchServID = (SearchService == null) ? 0 : SearchService.ID;
            int? SearchCohortID = (SearchCohort == null) ? 0 : SearchCohort.ID;
            int? SearchLeaderID = (SearchLeader == null) ? 0 : SearchLeader.ID;

            TeamList = db.search_team(SearchName, SearchMHCID, SearchBoroID, SearchServID, SearchLeaderID, SearchCohortID, SearchNoTrain, SearchHasMem).OrderBy(t => t.TeamName).ToList();
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
            BeginAddMode();
            Team newteam = new Team();
            newteam.NoTrain = false;
            newteam.Dont_Migrate = false;

            db.Teams.Add(newteam);
            TeamList = db.Teams.Local.Where(t => t.ID <= 0).ToList();
            SelectedTeam = newteam;
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
                NotifyPropertyChanged("Changed");
            }
        }

        private void BeginAddMode()
        {
            _addMode = true;
            AddTeamButtonVis = Visibility.Hidden;
            RemoveTeamButtonVis = Visibility.Hidden;
            AddServicesVis = Visibility.Collapsed;
            RemoveServicesVis = Visibility.Collapsed;
        }

        private void SaveTeamChanges(object Parameter)
        {
            SaveDataChanges(Parameter);
            if (_addMode)
            {
                InitalDisplayState();
                _addMode = false;
            }
        }

        private void AddBorough(object Parameter)
        {
            if (BoroughToAdd != null)
            {
                var existing = db.BoroughMems.Local.Where(b => b.ID == SelectedTeam.ID && b.Type == "T" && b.BoroughID == BoroughToAdd.ID);

                if (existing.Count() >= 1)
                {
                    MessageBox.Show("This team is already associated with " + BoroughToAdd.BoroughName, "Training Database", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    BoroughMem newboromem = new BoroughMem();
                    newboromem.Type = "T";
                    newboromem.ID = SelectedTeam.ID;
                    newboromem.Borough = BoroughToAdd;
                    db.BoroughMems.Add(newboromem);
                    BoroughMembership = db.BoroughMems.Local.Where(b => b.ID == SelectedTeam.ID && b.Type == "T").OrderBy(b => b.Borough.BoroughName).ToList();
                    SelectedBorough = newboromem;
                    NotifyPropertyChanged("Changed");
                }
            }
        }

        private void RemoveBorough(object Parameter)
        {
            if (SelectedBorough != null && (MessageBox.Show("Are you sure you want to delete association with " + SelectedBorough.Borough.BoroughName, "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                db.BoroughMems.Remove(SelectedBorough);
                BoroughMembership = db.BoroughMems.Local.Where(b => b.ID == SelectedTeam.ID && b.Type == "T").OrderBy(b => b.Borough.BoroughName).ToList();
                SelectedBorough = null;
                NotifyPropertyChanged("Changed");
            }
        }

        private void AddService(object Parameter)
        {
            if (ServiceToAdd != null)
            {
                var existing = db.ServMems.Local.Where(s => s.ID == SelectedTeam.ID && s.Type == "T" && s.ServID == ServiceToAdd.ID);

                if (existing.Count() >= 1)
                {
                    MessageBox.Show("This team is already associated with " + ServiceToAdd.ServiceName, "Training Database", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    ServMem newservmem = new ServMem();
                    newservmem.Type = "T";
                    newservmem.ID = SelectedTeam.ID;
                    newservmem.Service = ServiceToAdd;
                    newservmem.Main = false;
                    newservmem.Pri = false;
                    db.ServMems.Add(newservmem);
                    ServiceMembership = db.ServMems.Local.Where(s => s.ID == SelectedTeam.ID && s.Type == "T" && s.Main == false).OrderBy(s => s.Service.ServiceName).ToList();
                    SelectedService = newservmem;
                    NotifyPropertyChanged("Changed");
                }
            }
        }

        private void AddMHC(object Parameter)
        {
            if (MHCToAdd != null)
            {
                var existing = db.ServMems.Local.Where(s => s.ID == SelectedTeam.ID && s.Type == "T" && s.ServID == MHCToAdd.ID);

                if (existing.Count() >= 1)
                {
                    MessageBox.Show("This team is already associated with " + MHCToAdd.ServiceName, "Training Database", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    ServMem newservmem = new ServMem();
                    newservmem.Type = "T";
                    newservmem.ID = SelectedTeam.ID;
                    newservmem.Service = MHCToAdd;
                    newservmem.Main = true;
                    newservmem.Pri = false;
                    db.ServMems.Add(newservmem);
                    MHCMembership = db.ServMems.Local.Where(s => s.ID == SelectedTeam.ID && s.Type == "T" && s.Main == true).OrderBy(s => s.Service.ServiceName).ToList();
                    SelectedMHC = newservmem;
                    NotifyPropertyChanged("Changed");
                }
            }
        }

        private void RemoveService(object Parameter)
        {
            if (SelectedService != null && (MessageBox.Show("Are you sure you want to delete association with " + SelectedService.Service.ServiceName, "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                db.ServMems.Remove(SelectedService);
                ServiceMembership = db.ServMems.Local.Where(s => s.ID == SelectedTeam.ID && s.Type == "T" && s.Main == false).OrderBy(s => s.Service.ServiceName).ToList();
                SelectedService = null;
                NotifyPropertyChanged("Changed");
            }
        }

        private void RemoveMHC(object Parameter)
        {
            if (SelectedMHC != null && (MessageBox.Show("Are you sure you want to delete association with " + SelectedMHC.Service.ServiceName, "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                db.ServMems.Remove(SelectedMHC);
                MHCMembership = db.ServMems.Local.Where(s => s.ID == SelectedTeam.ID && s.Type == "T" && s.Main == true).OrderBy(s => s.Service.ServiceName).ToList();
                SelectedMHC = null;
                NotifyPropertyChanged("Changed");
            }
        }

        private void SetPrimaryService(object Parameter)
        {
            if (SelectedService != null)
            {
                foreach (ServMem s in ServiceMembership)
                {
                    if (s.Pri != (s == SelectedService)) s.Pri = (s == SelectedService);
                }
                NotifyPropertyChanged("Changed");
                ServiceMembership = db.ServMems.Local.Where(s => s.ID == SelectedTeam.ID && s.Type == "T" && s.Main == false).OrderBy(s => s.Service.ServiceName).ToList();
            }
        }
    }
}
