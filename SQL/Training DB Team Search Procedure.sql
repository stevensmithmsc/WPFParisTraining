create procedure train.search_team(
	@name varchar(50) = null,
	@mhc int = null,
	@borough char(1) = null,
	@service int = null,
	@leader int = null,
	@cohort int = null,
	@notrain int = 0,
	@hasMembers bit = 1)
as	
begin
	select ID, Team, ESR, Code, Leader, Cohort, NoTrain, Dont_Migrate
	from dbo.Teams
	where (@name is null or @name = ''  or Team = @name or Team like '%'+@name+'%')
		and (@mhc is null or @mhc = 0 or ID in (select ID from Train.ServMem where [Type] = 'T' and ServID = @mhc))
		and (@borough is null or @borough = '' or ID in (select ID from Train.BoroughMem where [Type] = 'T' and Borough = @borough))
		and (@service is null or @service = 0 or ID in (select ID from Train.ServMem where [Type] = 'T' and ServID = @service))
		and (@leader is null or @leader=0 or Leader = @leader)
		and (@cohort is null or @cohort=0 or Cohort = @cohort)
		and (@notrain is null or NoTrain = @notrain)
		and (@hasMembers is null or (@hasMembers = 1 and ID in (select TeamID from train.TeamMem where Active <> 0))
								or (@hasMembers = 0 and ID not in (select TeamID from train.TeamMem where Active <> 0)))
	order by Team
	
end	