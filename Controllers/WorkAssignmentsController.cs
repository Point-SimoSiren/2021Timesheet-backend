using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetBackend2021.Models;

namespace TimesheetBackend2021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkAssignmentsController : ControllerBase
    {

        private tuntidbContext db = new tuntidbContext();

        [HttpGet]
        [Route("")]
        public ActionResult GetAllActiveWorkAssignments()
        {

            try
            {
                var wa = (from w in db.WorkAssignments where w.Active == true && w.Completed == false select w).ToList();

                return Ok(wa);
            }
            catch (Exception e)
            {

                return BadRequest("Virhe: " + e.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        [HttpPost]
        [Route("")]
        public bool StartStop(Operation op)
        {
            if (op == null)
            {
                return(false);
            }

            WorkAssignment assignment = (from w in db.WorkAssignments
                                         where (w.Active == true) &&
                                         (w.IdWorkAssignment == op.WorkAssignmentID)
                                         select w).FirstOrDefault();

            if (assignment == null)
            {
                return(false);
            }

            // Start
            else if (op.OperationType == "start")
            {

                if (assignment.InProgress == true || assignment.Completed == true)
                {
                    return(false);
                }

                assignment.InProgress = true;
                assignment.WorkStartedAt = DateTime.Now.AddHours(2);
                assignment.LastModifiedAt = DateTime.Now.AddHours(2);

                db.SaveChanges();


                Timesheet newEntry = new Timesheet()
                {
                    IdWorkAssignment = op.WorkAssignmentID,
                    StartTime = DateTime.Now.AddHours(2),
                    Active = true,
                    IdEmployee = op.EmployeeID,
                    IdCustomer = op.CustomerID,
                    CreatedAt = DateTime.Now.AddHours(2),
                    Comments = op.Comment
                };

                db.Timesheets.Add(newEntry);

                db.SaveChanges(); //<-------------- Tämä puuttui, ja siksi ei luonutkaan uutta timesheetiä. Ongelma ilmeni salakavalasti vasta STOP vaiheessa

                return(true);

            }

            // Stop
            else
            {
          
                if (assignment.InProgress == false || assignment.Completed == true)
                {
                    return(false);
                }

                assignment.InProgress = false;
                assignment.CompletedAt = DateTime.Now.AddHours(2);
                assignment.Completed = true;
                assignment.LastModifiedAt = DateTime.Now.AddHours(2);
                db.SaveChanges();

                Timesheet ts = (from t in db.Timesheets
                                             where (t.Active == true) &&
                                             (t.IdWorkAssignment == op.WorkAssignmentID)
                                             select t).FirstOrDefault();

                ts.StopTime = DateTime.Now.AddHours(2);
                ts.LastModifiedAt = DateTime.Now.AddHours(2);
                ts.Comments = op.Comment;
                db.SaveChanges();

                return(true);
            }

        }

    }
}
