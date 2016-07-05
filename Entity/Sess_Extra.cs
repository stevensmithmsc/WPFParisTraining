using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFParisTraining.Entity
{
    public partial class Sess
    {
        public Nullable<DateTime> StartDate
        {
            get { if (Strt == null) return null; else return ((DateTime)Strt).Date; }
            set { if (value != null) { if (Strt == null) Strt = value; else { Strt = ((DateTime)value).Date.Add(((DateTime)Strt).TimeOfDay); } } }
        }
        public Nullable<TimeSpan> StartTime
        {
            get { if (Strt == null) return null; else return ((DateTime)Strt).TimeOfDay; }
            set { if (value != null) { if (Strt == null) Strt = DateTime.Now.Date.Add((TimeSpan)value); else { Strt = ((DateTime)Strt).Date.Add((TimeSpan)value); } } }
        }

        public Nullable<TimeSpan> EndTime
        {
            get { if (Endt == null) return null; else return ((DateTime)Endt).TimeOfDay; }
            set { if (value != null) { if (Endt == null && Strt == null) Endt = DateTime.Now.Date.Add((TimeSpan)value); else if (Endt == null) { Endt = ((DateTime)Strt).Date.Add((TimeSpan)value); } else { Endt = ((DateTime)Endt).Date.Add((TimeSpan)value); } } }
        }

        public int Bookings { get { return Attendances.Where(a => a.Outcome != 6 && a.Outcome != 7).Count(); } }
        public int UnoutcommedBookings { get { return Attendances.Where(a => a.Outcome == 0).Count(); } }
        public int AvailablePlaces { get { return (MaxP - Bookings); } }
    }
}
