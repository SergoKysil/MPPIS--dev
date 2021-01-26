using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IDayPriceService
    {
        Task<DayPriceDto> GetById(int id);

        Task <IEnumerable<DayPriceDto>> GetAll();

        Task<AddDayPriceDto> AddDayPrice(AddDayPriceDto addDayPriceDto);

        Task Update(DayPriceDto dayPriceDto);
    }
}
