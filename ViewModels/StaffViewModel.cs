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
    class StaffViewModel : DBViewModel
    {
        private List<Staff> _staffList;
        public List<Staff> StaffList { get { return _staffList; } set { _staffList = value;  NotifyPropertyChanged(); } }

        private Staff _selectedStaff;
        public Staff SelectedStaff { get { return _selectedStaff; } set { CheckLinkedEntities(); _selectedStaff = value;  NotifyPropertyChanged(); UpdateLinkedStuff(); } }

        private IEnumerable<TeamMem> _teamMemberships;
        public IEnumerable<TeamMem> TeamMemberships { get { return _teamMemberships; } set { _teamMemberships = value;  NotifyPropertyChanged(); } }
        private TeamMem _selectedTeam;
        public TeamMem SelectedTeam { get { return _selectedTeam; } set { _selectedTeam = value;  NotifyPropertyChanged(); } }

        private RA _staffRA;
        public RA StaffRA { get { return _staffRA; } set { _staffRA = value;  NotifyPropertyChanged(); } }

        private TNA _staffTNA;
        public TNA StaffTNA { get { return _staffTNA; }  set { _staffTNA = value;  NotifyPropertyChanged(); } }

        private IEnumerable<TeamApprov> _teamApprovals;
        public IEnumerable<TeamApprov> TeamApprovals { get { return _teamApprovals; } set { _teamApprovals = value;  NotifyPropertyChanged(); } }
        private TeamApprov _selectedTeamApprov;
        public TeamApprov SelectedTeamApprov { get { return _selectedTeamApprov; } set { _selectedTeamApprov = value;  NotifyPropertyChanged(); } }

        private IEnumerable<Req> _staffReqs;
        public IEnumerable<Req> StaffReqs { get { return _staffReqs; } set { _staffReqs = value;  NotifyPropertyChanged(); } }
        private Req _selectedReq;
        public Req SelectedReq { get { return _selectedReq; } set { _selectedReq = value;  NotifyPropertyChanged(); } }

        private IEnumerable<Attendance> _staffAttendances;
        public IEnumerable<Attendance> StaffAttendances { get { return _staffAttendances; } set { _staffAttendances = value;  NotifyPropertyChanged(); } }

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
        public IEnumerable<Course> Courses { get; private set; }

        public IEnumerable<Status> TNAOutcomes { get; private set; }
        public IEnumerable<Status> StatusESRUp { get; private set; }
        public IEnumerable<Status> StatusPDSRole { get; private set; }
        public IEnumerable<Status> StatusPlusUp { get; private set; }
        public IEnumerable<Status> ReqStatus { get; private set; }
        public IEnumerable<Status> AttendanceOutcome { get; private set; }

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
        public ICommand SaveCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand AddTeamApprovCommand { get; private set; }
        public ICommand RemoveTeamApprovCommand { get; private set; }
        public ICommand AddReqCommand { get; private set; }
        public ICommand RemoveReqCommand { get; private set; }
        public ICommand AddTeamCommand { get; private set; }
        public ICommand RemoveTeamCommand { get; private set; }


        public StaffViewModel()
        {
            db = new StaffEntities();
            db.Staffs.Where(s => s.LM == true || s.Trainer == true).Load();
            Leaders = db.Staffs.Local.Where(s => s.LM == true).OrderBy(s => s.Sname).ThenBy(s => s.Fname).ToList();
            NotifyPropertyChanged("Leaders");
            Trainers = db.Staffs.Local.Where(s => s.Trainer == true && s.External == false).OrderBy(s => s.Sname).ThenBy(s => s.Fname).ToList();
            NotifyPropertyChanged("Trainers");
            db.Staffs.Take(50).Load();
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
            db.Statuses.Load();
            TNAOutcomes = db.Statuses.Local.Where(s => s.TNA_OUT).ToList();
            NotifyPropertyChanged("TNAOutcomes");
            StatusESRUp = db.Statuses.Local.Where(s => s.RA_ESR).ToList();
            NotifyPropertyChanged("StatusESRUp");
            StatusPDSRole = db.Statuses.Local.Where(s => s.RA_PDS).ToList();
            NotifyPropertyChanged("StatusPDSRole");
            StatusPlusUp = db.Statuses.Local.Where(s => s.RA_PLUS).ToList();
            NotifyPropertyChanged("StatusPlusUp");
            ReqStatus = db.Statuses.Local.Where(s => s.Requirement).ToList();
            NotifyPropertyChanged("ReqStatus");
            AttendanceOutcome = db.Statuses.Local.Where(s => s.Attendance).ToList();
            NotifyPropertyChanged("AttendanceOutcome");
            db.Teams.Where(t => t.TeamMems.Count > 0 || t.ESR == null).Load();
            Teams = db.Teams.Local.OrderBy(t => t.TeamName).ToList();
            NotifyPropertyChanged("Teams");
            db.Courses.Load();
            Courses = db.Courses.Local.OrderBy(c => c.CourseName);
            NotifyPropertyChanged("Courses");


            ResetSearch(null);

            SearchCommand = new DelegateCommand<object>(Search);
            ResetCommand = new DelegateCommand<object>(ResetSearch);
            AddTeamApprovCommand = new DelegateCommand<object>(AddTeamApprov);
            RemoveTeamApprovCommand = new DelegateCommand<object>(RemoveTeamApprov);
            AddReqCommand = new DelegateCommand<object>(AddReq);
            RemoveReqCommand = new DelegateCommand<object>(RemoveReq);
            AddTeamCommand = new DelegateCommand<object>(AddTeamMembership);
            RemoveTeamCommand = new DelegateCommand<object>(RemoveTeamMembership);
            AddCommand = new DelegateCommand<object>(AddStaff);
        }

        private void UpdateLinkedStuff()
        {
            if (SelectedStaff != null)
            {
                db.TeamMems.Where(t => t.StaffID == SelectedStaff.ID).Load();
                TeamMemberships = db.TeamMems.Local.Where(t => t.StaffID == SelectedStaff.ID).OrderBy(t => t.Team.TeamName).ToList();
                db.Staff_List.Where(e => e.Employee_Number == SelectedStaff.ESRID).Load();
                ESR = db.Staff_List.Where(e => e.Employee_Number == SelectedStaff.ESRID).ToList();
                db.RAs.Where(r => r.ID == SelectedStaff.ID).Load();
                StaffRA = db.RAs.Local.SingleOrDefault(r => r.ID == SelectedStaff.ID);  
                if (StaffRA == null) StaffRA = new RA();
                db.TNAs.Where(t => t.ID == SelectedStaff.ID).Load();
                StaffTNA = db.TNAs.Local.SingleOrDefault(t => t.ID == SelectedStaff.ID);
                if (StaffTNA == null) StaffTNA = new TNA();
                db.TeamApprovs.Where(t => t.StaffID == SelectedStaff.ID).Load();
                TeamApprovals = db.TeamApprovs.Local.Where(t => t.StaffID == SelectedStaff.ID).ToList();
                SelectedTeamApprov = TeamApprovals.FirstOrDefault();
                db.Reqs.Where(r => r.StaffID == SelectedStaff.ID).Load();
                StaffReqs = db.Reqs.Local.Where(r => r.StaffID == SelectedStaff.ID).ToList();
                SelectedReq = StaffReqs.FirstOrDefault();
                db.Attendances.Where(a => a.StaffID == SelectedStaff.ID).Load();
                StaffAttendances = db.Attendances.Local.Where(a => a.StaffID == SelectedStaff.ID).ToList();
            }
        }

        private void CheckLinkedEntities()
        {
            if (SelectedStaff != null && StaffRA.Staff == null)
            {
                if (StaffRA.UUID != null || StaffRA.PDS_Role.HasValue || StaffRA.PLUS_Updated.HasValue ||
                    StaffRA.ESR_Updated.HasValue || StaffRA.RAComments != null || StaffRA.EGifL3.HasValue ||
                    StaffRA.PA1Rec.HasValue || StaffRA.Declaration.HasValue || StaffRA.Go_Live_Approved.HasValue ||
                    StaffRA.Account_Created.HasValue || StaffRA.Add_CITRIX.HasValue || StaffRA.Password_Emailed.HasValue ||
                    StaffRA.Access_to_Plus.HasValue || StaffRA.UUID_Add_ESR.HasValue)
                {
                    StaffRA.ID = SelectedStaff.ID;
                    SelectedStaff.RA = StaffRA;
                    db.RAs.Add(StaffRA);
                }
            }

            if (SelectedStaff != null && StaffTNA.Staff == null)
            {
                if (StaffTNA.Date_Received.HasValue || StaffTNA.TrainerID.HasValue || StaffTNA.Contact_Date.HasValue ||
                    StaffTNA.Contact_Outcome != null || StaffTNA.TNA_Outcome.HasValue )
                {
                    StaffTNA.ID = SelectedStaff.ID;
                    SelectedStaff.TNA = StaffTNA;
                    db.TNAs.Add(StaffTNA);
                }
            }

            NotifyPropertyChanged("Changed");
        }

        private void Search(object parameter)
        {
            int? TeamSearchID = (TeamSearch == null) ? 0 : TeamSearch.ID;
            int? LineManSearchID = (LineManSearch == null) ? 0 : LineManSearch.ID;
            int? CohortSearchID = (CohortSearch == null) ? 0 : CohortSearch.ID;
            StaffList = db.search_staff(StaffCodeSearch, NameSearch, JobTitleSearch, MHCSearch, BoroughSearch, ServiceSearch, TeamSearchID, LineManSearchID, CohortSearchID, LeftTrustSearch, ExternalSearch).OrderBy(s => s.Sname).ThenBy(s => s.Fname).ToList();
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

        private void AddTeamApprov(object parameter)
        {
            TeamApprov newTeamApprov = new TeamApprov();
            newTeamApprov.StaffID = SelectedStaff.ID;
            db.TeamApprovs.Add(newTeamApprov);
            TeamApprovals = db.TeamApprovs.Local.Where(t => t.StaffID == SelectedStaff.ID).ToList();
            SelectedTeamApprov = newTeamApprov;
            NotifyPropertyChanged("Changed");
        }

        private void RemoveTeamApprov(object parameter)
        {
            if (SelectedTeamApprov != null && MessageBox.Show("Are you sure you want to delete approval for " + SelectedTeamApprov.Team, "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.TeamApprovs.Remove(SelectedTeamApprov);
                TeamApprovals = db.TeamApprovs.Local.Where(t => t.StaffID == SelectedStaff.ID).ToList();
                SelectedTeamApprov = TeamApprovals.FirstOrDefault();
                NotifyPropertyChanged("Changed");
            }
        }

        private void AddReq(object parameter)
        {
            Req newReq = new Req();
            newReq.StaffID = SelectedStaff.ID;
            newReq.StatusID = 1;
            db.Reqs.Add(newReq);
            StaffReqs = db.Reqs.Local.Where(r => r.StaffID == SelectedStaff.ID).ToList();
            SelectedReq = newReq;
            NotifyPropertyChanged("Changed");
        }

        private void RemoveReq(object parameter)
        {
            if (SelectedReq != null && MessageBox.Show("Are you sure you want to delete requirement for " + SelectedReq.Course.CourseName, "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                db.Reqs.Remove(SelectedReq);
                StaffReqs = db.Reqs.Local.Where(r => r.StaffID == SelectedStaff.ID).ToList();
                SelectedReq = StaffReqs.FirstOrDefault();
                NotifyPropertyChanged("Changed");
            }
        }

        private void AddTeamMembership(object parameter)
        {
            TeamMem newTeamMem = new TeamMem();
            newTeamMem.StaffID = SelectedStaff.ID;
            newTeamMem.Active = true;
            newTeamMem.Main = SelectedStaff.TeamMems.Count() == 0 ? true : false;
            db.TeamMems.Add(newTeamMem);
            TeamMemberships = db.TeamMems.Local.Where(t => t.StaffID == SelectedStaff.ID).ToList();
            SelectedTeam = newTeamMem;
            NotifyPropertyChanged("Changed");
        }

        private void RemoveTeamMembership(object parameter)
        {
            if (SelectedTeam != null && (SelectedTeam.Team == null || MessageBox.Show("Are you sure you want to delete membership of " + SelectedTeam.Team.TeamName, "Training Database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                db.TeamMems.Remove(SelectedTeam);
                TeamMemberships = db.TeamMems.Local.Where(t => t.StaffID == SelectedStaff.ID).OrderBy(t => t.Team.TeamName).ToList();
                SelectedTeam = null;
                NotifyPropertyChanged("Changed");
            }
        }

        private void AddStaff(object parameter)
        {
            Staff newStaff = new Staff();
            StaffList.Add(newStaff);
            SelectedStaff = newStaff;
        }
    }
}
