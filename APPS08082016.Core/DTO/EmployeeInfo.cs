using System;

namespace APPS08082016.Core.DTO
{
    public class EmployeeInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName {
            get
            {
                if (!string.IsNullOrEmpty(MiddleName))
                {
                    return FirstName + @" " + MiddleName + @" " + LastName;
                }
                return FirstName + @" " + LastName;
            }
        }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime JoinDate { get; set; }
        public int NoticePeriod { get; set; }
        public string Department { get; set; }

        public string Experience => DateDifferenceFromCurrentDay(JoinDate);

        public string Age => DateDifferenceFromCurrentDay(DateofBirth);

        private string DateDifferenceFromCurrentDay(DateTime day)
        {
            int validYear = DateTime.Now.Year - 100;
            if (day.Year < validYear)
                return "Invaild Date";
            return (DateTime.UtcNow.Date - day.Date).Days.ToString();
        }
    }
}
