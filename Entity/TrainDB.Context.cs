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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<CourseReq> CourseReqs { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Req> Reqs { get; set; }
        public virtual DbSet<Sess> Sesses { get; set; }
        public virtual DbSet<Borough> Boroughs { get; set; }
        public virtual DbSet<BoroughMem> BoroughMems { get; set; }
        public virtual DbSet<mail_temp> mail_temp { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServMem> ServMems { get; set; }
    
        public virtual ObjectResult<Course> search_course(string name, Nullable<bool> paris, Nullable<bool> ch, Nullable<bool> ext, Nullable<bool> obs)
        {
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var parisParameter = paris.HasValue ?
                new ObjectParameter("paris", paris) :
                new ObjectParameter("paris", typeof(bool));
    
            var chParameter = ch.HasValue ?
                new ObjectParameter("ch", ch) :
                new ObjectParameter("ch", typeof(bool));
    
            var extParameter = ext.HasValue ?
                new ObjectParameter("ext", ext) :
                new ObjectParameter("ext", typeof(bool));
    
            var obsParameter = obs.HasValue ?
                new ObjectParameter("obs", obs) :
                new ObjectParameter("obs", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Course>("search_course", nameParameter, parisParameter, chParameter, extParameter, obsParameter);
        }
    
        public virtual ObjectResult<Course> search_course(string name, Nullable<bool> paris, Nullable<bool> ch, Nullable<bool> ext, Nullable<bool> obs, MergeOption mergeOption)
        {
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var parisParameter = paris.HasValue ?
                new ObjectParameter("paris", paris) :
                new ObjectParameter("paris", typeof(bool));
    
            var chParameter = ch.HasValue ?
                new ObjectParameter("ch", ch) :
                new ObjectParameter("ch", typeof(bool));
    
            var extParameter = ext.HasValue ?
                new ObjectParameter("ext", ext) :
                new ObjectParameter("ext", typeof(bool));
    
            var obsParameter = obs.HasValue ?
                new ObjectParameter("obs", obs) :
                new ObjectParameter("obs", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Course>("search_course", mergeOption, nameParameter, parisParameter, chParameter, extParameter, obsParameter);
        }
    
        public virtual ObjectResult<Sess> search_session(Nullable<int> course, Nullable<int> trainer, Nullable<int> location, Nullable<System.DateTime> from, Nullable<System.DateTime> to, Nullable<bool> ext, Nullable<bool> obs, Nullable<bool> paris, Nullable<bool> avail)
        {
            var courseParameter = course.HasValue ?
                new ObjectParameter("course", course) :
                new ObjectParameter("course", typeof(int));
    
            var trainerParameter = trainer.HasValue ?
                new ObjectParameter("trainer", trainer) :
                new ObjectParameter("trainer", typeof(int));
    
            var locationParameter = location.HasValue ?
                new ObjectParameter("location", location) :
                new ObjectParameter("location", typeof(int));
    
            var fromParameter = from.HasValue ?
                new ObjectParameter("from", from) :
                new ObjectParameter("from", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("to", to) :
                new ObjectParameter("to", typeof(System.DateTime));
    
            var extParameter = ext.HasValue ?
                new ObjectParameter("ext", ext) :
                new ObjectParameter("ext", typeof(bool));
    
            var obsParameter = obs.HasValue ?
                new ObjectParameter("obs", obs) :
                new ObjectParameter("obs", typeof(bool));
    
            var parisParameter = paris.HasValue ?
                new ObjectParameter("paris", paris) :
                new ObjectParameter("paris", typeof(bool));
    
            var availParameter = avail.HasValue ?
                new ObjectParameter("avail", avail) :
                new ObjectParameter("avail", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sess>("search_session", courseParameter, trainerParameter, locationParameter, fromParameter, toParameter, extParameter, obsParameter, parisParameter, availParameter);
        }
    
        public virtual ObjectResult<Sess> search_session(Nullable<int> course, Nullable<int> trainer, Nullable<int> location, Nullable<System.DateTime> from, Nullable<System.DateTime> to, Nullable<bool> ext, Nullable<bool> obs, Nullable<bool> paris, Nullable<bool> avail, MergeOption mergeOption)
        {
            var courseParameter = course.HasValue ?
                new ObjectParameter("course", course) :
                new ObjectParameter("course", typeof(int));
    
            var trainerParameter = trainer.HasValue ?
                new ObjectParameter("trainer", trainer) :
                new ObjectParameter("trainer", typeof(int));
    
            var locationParameter = location.HasValue ?
                new ObjectParameter("location", location) :
                new ObjectParameter("location", typeof(int));
    
            var fromParameter = from.HasValue ?
                new ObjectParameter("from", from) :
                new ObjectParameter("from", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("to", to) :
                new ObjectParameter("to", typeof(System.DateTime));
    
            var extParameter = ext.HasValue ?
                new ObjectParameter("ext", ext) :
                new ObjectParameter("ext", typeof(bool));
    
            var obsParameter = obs.HasValue ?
                new ObjectParameter("obs", obs) :
                new ObjectParameter("obs", typeof(bool));
    
            var parisParameter = paris.HasValue ?
                new ObjectParameter("paris", paris) :
                new ObjectParameter("paris", typeof(bool));
    
            var availParameter = avail.HasValue ?
                new ObjectParameter("avail", avail) :
                new ObjectParameter("avail", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sess>("search_session", mergeOption, courseParameter, trainerParameter, locationParameter, fromParameter, toParameter, extParameter, obsParameter, parisParameter, availParameter);
        }
    
        public virtual ObjectResult<Staff> search_staff(string staff_code, string name, string job_Title, Nullable<int> mhc, string borough, Nullable<int> service, Nullable<int> team, Nullable<int> line_man, Nullable<int> cohort, Nullable<bool> left, Nullable<bool> ext)
        {
            var staff_codeParameter = staff_code != null ?
                new ObjectParameter("staff_code", staff_code) :
                new ObjectParameter("staff_code", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var job_TitleParameter = job_Title != null ?
                new ObjectParameter("job_Title", job_Title) :
                new ObjectParameter("job_Title", typeof(string));
    
            var mhcParameter = mhc.HasValue ?
                new ObjectParameter("mhc", mhc) :
                new ObjectParameter("mhc", typeof(int));
    
            var boroughParameter = borough != null ?
                new ObjectParameter("borough", borough) :
                new ObjectParameter("borough", typeof(string));
    
            var serviceParameter = service.HasValue ?
                new ObjectParameter("service", service) :
                new ObjectParameter("service", typeof(int));
    
            var teamParameter = team.HasValue ?
                new ObjectParameter("team", team) :
                new ObjectParameter("team", typeof(int));
    
            var line_manParameter = line_man.HasValue ?
                new ObjectParameter("line_man", line_man) :
                new ObjectParameter("line_man", typeof(int));
    
            var cohortParameter = cohort.HasValue ?
                new ObjectParameter("cohort", cohort) :
                new ObjectParameter("cohort", typeof(int));
    
            var leftParameter = left.HasValue ?
                new ObjectParameter("left", left) :
                new ObjectParameter("left", typeof(bool));
    
            var extParameter = ext.HasValue ?
                new ObjectParameter("ext", ext) :
                new ObjectParameter("ext", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Staff>("search_staff", staff_codeParameter, nameParameter, job_TitleParameter, mhcParameter, boroughParameter, serviceParameter, teamParameter, line_manParameter, cohortParameter, leftParameter, extParameter);
        }
    
        public virtual ObjectResult<Staff> search_staff(string staff_code, string name, string job_Title, Nullable<int> mhc, string borough, Nullable<int> service, Nullable<int> team, Nullable<int> line_man, Nullable<int> cohort, Nullable<bool> left, Nullable<bool> ext, MergeOption mergeOption)
        {
            var staff_codeParameter = staff_code != null ?
                new ObjectParameter("staff_code", staff_code) :
                new ObjectParameter("staff_code", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var job_TitleParameter = job_Title != null ?
                new ObjectParameter("job_Title", job_Title) :
                new ObjectParameter("job_Title", typeof(string));
    
            var mhcParameter = mhc.HasValue ?
                new ObjectParameter("mhc", mhc) :
                new ObjectParameter("mhc", typeof(int));
    
            var boroughParameter = borough != null ?
                new ObjectParameter("borough", borough) :
                new ObjectParameter("borough", typeof(string));
    
            var serviceParameter = service.HasValue ?
                new ObjectParameter("service", service) :
                new ObjectParameter("service", typeof(int));
    
            var teamParameter = team.HasValue ?
                new ObjectParameter("team", team) :
                new ObjectParameter("team", typeof(int));
    
            var line_manParameter = line_man.HasValue ?
                new ObjectParameter("line_man", line_man) :
                new ObjectParameter("line_man", typeof(int));
    
            var cohortParameter = cohort.HasValue ?
                new ObjectParameter("cohort", cohort) :
                new ObjectParameter("cohort", typeof(int));
    
            var leftParameter = left.HasValue ?
                new ObjectParameter("left", left) :
                new ObjectParameter("left", typeof(bool));
    
            var extParameter = ext.HasValue ?
                new ObjectParameter("ext", ext) :
                new ObjectParameter("ext", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Staff>("search_staff", mergeOption, staff_codeParameter, nameParameter, job_TitleParameter, mhcParameter, boroughParameter, serviceParameter, teamParameter, line_manParameter, cohortParameter, leftParameter, extParameter);
        }
    
        public virtual ObjectResult<Team> search_team(string name, Nullable<int> mhc, string borough, Nullable<int> service, Nullable<int> leader, Nullable<int> cohort, Nullable<bool> notrain, Nullable<bool> hasMembers)
        {
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var mhcParameter = mhc.HasValue ?
                new ObjectParameter("mhc", mhc) :
                new ObjectParameter("mhc", typeof(int));
    
            var boroughParameter = borough != null ?
                new ObjectParameter("borough", borough) :
                new ObjectParameter("borough", typeof(string));
    
            var serviceParameter = service.HasValue ?
                new ObjectParameter("service", service) :
                new ObjectParameter("service", typeof(int));
    
            var leaderParameter = leader.HasValue ?
                new ObjectParameter("leader", leader) :
                new ObjectParameter("leader", typeof(int));
    
            var cohortParameter = cohort.HasValue ?
                new ObjectParameter("cohort", cohort) :
                new ObjectParameter("cohort", typeof(int));
    
            var notrainParameter = notrain.HasValue ?
                new ObjectParameter("notrain", notrain) :
                new ObjectParameter("notrain", typeof(bool));
    
            var hasMembersParameter = hasMembers.HasValue ?
                new ObjectParameter("hasMembers", hasMembers) :
                new ObjectParameter("hasMembers", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Team>("search_team", nameParameter, mhcParameter, boroughParameter, serviceParameter, leaderParameter, cohortParameter, notrainParameter, hasMembersParameter);
        }
    
        public virtual ObjectResult<Team> search_team(string name, Nullable<int> mhc, string borough, Nullable<int> service, Nullable<int> leader, Nullable<int> cohort, Nullable<bool> notrain, Nullable<bool> hasMembers, MergeOption mergeOption)
        {
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var mhcParameter = mhc.HasValue ?
                new ObjectParameter("mhc", mhc) :
                new ObjectParameter("mhc", typeof(int));
    
            var boroughParameter = borough != null ?
                new ObjectParameter("borough", borough) :
                new ObjectParameter("borough", typeof(string));
    
            var serviceParameter = service.HasValue ?
                new ObjectParameter("service", service) :
                new ObjectParameter("service", typeof(int));
    
            var leaderParameter = leader.HasValue ?
                new ObjectParameter("leader", leader) :
                new ObjectParameter("leader", typeof(int));
    
            var cohortParameter = cohort.HasValue ?
                new ObjectParameter("cohort", cohort) :
                new ObjectParameter("cohort", typeof(int));
    
            var notrainParameter = notrain.HasValue ?
                new ObjectParameter("notrain", notrain) :
                new ObjectParameter("notrain", typeof(bool));
    
            var hasMembersParameter = hasMembers.HasValue ?
                new ObjectParameter("hasMembers", hasMembers) :
                new ObjectParameter("hasMembers", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Team>("search_team", mergeOption, nameParameter, mhcParameter, boroughParameter, serviceParameter, leaderParameter, cohortParameter, notrainParameter, hasMembersParameter);
        }
    
        public virtual ObjectResult<user_details_Result> user_details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<user_details_Result>("user_details");
        }
    }
}
