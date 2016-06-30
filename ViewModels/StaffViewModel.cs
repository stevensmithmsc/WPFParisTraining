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
    class StaffViewModel : ViewModel
    {
        private StaffEntities db;

        private List<Staff> _staffList;
        public List<Staff> StaffList { get { return _staffList; } set { _staffList = value;  NotifyPropertyChanged(); } }

        private Staff _selectedStaff;
        public Staff SelectedStaff { get { return _selectedStaff; } set { _selectedStaff = value;  NotifyPropertyChanged(); UpdateLinkedStuff(); } }

        public IEnumerable<Title> Titles { get; private set; } 
        public IEnumerable<Genders> GenderList { get; private set; }

        private IEnumerable<Staff_List> _esr;
        public IEnumerable<Staff_List> ESR { get { return _esr; } private set { _esr = value; NotifyPropertyChanged(); } }

        public IEnumerable<Cohort> CohortList { get; private set; }
        public IEnumerable<Cost_Centres> CostCentres { get; private set; }
        public IEnumerable<Subjective> Subjectives { get; private set; }
        public IEnumerable<Staff> Leaders { get; private set; }
        public IEnumerable<Staff> Trainers { get; private set; }
        public IEnumerable<Team> Teams { get; private set; }

        public IEnumerable<Status> TNAOutcomes { get; private set; }
        public IEnumerable<Status> StatusESRUp { get; private set; }
        public IEnumerable<Status> StatusPDSRole { get; private set; }
        public IEnumerable<Status> StatusPlusUp { get; private set; }

        //Search Fields:
        private string _staffCodeSearch;
        private string _nameSearch;
        private string _jobTitleSearch;
        private int? _mhcSearch;
        private string _boroughSearch;
        private int? _serviceSearch;
        private Team _teamSearch;
        private Staff _lineManSearch;
        private Cohort _cohortSearch;
        private bool? _leftTrustSearch;
        private bool? _externalSearch;

        public string StaffCodeSearch { get { return _staffCodeSearch; } set { _staffCodeSearch = value; NotifyPropertyChanged(); } }
        public string NameSearch { get { return _nameSearch; } set { _nameSearch = value;  NotifyPropertyChanged(); } }
        public string JobTitleSearch { get { return _jobTitleSearch; } set { _jobTitleSearch = value; NotifyPropertyChanged(); } }
        public int? MHCSearch { get { return _mhcSearch; } set { _mhcSearch = value; NotifyPropertyChanged(); } }
        public string BoroughSearch { get { return _boroughSearch; } set { _boroughSearch = value; NotifyPropertyChanged(); } }
        public int? ServiceSearch { get { return _serviceSearch; } set { _serviceSearch = value; NotifyPropertyChanged(); } }
        public Team TeamSearch { get { return _teamSearch; } set { _teamSearch = value; NotifyPropertyChanged(); } }
        public Staff LineManSearch { get { return _lineManSearch; } set { _lineManSearch = value;  NotifyPropertyChanged(); } }
        public Cohort CohortSearch { get { return _cohortSearch; } set { _cohortSearch = value; NotifyPropertyChanged(); } }
        public bool? LeftTrustSearch { get { return _leftTrustSearch; } set { _leftTrustSearch = value; NotifyPropertyChanged(); } }
        public bool? ExternalSearch { get { return _externalSearch; } set { _externalSearch = value; NotifyPropertyChanged(); } }

        public ICommand SearchCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }

        public StaffViewModel()
        {
            db = new StaffEntities();
            db.Staffs.Load();
            StaffList = db.Staffs.Local.ToList();
            SelectedStaff = StaffList.First();
            db.Titles.Load();
            Titles = db.Titles.Local.ToList();
            NotifyPropertyChanged("Titles");
            GenderList = new List<Genders>(){ Genders.Male, Genders.Female, Genders.Not_Known };
            db.Cohorts.Load();
            CohortList = db.Cohorts.Local.OrderBy(c => c.Number).ToList();
            NotifyPropertyChanged("CohortList");
            db.Cost_Centres.Where(cc => cc.Enbld).Load();
            CostCentres = db.Cost_Centres.Local.ToList();
            NotifyPropertyChanged("CostCentres");
            db.Subjectives.Load();
            Subjectives = db.Subjectives.Local.ToList();
            NotifyPropertyChanged("Subjectives");
            Leaders = db.Staffs.Local.Where(s => s.LM == true).OrderBy(s => s.Sname).ToList();
            NotifyPropertyChanged("Leaders");
            db.Statuses.Load();
            TNAOutcomes = db.Statuses.Local.Where(s => s.TNA_OUT).ToList();
            NotifyPropertyChanged("TNAOutcomes");
            StatusESRUp = db.Statuses.Local.Where(s => s.RA_ESR).ToList();
            NotifyPropertyChanged("StatusESRUp");
            StatusPDSRole = db.Statuses.Local.Where(s => s.RA_PDS).ToList();
            NotifyPropertyChanged("StatusPDSRole");
            StatusPlusUp = db.Statuses.Local.Where(s => s.RA_PLUS).ToList();
            NotifyPropertyChanged("StatusPlusUp");
            Trainers = db.Staffs.Local.Where(s => s.Trainer == true && s.External == false).OrderBy(s => s.Sname).ToList();
            NotifyPropertyChanged("Trainers");
            db.Teams.Where(t => t.TeamMems.Count > 0 || t.ESR == null).Load();
            Teams = db.Teams.Local.OrderBy(t => t.TeamName).ToList();
            NotifyPropertyChanged("Teams");

            ResetSearch(null);

            SearchCommand = new DelegateCommand<object>(Search);
            ResetCommand = new DelegateCommand<object>(ResetSearch);
        }

        private void UpdateLinkedStuff()
        {
            if (SelectedStaff != null)
            {
                db.Staff_List.Where(e => e.Employee_Number == SelectedStaff.ESRID).Load();
                ESR = db.Staff_List.Where(e => e.Employee_Number == SelectedStaff.ESRID).ToList();
            }
        }

        private void Search(object parameter)
        {
            int? TeamSearchID = (TeamSearch == null) ? 0 : TeamSearch.ID;
            int? LineManSearchID = (LineManSearch == null) ? 0 : LineManSearch.ID;
            int? CohortSearchID = (CohortSearch == null) ? 0 : CohortSearch.ID;
            StaffList = db.search_staff(StaffCodeSearch, NameSearch, JobTitleSearch, MHCSearch, BoroughSearch, ServiceSearch, TeamSearchID, LineManSearchID, CohortSearchID, LeftTrustSearch, ExternalSearch).OrderBy(s => s.Sname).ToList();
            SelectedStaff = StaffList.FirstOrDefault();
        }

        private void ResetSearch(object parameter)
        {
            StaffCodeSearch = null;
            NameSearch = null;
            JobTitleSearch = null;
            MHCSearch = null;
            BoroughSearch = null;
            ServiceSearch = null;
            TeamSearch = null;
            LineManSearch = null;
            CohortSearch = null;
            LeftTrustSearch = false;
            ExternalSearch = false;
        }
    }
}
