using Application.Dto;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.RDBMS;
using Domain.RDBMS.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<LocationDto> GetById(int locationId)
        {
            return _mapper.Map<LocationDto>(await _locationRepository.GetAll().FirstOrDefaultAsync(x => x.Id == locationId));
        }

        public async Task Update(LocationDto locationDto)
        {
            var newLocation = _mapper.Map<Location>(locationDto);
            _locationRepository.Update(newLocation);
            await _locationRepository.SaveChangesAsync();
        }

        public async Task<LocationDto> Remove(int locationId)
        {
            var location = await _locationRepository.FindByIdAsync(locationId);
            if(location == null)
                return null;
            _locationRepository.Remove(location);
            await _locationRepository.SaveChangesAsync();
            return _mapper.Map<LocationDto>(location);
        }
    }
}
