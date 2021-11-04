using EnsekExercise.Dtos;
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
    [Route("meter-reading-uploads")]
    public class MeterReadingUploadsController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingUploadsController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            var uploadMeterReadingsResponse = new UploadMeterReadingsResponse();

            if (Request.Form.Files.Single().FileName.EndsWith(".csv"))
            {
                uploadMeterReadingsResponse = _meterReadingService.UploadMeterReadings(Request.Form.Files.Single());
            }

            return Ok(uploadMeterReadingsResponse);
        }
    }
}
