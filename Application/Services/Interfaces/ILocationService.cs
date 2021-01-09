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
    }
}
