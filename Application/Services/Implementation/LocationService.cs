using Application.Dto;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.RDBMS;
using Domain.RDBMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Location> _locationRepository;

        public LocationService(IMapper mapper, IRepository<Location> repository)
        {
            this._mapper = mapper;
            this._locationRepository = repository;
        }

        public async Task<LocationDto> AddNewLocation(LocationDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            _locationRepository.Add(location);
            await _locationRepository.SaveChangesAsync();
            return _mapper.Map<LocationDto>(location);
        }
    }
}
