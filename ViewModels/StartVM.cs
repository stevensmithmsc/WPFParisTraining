using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFParisTraining.Entity;

namespace WPFParisTraining.ViewModels
{
    class StartVM : DBViewModel
    {
        private string _sysUserName;
        private string _currentUserName;
        private bool _isSysAdm;
        private bool _isDbOwner;
        private bool _isTrainAdm;
        private bool _isTrainer;
        private bool _isRA;
        private Staff _userStaffRecord;

        public string SysUserName { get { return _sysUserName; } set { _sysUserName = value;  NotifyPropertyChanged(); } }
        public string CurrentUserName { get { return _currentUserName; } set { _currentUserName = value; NotifyPropertyChanged(); } }
        public bool IsSysAdm { get { return _isSysAdm; } set { _isSysAdm = value; NotifyPropertyChanged(); } }
        public bool IsDbOwner { get { return _isDbOwner; } set { _isDbOwner = value; NotifyPropertyChanged(); } }
        public bool IsTrainAdm { get { return _isTrainAdm; } set { _isTrainAdm = value; NotifyPropertyChanged(); } }
        public bool IsTrainer { get { return _isTrainer; } set { _isTrainer = value; NotifyPropertyChanged(); } }
        public bool IsRA { get { return _isRA; } set { _isRA = value; NotifyPropertyChanged(); } }
        public Staff UserStaffRecord { get { return _userStaffRecord; } set { _userStaffRecord = value;  NotifyPropertyChanged(); } }



        protected override void AssignCommands()
        {
            
        }

        protected override void LoadInitalData()
        {
            var userdata = db.user_details().FirstOrDefault();

            SysUserName = userdata.System_User_Name;
            CurrentUserName = userdata.Current_User_Name;
            if (userdata.SysAdm != null && userdata.SysAdm == 1) { IsSysAdm = true; } else { IsSysAdm = false; }
            if (userdata.dbowner != null && userdata.dbowner == 1) { IsDbOwner = true; } else { IsDbOwner = false; }
            if (userdata.TrainAdm != null && userdata.TrainAdm == 1) { IsTrainAdm = true; } else { IsTrainAdm = false; }
            if (userdata.trainer != null && userdata.trainer == 1) { IsTrainer = true; } else { IsTrainer = false; }
            if (userdata.ra != null && userdata.ra == 1) { IsRA = true; } else { IsRA = false; }
            if (userdata.id != null)
            {
                UserStaffRecord = db.Staffs.Find(userdata.id);
            }
        }

        protected override void LoadRefData()
        {
            
        }
    }
}
