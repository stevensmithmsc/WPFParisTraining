create procedure train.search_staff(
	@staff_code char(10) = null,
	@name varchar(50) = null,
	@job_Title varchar(100) = null,
	@mhc int = null,
	@borough char(1) = null,
	@service int = null,
	@team int = null,
	@line_man int = null,
	@cohort int = null,
	@left bit = 0,
	@ext bit = 0)
as	
begin
	select ID, Title TitleID, Fname, Sname, PName, Gender, JobTitle, Finance, JobStatus, LineMan, Cohort CohortID, UUID, Phone, Email,
		LM, Trainer, LeftTrust, NoTraining, Bank, [External], ESRID, MName, Comments, ADAccount 
	from train.Staff
	where (@staff_code is null or @staff_code = '' or ESRID in (select cEmployee from ESR.Staff where Staff_Code = @staff_code))
		and (@name is null or @name = '' or Fname like '%'+@name+'%' or Sname like '%'+@name+'%' or PName like '%'+@name+'%' 
			or ISNULL((select top 1Titles.Title from train.Titles where Titles.ID = Staff.Title) + ' ', '') + Fname + ' ' + Sname like '%'+@name+'%')
		and (@job_Title is null or @job_Title = '' or JobTitle like '%'+@job_Title+'%')
		and (@mhc is null or @mhc = 0 or ID in (select ID from Train.ServMem where [Type] = 'S' and ServID = @mhc))
		and (@borough is null or @borough = '' or ID in (select ID from Train.BoroughMem where [Type] = 'S' and Borough = @borough))
		and (@service is null or @service = 0 or ID in (select ID from Train.ServMem where [Type] = 'S' and ServID = @service))
		and (@team is null or @team = '' or ID in (select StaffID from Train.TeamMem where TeamID = @team and [Active] <> 0))
		and (@line_man is null or @line_man = 0 or LineMan = @line_man)
		and (@cohort is null or @cohort = 0 or Cohort = @cohort or (Cohort is null and ID in (select StaffID from Train.TeamMem where TeamID in (select ID from dbo.Teams where Cohort = @cohort) and [Active] <> 0 and Main <> 0))) 
		and (@left is null or LeftTrust = @left)
		and (@ext is null or [External] = @ext)
	order by Sname, Fname
end