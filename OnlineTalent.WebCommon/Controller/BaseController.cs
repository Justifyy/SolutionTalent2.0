using OnlineTalent.Core.Models;
using OnlineTalent.WebCommon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineTalent.WebCommon.Controller
{
    public class BaseController<T> :  System.Web.Mvc.Controller where T: class,new()
    {
        // GET: Customers
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            ReturnOutput returnOutput = new ReturnOutput();

            if (id > 0)// IsActive = true yap Delete
            {
                ControllerPageServices<T,T>.SetDeletedWithUpdate(id, true);
            }

            var result = ControllerPageServices<T, T>.GetAllData("");

            return View(result);
        }

        [HttpGet]
        public ActionResult Form(int id)
        {
            T entities = new T();

            if (id == 0)
            {
                return View(entities);
            }

            var repository = BaseCommonRepository<T>.BaseRepository();

            ReturnOutput returnOutput = new ReturnOutput();
            var result = repository.FindById(id, ref returnOutput);

            return View();
        }

        [HttpPost]
        public ActionResult SaveAndUpdate(T entity)
        {
            var repository = BaseCommonRepository<T>.BaseRepository();

            ReturnOutput returnOutput = new ReturnOutput();

            int id = (int)entity.GetType().GetProperty("Id").GetValue(entity);

            if (id == 0)
            {
                var result = repository.Add(entity, ref returnOutput);
            }
            else
            {
                repository.Update(entity, ref returnOutput);
            }

            ViewBag.Status = returnOutput.ErrorMessage;

            return View();
        }
    }
}
