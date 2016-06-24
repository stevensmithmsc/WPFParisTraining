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
    class SessionsViewModel : ViewModel
    {
        private StaffEntities db;

        private ObservableCollection<Sess> _sessionList;
        public ObservableCollection<Sess> SessionList { get { return _sessionList; } set { _sessionList = value; NotifyPropertyChanged(); } }

        private Sess _selectedSession;
        public Sess SelectedSession { get { return _selectedSession; } set { _selectedSession = value; NotifyPropertyChanged(); } }

        public SessionsViewModel()
        {
            db = new StaffEntities();
            db.Sesses.Where(s => s.Strt > DateTime.Now).OrderBy(s => s.Strt).Load();
            SessionList = db.Sesses.Local;
        }
    }
}
