using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFParisTraining.Entity
{
    public partial class Sess
    {
        public Nullable<DateTime> StartDate { get { if (Strt == null) return null; else return ((DateTime)Strt).Date; }
                                              set { if (value != null) { if (Strt == null) Strt = value; else { Strt = ((DateTime)value).Date.Add(((DateTime)Strt).TimeOfDay); } } } }
        public Nullable<TimeSpan> StartTime { get { if (Strt == null) return null; else return ((DateTime)Strt).TimeOfDay; }
                                              set { if (value != null) { if (Strt == null) Strt = DateTime.Now.Date.Add((TimeSpan)value); else { Strt = ((DateTime)Strt).Date.Add((TimeSpan)value); } } } }
    }
}
