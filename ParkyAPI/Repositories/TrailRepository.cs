using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repositories
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _db;

        public TrailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTrail(TrailModel trail)
        {
            _db.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(TrailModel trail)
        {
            _db.Trails.Remove(trail);
            return Save();
        }

        public TrailModel GetTrail(int trailId)
        {
            return _db.Trails.Include(c => c.NationalPark).FirstOrDefault(x => x.Id == trailId);
        }

        public ICollection<TrailModel> GetTrails()
        {
            return _db.Trails.Include(c => c.NationalPark).OrderBy(x => x.Name).ToList();
        }

        public bool TrailExists(string name)
        {
            bool value = _db.Trails.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool TrailExists(int id)
        {
            return _db.Trails.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrail(TrailModel trail)
        {
            _db.Trails.Update(trail);
            return Save();
        }

        public ICollection<TrailModel> GetTrailsInNationalPark(int nationalParkId)
        {
            return _db.Trails.Include(c => c.NationalPark).Where(c => c.NationalParkId == nationalParkId).ToList();
        }
    }
}
