using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFParisTraining.Entity
{
    public partial class Course
    {
        public int NumberRequired { get { return StaffReqs.Where(r => r.StatusID == 1).Count(); } }
        public int NumberCompleted { get { return StaffReqs.Where(r => r.StatusID == 5).Count(); } }
        public int PlacesAvailable { get { return Sesses.Where(s => s.Strt > DateTime.Now).Sum(s => s.AvailablePlaces); } }
        public int PlacesBooked { get { return Sesses.Sum(s => s.UnoutcommedBookings); } }
    }
}
