﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TimesheetBackend2021.Models
{
    public partial class WorkAssignment
    {
        public WorkAssignment()
        {
            Timesheets = new HashSet<Timesheet>();
        }

        public int IdWorkAssignment { get; set; }
        public int? IdCustomer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? InProgress { get; set; }
        public DateTime? WorkStartedAt { get; set; }
        public bool? Completed { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? Active { get; set; }

        public virtual Customer IdWorkAssignmentNavigation { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}
