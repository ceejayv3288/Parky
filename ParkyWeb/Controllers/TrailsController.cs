using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkyWeb.Models;
using ParkyWeb.Models.ViewModels;
using ParkyWeb.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class TrailsController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly ITrailRepository _trailRepository;

        public TrailsController(INationalParkRepository nationalParkRepository, ITrailRepository trailRepository)
        {
            _nationalParkRepository = nationalParkRepository;
            _trailRepository = trailRepository;
        }

        public IActionResult Index()
        {
            return View(new Trail() { });
        }

        public async Task<IActionResult> GetAllTrails()
        {
            return Json(new { data = await _trailRepository.GetAllAsync(StaticDetails.TrailAPIPath) });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalPark> nationalParkList = await _nationalParkRepository.GetAllAsync(StaticDetails.NationalParkAPIPath);

            TrailsViewModel objVM = new TrailsViewModel()
            {
                NationalParkList = nationalParkList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(objVM);
            }

            //Flow will come here for update
            objVM.Trail = await _trailRepository.GetAsync(StaticDetails.TrailAPIPath, id.GetValueOrDefault());
            if (objVM == null)
            {
                return NotFound();
            }
            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailsViewModel obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Trail.Id == 0)
                {
                    await _trailRepository.CreateAsync(StaticDetails.TrailAPIPath, obj.Trail);
                }
                else
                {
                    await _trailRepository.UpdateAsync(StaticDetails.TrailAPIPath + obj.Trail.Id, obj.Trail);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _nationalParkRepository.DeleteAsync(StaticDetails.TrailAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful"});
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}
