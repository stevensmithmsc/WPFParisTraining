using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WPFParisTraining.Entity;
using WPFParisTraining.Helpers;

namespace WPFParisTraining.ViewModels
{
    class RegisterViewModel : ViewModel
    {
        public FlowDocument SampleDoc { get; private set; }

        public RegisterViewModel()
        {
            StaffEntities db = new StaffEntities();
            Sess Session = db.Sesses.Find(2894);

            SampleDoc = new FlowDocument();
            SampleDoc.PageWidth = 1200;
            SampleDoc.Blocks.Add(SessionRegister.Generate(Session));
            SampleDoc.Blocks.Add(new Paragraph(new Run("Trainers, please add any DNAs and ensure this is returned to the IT Training Administrator as soon as possible - fax if necessary 0161 716 3391")));
            NotifyPropertyChanged("SampleDoc");
        }
    }
}
