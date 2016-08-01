drop procedure train.search_course
go

create procedure train.search_course(
	@name varchar(80) = null,
	@paris bit = null,
	@ch bit = null,
	@ext bit = 0,
	@obs bit = 0)
as	
begin
	select ID, Course CourseName, [Length], Notes, [External], Paris, Template, FullColour, BookedColour, EmptyColour, Obselete, Child_Health 
	from train.Courses
	where (@name is null or @name = ''  or Course = @name or Course like '%'+@name+'%')
		and (@paris is null or Paris = @paris)
		and (@ch is null or Child_Health = @ch)
		and (@ext is null or [External] = @ext)
		and (@obs is null or Obselete = @obs)
	order by Course
	
end