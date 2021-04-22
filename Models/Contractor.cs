using System;
using System.Collections.Generic;

#nullable disable

namespace TimesheetBackend2021.Models
{
    public partial class Contractor
    {
        public Contractor()
        {
            Employees = new HashSet<Employee>();
            Timesheets = new HashSet<Timesheet>();
        }

        public int IdContractor { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string VatId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}
