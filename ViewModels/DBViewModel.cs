﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFParisTraining.Entity;

namespace WPFParisTraining.ViewModels
{
    public abstract class DBViewModel : ViewModel, IDisposable
    {
        protected StaffEntities db;
        protected bool _addMode;

        public bool Changed { get { return db.ChangeTracker.HasChanges(); } }

        public ICommand SaveCommand { get; protected set; }
        public ICommand AddCommand { get; protected set; }
        public ICommand RemoveCommand { get; protected set; }

        public DBViewModel()
        {
            //set db context
            db = new StaffEntities();

            LoadRefData();
            LoadInitalData();
            AssignCommands();
        }

        protected abstract void LoadRefData();
        protected abstract void LoadInitalData();
        protected abstract void AssignCommands();

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
