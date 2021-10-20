﻿using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;

        public NationalParksController(INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepository = nationalParkRepository;
        }

        public IActionResult Index()
        {
            return View(new NationalPark() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark obj = new NationalPark();
            if (id == null)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update
            obj = await _nationalParkRepository.GetAsync(StaticDetails.NationalParkAPIPath, id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public async Task<IActionResult> GetAllNationalParks()
        {
            return Json(new { data = await _nationalParkRepository.GetAllAsync(StaticDetails.NationalParkAPIPath) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalPark obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    obj.Picture = p1;
                }
                else
                {
                    var objFromDb = await _nationalParkRepository.GetAsync(StaticDetails.NationalParkAPIPath, obj.Id);
                    obj.Picture = objFromDb.Picture;
                }
                if (obj.Id == 0)
                {
                    await _nationalParkRepository.CreateAsync(StaticDetails.NationalParkAPIPath, obj);
                }
                else
                {
                    await _nationalParkRepository.UpdateAsync(StaticDetails.NationalParkAPIPath + obj.Id, obj);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }
    }
}
