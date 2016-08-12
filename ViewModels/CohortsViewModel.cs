using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFParisTraining.Entity;

namespace WPFParisTraining.ViewModels
{
    class CohortsViewModel : DBViewModel
    {
        private IEnumerable<Cohort> _cohortList;
        public IEnumerable<Cohort> CohortList { get { return _cohortList; } set { _cohortList = value; NotifyPropertyChanged(); } }

        private Cohort _selectedCohort;
        public Cohort SelectedCohort { get { return _selectedCohort; } set { _selectedCohort = value; NotifyPropertyChanged(); UpdateTeamList(); NotifyPropertyChanged("Changed"); } }

        public IEnumerable<Team> TeamList { get; private set; }

        private Visibility _addCohortButtonVis;
        private Visibility _removeCohortButtonVis;
        public Visibility AddCohortButtonVis { get { return _addCohortButtonVis; } set { if (value != _addCohortButtonVis) { _addCohortButtonVis = value; NotifyPropertyChanged(); } } }
        public Visibility RemoveCohortButtonVis { get { return _removeCohortButtonVis; } set { if (value != _removeCohortButtonVis) { _removeCohortButtonVis = value; NotifyPropertyChanged(); } } }

        protected override void AssignCommands()
        {
            AddCommand = new DelegateCommand<object>(AddCohort);
            RemoveCommand = new DelegateCommand<object>(RemoveCohort);
            SaveCommand = new DelegateCommand<object>(SaveDataChanges);
        }

        protected override void InitalDisplayState()
        {
            AddCohortButtonVis = Visibility.Visible;
            RemoveCohortButtonVis = Visibility.Visible;
        }

        protected async override void LoadInitalData()
        {
            CohortList = await db.Cohorts.OrderBy(c => c.Number).ThenBy(c => c.CohortDescription).ToListAsync();
            SelectedCohort = CohortList.FirstOrDefault();
        }

        protected override void LoadRefData()
        {
            
        }

        private async void UpdateTeamList()
        {
            if (SelectedCohort != null)
            {
                TeamList = await db.Teams.Where(t => t.CohortID == SelectedCohort.ID).OrderBy(t => t.TeamName).ToListAsync();
                NotifyPropertyChanged("TeamList");
            }
        }

        private void AddCohort(object Parameter)
        {
            Cohort newCohort = new Cohort();
            newCohort.Number = db.Cohorts.Local.Select(c => c.Number).Max();
            db.Cohorts.Add(newCohort);
            CohortList = db.Cohorts.Local.OrderBy(c => c.Number).ThenBy(c => c.CohortDescription).ToList();
            SelectedCohort = newCohort;
        }

        private void RemoveCohort(object Parameter)
        {
            if (SelectedCohort != null)
            {
                db.Cohorts.Remove(SelectedCohort);
                CohortList = db.Cohorts.Local.OrderBy(c => c.Number).ThenBy(c => c.CohortDescription).ToList();
                SelectedCohort = CohortList.FirstOrDefault();
            }
        }
    }
}
