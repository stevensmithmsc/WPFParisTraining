﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFParisTraining.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StaffEntities : DbContext
    {
        public StaffEntities()
            : base("name=StaffEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cohort> Cohorts { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<RA> RAs { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<TeamApprov> TeamApprovs { get; set; }
        public virtual DbSet<TeamMem> TeamMems { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<TNA> TNAs { get; set; }
        public virtual DbSet<Staff_List> Staff_List { get; set; }
        public virtual DbSet<Cost_Centres> Cost_Centres { get; set; }
        public virtual DbSet<Subjective> Subjectives { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
    }
}
