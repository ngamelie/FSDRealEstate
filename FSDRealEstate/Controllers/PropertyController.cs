using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using FSDRealEstate.Models;
using FSDRealEstate.ViewModel;
using FSDRealEstate.Repository;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using Microsoft.CodeAnalysis;

using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage;
using Azure.Core;
using System.IO.Compression;

using System.IO;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


using System.Security.Policy;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Extensions.Hosting.Internal;

namespace FSDRealEstate.Controllers
{
    [Route("Property")]
    public class PropertyController : Controller
    {
        private readonly IProperty _property;
        private readonly ICategory _category;
        private readonly IImage _image;
        private readonly string AzureUrl = "https://fsdimage.blob.core.windows.net/realestate/";

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
            ViewData["AzureUrl"] = AzureUrl;
            return View();
        }

        [Route("Sell")]
        public ViewResult Sell()
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
            ViewData["AzureUrl"] = AzureUrl;
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

        [Route("Details")]
        public async Task<IActionResult> Details(int id)
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

            List<Image> imgList = _image.GetAll().ToList();
            var img = from i in imgList
                    where i.Property_id == obj.Id
                    select i;
            List<Image> l = img.ToList();

            PropertyViewModel propertyViewModel = new PropertyViewModel();
            propertyViewModel.Id = id;  
            propertyViewModel.Address = obj.Address;
            propertyViewModel.Price = obj.Price;
            propertyViewModel.Description = obj.Description;
            propertyViewModel.Category_id = obj.Category_id;
            propertyViewModel.Status = obj.Status;
            propertyViewModel.Location = obj.Location;
            propertyViewModel.Owner_id = obj.Owner_id;
            propertyViewModel.imgName = (l.Count > 0) ? l[0].ImageUrl : "noimage.jpg";
            ViewBag.obj = propertyViewModel;
            ViewData["AzureUrl"] = AzureUrl;
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

            return RedirectToAction(nameof(Sell));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateOne([Bind("Category_id,Owner_id,Address,Price,Status,Description,Location")] Property obj)
        {
            if (ModelState.IsValid)
            {
                _property.Create(obj);
                return RedirectToAction(nameof(Sell));
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
                return RedirectToAction(nameof(Sell));
            }

            return View("Update", new { id = obj.Id });
        }

        [HttpPost]
        [Route("AddImage")]
        public async Task<IActionResult> AddImage(ImageViewModel obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Photo == null)
                    return View("AddImage", new { id = obj.Property_id });
                Image img = new Image();
                img.Property_id = obj.Property_id;
                img.ImageUrl = obj.Photo.FileName;

                _image.DeleteList(img.Property_id);
                _image.Create(img);
                _image.uploadBlob(obj.Photo);

                return RedirectToAction(nameof(Sell));
            }

            return View("AddImage", new { id = obj.Property_id });
        }



    }
}
