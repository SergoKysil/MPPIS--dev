using Application.Dto;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.RDBMS;
using Domain.RDBMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class DayPriceService : IDayPriceService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DayPrice> _dayPriceRepository;

        public DayPriceService(IMapper mapper, IRepository<DayPrice> repository)
        {
            this._mapper = mapper;
            this._dayPriceRepository = repository;
        }

        public async Task<AddDayPriceDto> AddDayPrice(AddDayPriceDto addDayPriceDto)
        {
            var price = _mapper.Map<DayPrice>(addDayPriceDto);
            _dayPriceRepository.Add(price);
            await _dayPriceRepository.SaveChangesAsync();
            return _mapper.Map<AddDayPriceDto>(price);
        }

        public async Task<IEnumerable<DayPriceDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<DayPriceDto>>(await _dayPriceRepository.GetAll().ToListAsync());
        }

        public async Task<DayPriceDto> GetById(int id)
        {
            return _mapper.Map<DayPriceDto>(await _dayPriceRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task Update(DayPriceDto dayPriceDto)
        {
            var newDayPrice = _mapper.Map<DayPrice>(dayPriceDto);
            _dayPriceRepository.Update(newDayPrice);
            await _dayPriceRepository.SaveChangesAsync();
        }
    }
}
