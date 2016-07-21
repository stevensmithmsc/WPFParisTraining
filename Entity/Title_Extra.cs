using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFParisTraining.Entity
{
    public partial class Title
    {
        public List<Genders> AllowedGenders { get
            {
                List<Genders>  genlist = new List<Genders>();
                foreach (string s in AvailGenders.Split(';'))
                {
                    genlist.Add((Genders)Enum.Parse(typeof(Genders), s));
                }
                return genlist;
            } }
    }
}
