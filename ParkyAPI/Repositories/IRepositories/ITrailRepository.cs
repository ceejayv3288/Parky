using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repositories.IRepositories
{
    public interface ITrailRepository
    {
        ICollection<TrailModel> GetTrails();
        ICollection<TrailModel> GetTrailsInNationalPark(int nationalParkId);
        TrailModel GetTrail(int trailId);
        bool TrailExists(string name);
        bool TrailExists(int id);
        bool CreateTrail(TrailModel trail);
        bool UpdateTrail(TrailModel trail);
        bool DeleteTrail(TrailModel trail);
        bool Save();
    }
}
