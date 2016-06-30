create procedure train.search_session(
	@course int = null,
	@trainer int = null,
	@location int = null,
	@from date = null,
	@to date = null,
	@ext bit = 0,
	@obs bit = null,
	@paris bit = null,
	@avail bit = null)
as	
begin
	select ID, Course CourseID, Trainer TrainerID, Location LocationID, Strt, Endt, MaxP, Notes
	from train.Sess
	where (@course is null or @course=0 or Course = @course)
		and (@trainer is null or @trainer=0 or Trainer = @trainer)
		and (@location is null or @location=0 or Location = @location)
		and (@from is null or CAST(Strt as date) >= @from)
		and (@to is null or CAST(Endt as date) <= @to)
		and (@ext is null or Course in (select ID from train.Courses where [External] = @ext))
		and (@obs is null or Course in (select ID from train.Courses where Obselete = @obs))
		and (@paris is null or Course in (select ID from train.Courses where Paris = @paris))
		and (@avail is null or (@avail = 1 and (select COUNT(*) from train.Attendance where Attendance.Sess = Sess.ID and ISNULL(Outcome, 0) not in (6, 7)) < MaxP and Strt > GETDATE())
							or (@avail = 0 and (select COUNT(*) from train.Attendance where Attendance.Sess = Sess.ID and ISNULL(Outcome, 0) not in (6, 7)) >= MaxP))
	order by case when Strt > DATEADD(mm, -1, getdate()) then 1 else 2 end, 
	case when Strt > DATEADD(mm, -1, getdate()) then Strt else null end, 
	case when Strt > DATEADD(mm, -1, getdate()) then null else Strt end desc, Course
end