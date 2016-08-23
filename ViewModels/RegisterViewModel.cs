using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WPFParisTraining.Helpers;

namespace WPFParisTraining.ViewModels
{
    class RegisterViewModel : ViewModel
    {
        public FlowDocument SampleDoc { get; private set; }

        public RegisterViewModel()
        {
            SampleDoc = new FlowDocument();
            SampleDoc.PageWidth = 1200;
            SampleDoc.Blocks.Add(SessionRegister.Generate());
            NotifyPropertyChanged("SampleDoc");
        }
    }
}
