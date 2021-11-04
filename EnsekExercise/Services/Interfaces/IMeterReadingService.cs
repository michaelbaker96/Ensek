using EnsekExercise.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekExercise.Services
{
    public interface IMeterReadingService
    {
        UploadMeterReadingsResponse UploadMeterReadings(IFormFile csvFile);

        IEnumerable<MeterReading> GetAllMeterReadings();

        MeterReading GetMeterReadingById(int id);
    }
}
