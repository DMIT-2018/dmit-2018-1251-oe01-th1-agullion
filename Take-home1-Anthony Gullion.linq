<Query Kind="Statements">
  <Connection>
    <ID>7279801f-7140-4aa8-987c-a4c50b1dee9e</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-2M0366G</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>StartTed-2025-Sept</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

//Question 1
ClubActivities
	.Where(x => x.CampusVenue.Location != "Scheduled Room" && x.StartDate > new DateTime(2025, 01, 01) && x.Name != "BTech Club meeting")
	.Select(x => new 
	{
		StartDate = x.StartDate,
		Location = x.CampusVenue.Location,
		Club = x.Club.ClubName,
		Activity = x.Name		
	})
	.OrderBy(x => x.StartDate)
	.Dump();

	
//Question 2
Programs
	.Where(x => x.ProgramCourses
					.Where(pc => pc.Required == true)
					.Count() >= 22)
	.Select(x => new
	{
		School = x.Schools.SchoolName,
		Program = x.ProgramName,
		RequiredCourseCount = x.ProgramCourses
								.Where(pc => pc.Required == true)
								.Count(),
		OptionalCourseCount = x.ProgramCourses
								.Where(pc => pc.Required != true)
								.Count()
	})
	.OrderBy(x => x.Program)
	.Dump();
	
//Question 3
Students
	.Where(x => x.StudentPayments.Count() == 0 && x.Countries.CountryName != "Canada")
	.OrderBy(x => x.LastName)
	.Select(x => new 
	{
		StudentNumber = x.StudentNumber,
		CountryName = x.Countries.CountryName,
		FullName = $"{x.FirstName} {x.LastName}",
		ClubMembershipCount = x.ClubMembers.Count() == 0 ? "None" : x.ClubMembers.Count().ToString()
	})
	.Dump();