using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            
        }

        private async void UpdateCourseList()
        {
            if (SelectedTemplate != null)
            {
                CoursesUsing = await db.Courses.Where(c => c.Template == SelectedTemplate.ID).ToListAsync();
                NotifyPropertyChanged("CoursesUsing");
            }
            
        }
    }
}
