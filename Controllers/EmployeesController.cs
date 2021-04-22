﻿using Microsoft.AspNetCore.Http;
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
    public class EmployeesController : ControllerBase
    {
        private tuntidbContext db = new tuntidbContext();

        [HttpGet]
        [Route("")]
        public ActionResult GetAllActiveEmployees()
        {

            try
            {
                var emp = (from e in db.Employees where e.Active == true select e).ToList();

                return Ok(emp);
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
    }
}
