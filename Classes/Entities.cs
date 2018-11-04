using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UDIRS.Entities
{
	public class Person
	{
		public Int32 Id { get; set; }
		public string Username { get; set; }
		public string Pwd { get; set; }
		public string Firstname { get; set; }
		public string Midname { get; set; }
		public string Lastname { get; set; }
		public string Fullname { get; set; }
		public string FirstnameAr { get; set; }
		public string MidnameAr { get; set; }
		public string LastnameAr { get; set; }
		public string FullnameAr { get; set; }
		public string Title { get; set; }
		public string JobTitle { get; set; }
		public string NationIqama { get; set; }
		public string ComputerNo { get; set; }
		public string WorkEmail { get; set; }
		public string PersonalEmail { get; set; }
		public string WorkPhone { get; set; }
		public string Mobile { get; set; }
		public string Gender { get; set; }
		public string JoinDateToUD { get; set; }
		public string JoinDateToDept { get; set; }
		public string RecStatus { get; set; }
		public string RecUser { get; set; }
		public DateTime RecDate { get; set; }
	}
	public class PersonRoles
	{
		public Int64 RolesID { get; set; }
		public string Shortname { get; set; }
		public string Longname { get; set; }
	}
    
}