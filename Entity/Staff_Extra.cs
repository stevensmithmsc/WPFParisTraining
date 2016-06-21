using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFParisTraining.Entity
{
    public partial class Staff
    {
        public string FullName { get { return (TitleID.HasValue ? Title.TitleValue + " " : "") + Fname + " " + Sname; } }
        public string DisplayName { get { return PName!=null&&PName.Length > 0 ? PName : FullName; } }
        public string SimpleName { get { return PName!=null&&PName.Length > 0 ? PName : (Fname + " " + Sname); } }

        public Team MainTeam { get { return (TeamMems.Where(m => m.Active && m.Main).Count())>0?TeamMems.Where(m => m.Active && m.Main).First().Team:null; } }

        public Cohort CalculatedCohort { get { return CohortID.HasValue ? Cohort : (MainTeam == null ? null : MainTeam.Cohort); } }
    }
}
