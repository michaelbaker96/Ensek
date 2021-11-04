using EnsekExercise.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekExercise.Controllers
{
    [ApiController]
    [Route("meter-readings")]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MeterReading>> GetMeterReadings()
        {
            var meterReadings = _meterReadingService.GetAllMeterReadings();

            return Ok(meterReadings);
        }

        [HttpGet("{id}", Name = "GetMeterReadingById")]
        public ActionResult<MeterReading> GetMeterReadingById(int id)
        {
            var meterReading = _meterReadingService.GetMeterReadingById(id);

            if (meterReading != null)
            {
                return Ok(meterReading);
            }

            return NotFound();
        }
    }
}
