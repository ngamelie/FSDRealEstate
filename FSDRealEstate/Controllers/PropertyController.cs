using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using FSDRealEstate.Models;
using FSDRealEstate.Repository;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using Microsoft.CodeAnalysis;

namespace FSDRealEstate.Controllers
{
    [Route("Property")]
    public class PropertyController : Controller
    {
        private readonly IProperty _property;
        private readonly ICategory _category;
        private readonly IImage _image;

        public PropertyController(IProperty property, ICategory category, IImage image)
        {
            _property = property;
            _category = category;
            _image = image; 

        }

        [HttpGet]
        public ViewResult Index()
        {
            List<Property> propertyList = _property.GetAll().ToList();
            List<Image> imageList = _image.GetAll().ToList();

            var innerJoin = from p in propertyList
                            join i in imageList
                            on p.Id equals i.Property_id
                            into p_i
                            from img in p_i.DefaultIfEmpty()
                            select new
                            { p, img };

            ViewBag.Properties = innerJoin;
            return View();
        }

        [Route("Create")]
        public ViewResult Create()
        {
            return View();
        }

        [Route("AddImage")]
        public async Task<IActionResult> AddImage(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Property_id"] = id;
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
            IEnumerable<Category> cateList = _category.GetAll();
           // ViewBag.cateList = cateList.ToList();

            List<SelectListItem> cateSelect = new List<SelectListItem>();

            foreach (var item in cateList)
            {
                if(item.Id == obj.Category_id)
                {
                    cateSelect.Add(new SelectListItem { Text = item.CategoryName, Value = item.Id.ToString(), Selected = true});
                }
                else
                {
                    cateSelect.Add(new SelectListItem { Text = item.CategoryName, Value = item.Id.ToString() });
                }
                
            }

            ViewBag.cateSelect = cateSelect;

            return View();
        }

        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            _image.DeleteList(id);
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
        public async Task<IActionResult> UpdateOne(int cateSelect, [Bind("Id,Category_id,Owner_id,Address,Price,Status,Description,Location")] Property obj)
        {
            if (ModelState.IsValid)
            {
                obj.Category_id = cateSelect;
                _property.Update(obj);
                return RedirectToAction(nameof(Index));
            }

            return View("Update", new { id = obj.Id });
        }

        [HttpPost]
        [Route("AddImage")]
        public async Task<IActionResult> AddImage([Bind("Property_id, ImageUrl")] Image obj)
        {
            if (ModelState.IsValid)
            {
                _image.Create(obj);
                return RedirectToAction(nameof(Index));
            }

            return View("AddImage", new { id = obj.Property_id });
        }



    }
}
