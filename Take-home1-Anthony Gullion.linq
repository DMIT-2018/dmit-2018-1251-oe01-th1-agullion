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
					.Count(pc => pc.Required == true) >= 22)
	.Select(x => new
	{
		School = x.Schools.SchoolName,
		Program = x.ProgramName,
		RequiredCourseCount = x.ProgramCourses
								.Count(pc => pc.Required == true),
		OptionalCourseCount = x.ProgramCourses
								.Count(pc => pc.Required != true)
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
	
//Question 4
Employees
	.Where(x => x.Position.Description == "Instructor" && x.ReleaseDate == null && x.ClassOfferings.Any())
	.OrderByDescending(x => x.ClassOfferings.Count())
	.ThenBy(x => x.LastName)
	.Select(x => new 
	{
		ProgramName = x.Program.ProgramName,
		FullName = x.FirstName + " " + x.LastName,
		WorkLoad = (x.ClassOfferings.Count() <= 8 && x.ClassOfferings.Any()) ? "Low" :
						(x.ClassOfferings.Count() <= 24 && x.ClassOfferings.Any()) ? "Med" : "High"
	})
	.Dump();


//Question 5
Clubs
	.Select(x => new 
	{
		Supervisor = x.EmployeeID == null ? "Unknown" : x.Employee.FirstName + " " + x.Employee.LastName,
		Club = x.ClubName,
		MemberCount = x.ClubMembers.Count(),
		Activities = x.ClubActivities.Count == 0 ? "None Schedule" : x.ClubActivities.Count().ToString()
	})
	.OrderByDescending(x => x.MemberCount)
	.Dump();