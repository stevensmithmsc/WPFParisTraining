using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using WPFParisTraining.Entity;

namespace WPFParisTraining.ViewModels
{
    class EMailTemplateViewModel : DBViewModel
    {
        private IEnumerable<mail_temp> _templateList;
        public IEnumerable<mail_temp> TemplateList { get { return _templateList; } set { _templateList = value;  NotifyPropertyChanged(); } }

        private mail_temp _selectedTemplate;
        public mail_temp SelectedTemplate { get { return _selectedTemplate; } set { _selectedTemplate = value;  NotifyPropertyChanged(); UpdateCourseList(); } }

        public IEnumerable<Course> CoursesUsing { get; private set; }

        //public FlowDocument SampleMessage { get; private set; }

        protected override void AssignCommands()
        {
            
        }

        protected override void InitalDisplayState()
        {
            
        }

        protected override void LoadInitalData()
        {
            TemplateList = db.mail_temp.ToList();
            SelectedTemplate = TemplateList.FirstOrDefault();
        }

        protected override void LoadRefData()
        {
            /*Paragraph par = new Paragraph();

            Run runText = new Run(
                "Dear ....");
            Run runNumerals = new Run("Course Details");
            //Span runFractions = new Span("<Paragraph>Details of where to find the locations can be found via the following link:- </Paragraph><Paragraph TextAlignment='center'><Hyperlink NavigateUri='http://portal/ict/css/training/Pages/Paris-Training-Locations.aspx'>http://portal/ict/css/training/Pages/Paris-Training-Locations.aspx</Hyperlink></Paragraph><Paragraph Foreground='#595959'><Span FontStyle='italic'>(Parking is not available at Blenheim so please <Run FontWeight='bold'>DO NOT</Run> park in the marked bays surrounding Blenheim. There is a car park opposite which is pay and display at £5 for all day or £3 for 3 hours. If your training is scheduled for Training Room 1 or 2 at Royal Oldham, please note that the location is at Willows Ward, Forest House and <Run FontWeight='bold'>NOT</Run> Pennine Acute Training Centre.)</Span></Paragraph><Paragraph>If for any reason you are unable to attend any part of the training please contact the IT Training Department as soon as possible on 0161 716 3320 or 0161 716 1234</Paragraph><Paragraph>Kind Regards</Paragraph><Paragraph FontFamily='bradley hand itc' FontSize='1.5em'>IT Training</Paragraph>");

            par.Inlines.Add(runText);
            par.Inlines.Add(new LineBreak());
            par.Inlines.Add(new LineBreak());
            par.Inlines.Add(runNumerals);
            par.Inlines.Add(new LineBreak());
            par.Inlines.Add(new LineBreak());
            //par.Inlines.Add(runFractions);

            par.TextAlignment = TextAlignment.Left;
            par.FontSize = 18;
            par.FontFamily = new FontFamily("Arial Sans-Serif");

            

            SampleMessage = new FlowDocument(par);
            NotifyPropertyChanged("SampleMessage");*/
        }

        private async void UpdateCourseList()
        {
            if (SelectedTemplate != null)
            {
                CoursesUsing = await db.Courses.Where(c => c.Template == SelectedTemplate.ID).OrderBy(c => c.CourseName).ToListAsync();
                NotifyPropertyChanged("CoursesUsing");
            }
            
        }
    }
}
