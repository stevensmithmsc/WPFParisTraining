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

        protected override void AssignCommands()
        {
            
        }

        protected override void InitalDisplayState()
        {
            
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
