using System.ComponentModel.DataAnnotations;

namespace FileUploadApi_11.Models
{
    public class Employee
    {
        public int Id { get; set; }
        //[Required]
        //public string Name { get; set; }
        //public string  Address { get; set; }
        //public string Email { get; set; }
        //public string AccountCode { get; set; }
        //public int Age { get; set; }
        //public int  Salary  { get; set; }
        //public int ContactNo { get; set; }
        //public int CountryCode { get; set; }
        //public string BloodGroup { get; set; }
        //public string Country { get; set; }
        //public string State { get; set; }
        //public string City { get; set; }
        //public string  Department { get; set; }
        //public string Designation  { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public string Date  { get; set; }


    }
}
