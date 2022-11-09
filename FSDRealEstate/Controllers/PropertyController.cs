using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using FSDRealEstate.Models;
using FSDRealEstate.Repository;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FSDRealEstate.Controllers
{
    [Route("Property")]
    public class PropertyController : Controller
    {
        private readonly IProperty _property;

        public PropertyController(IProperty property)
        {
            _property = property;
        }

        [HttpGet]
        public ViewResult Index()
        {
            IEnumerable<Property> iEnumerable = _property.GetAll();
            ViewData["Properties"] = iEnumerable.ToList();
            return View();
        }

        [Route("Create")]
        public ViewResult Create()
        {
            return View();
        }

        [Route("Update")]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Property obj = _property.GetObject(id);
            if (obj == null)
            {
                return NotFound();
            }
            ViewBag.obj = obj;
            return View();
        }

        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {

            Property obj = _property.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateOne([Bind("Category_id,Owner_id,Address,Price,Status,Description,Location")] Property obj)
        {
            if (ModelState.IsValid)
            {
                _property.Create(obj);
                return RedirectToAction(nameof(Index));
            }

            return View("Create");
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateOne([Bind("Id,Category_id,Owner_id,Address,Price,Status,Description,Location")] Property obj)
        {
            if (ModelState.IsValid)
            {
                _property.Update(obj);
                return RedirectToAction(nameof(Index));
            }

            return View("Update", new {id = obj.Id });
        }



    }
}
