using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPPIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPIS.Controllers
{
    public class AddDayPriceController : Controller
    {
        private readonly IDayPriceService _dayPriceService;


        public AddDayPriceController(IDayPriceService dayPriceService)
        {
            this._dayPriceService = dayPriceService;
        }

        public IActionResult AddDayPrice()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AddDayPriceDto>> AddDayPrice([FromForm] AddDayPriceViewModel addDayPriceViewModel)
        {

            if (ModelState.IsValid)
            {
                AddDayPriceDto addDayPriceDto = new AddDayPriceDto
                {
                    Price = addDayPriceViewModel.Price
                };

                var createdPrice = await _dayPriceService.AddDayPrice(addDayPriceDto);

                if (createdPrice != null)
                    RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Error");
            return View(addDayPriceViewModel);
        }

       
    }
}
