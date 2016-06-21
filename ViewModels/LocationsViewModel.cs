using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFParisTraining.Entity;

namespace WPFParisTraining.ViewModels
{
    class LocationsViewModel : ViewModel
    {
        private StaffEntities db;

        

        public LocationsViewModel()
        {
            db = new StaffEntities();

        }
    }
}
