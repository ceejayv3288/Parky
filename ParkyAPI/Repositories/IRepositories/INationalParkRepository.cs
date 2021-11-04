using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repositories.IRepositories
{
    public interface INationalParkRepository
    {
        ICollection<NationalParkModel> GetNationalParks();
        NationalParkModel GetNationalPark(int nationalParkId);
        bool NationalParkExists(string name);
        bool NationalParkExists(int id);
        bool CreateNationalPark(NationalParkModel nationalPark);
        bool UpdateNationalPark(NationalParkModel nationalPark);
        bool DeleteNationalPark(NationalParkModel nationalPark);
        bool Save();
    }
}
