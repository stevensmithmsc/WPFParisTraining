using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFParisTraining.Entity;

namespace WPFParisTraining.ViewModels
{
    class BulkReqViewModel : DBViewModel
    {
        private IEnumerable<Cohort> _allCohorts;
        private IEnumerable<Cohort> _selectedCohorts;

        public IEnumerable<Cohort> AllCohorts { get { return _allCohorts; } set { _allCohorts = value; NotifyPropertyChanged(); } }
        public IEnumerable<Cohort> SelectedCohorts { get { return _selectedCohorts; } set { _selectedCohorts = value; NotifyPropertyChanged(); } }

        private bool _ignoreTeams;
        private bool? _parisCourses;
        private bool? _chCourses;

        public bool IgnoreTeams { get { return _ignoreTeams; } set { if (value != _ignoreTeams) { _ignoreTeams = value; NotifyPropertyChanged(); } } }
        public bool? ParisCourses { get { return _parisCourses; } set { if (value != _parisCourses) { _parisCourses = value; NotifyPropertyChanged(); } } }
        public bool? CHCourses { get { return _chCourses; } set { if (value != _chCourses) { _chCourses = value; NotifyPropertyChanged(); } } }

        protected override void AssignCommands()
        {
            
        }

        protected override void InitalDisplayState()
        {
            IgnoreTeams = false;
            ParisCourses = null;
            CHCourses = null;
        }

        async protected override void LoadInitalData()
        {
            AllCohorts = await db.Cohorts.OrderBy(c => c.Number).ThenBy(c => c.CohortDescription).ToListAsync();
        }

        protected override void LoadRefData()
        {
            
        }
    }
}
