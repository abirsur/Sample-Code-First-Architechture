using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPS08082016.Core.EntityModel.Base;

namespace APPS08082016.Core.EntityModel
{
    public class EmployeeInfoEntity:BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime JoinDate { get; set; }
        public int NoticePeriod { get; set; }
        public string Department { get; set; }
    }
}
