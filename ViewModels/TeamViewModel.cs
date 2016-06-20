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

        public TeamViewModel()
        {
            db = new StaffEntities();
            db.Teams.Load();
            TeamList = db.Teams.Local;
            SelectedTeam = TeamList.First();
        }

        private void UpdateLinkedStuff()
        {

        }
    }
}
