﻿using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repositories
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _db;

        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _db.NationalParks.FirstOrDefault(x => x.Id == nationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(x => x.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            bool value = _db.NationalParks.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExists(int id)
        {
            return _db.NationalParks.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
