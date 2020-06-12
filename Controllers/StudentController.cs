using Projekt.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student


        db_ProjektEntities dbObj = new db_ProjektEntities();
        public ActionResult Student(Student obj)
        {
            
                return View(obj);
           
        }

        [HttpPost]
        public ActionResult AddStudent(Student model)
        {
            Student obj = new Student();
            if (ModelState.IsValid)
            {
                obj.ID = model.ID;
                obj.Name = model.Name;
                obj.First_Name = model.First_Name;
                obj.Email = model.Email;
                obj.Mobile = model.Mobile;
                obj.Description = model.Description;

                if (model.ID == 0)
                {
                    dbObj.Students.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }
                ModelState.Clear();
            }
            
            return View("Student");
        }

        public ActionResult StudentList()
        {
            var res = dbObj.Students.ToList();
            return View(res);
        }

        public ActionResult Delete(int id)
        {
            var res = dbObj.Students.Where(x => x.ID == id).First();
            dbObj.Students.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.Students.ToList();
            return View("StudentList",list);
        }
    }
}