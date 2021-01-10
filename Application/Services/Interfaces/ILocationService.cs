using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ILocationService
    {
        Task<LocationDto> AddNewLocation(LocationDto locationDto);

        Task<LocationDto> GetById(int locationId);

        Task Update(LocationDto locationDto);

        Task<LocationDto> Remove(int locationId);
    }
}
