using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFParisTraining.Entity;

namespace WPFParisTraining.ViewModels
{
    public abstract class DBViewModel : ViewModel, IDisposable
    {
        protected StaffEntities db;
        protected bool _addMode;

        public bool Changed { get { return db.ChangeTracker.HasChanges(); } }

        async protected void SaveDataChanges(object parameter)
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error writing to Database!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                NotifyPropertyChanged("Changed");
            } 
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
