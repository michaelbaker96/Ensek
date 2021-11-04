using CsvHelper;
using CsvHelper.Configuration;
using EnsekExercise.Data;
using EnsekExercise.Dtos;
using EnsekExercise.Models.Mappings;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EnsekExercise.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly AppDbContext _context;
        private readonly IAccountService _accountService;
        public MeterReadingService(AppDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public UploadMeterReadingsResponse UploadMeterReadings(IFormFile csvFile)
        {
            var meterReadings = GetMeterReadingsFromFile(csvFile);

            string pattern = @"^\d{5}$";
            Regex regex = new Regex(pattern);

            var uploadMeterReadingsResponse = new UploadMeterReadingsResponse();

            var accountIds = _accountService.GetAllAccounts().Select(x => x.AccountId).ToList();

            foreach (var meterReading in meterReadings)
            {
                if (regex.IsMatch(meterReading.MeterReadValue) && accountIds.Contains(meterReading.AccountId))
                {
                    _context.MeterReadings.AddIfNotExists(meterReading, x => x.AccountId == meterReading.AccountId && x.MeterReadingDateTime == meterReading.MeterReadingDateTime);

                    //small bit of confusion as to whether a "successful" reading, means it was successfully passed, or actually added to the Db during this specific call.
                    uploadMeterReadingsResponse.SuccessfulUploadCount++;
                }
                else
                {
                    uploadMeterReadingsResponse.UnsuccessfulUploadCount++;
                }
            }

            SaveChanges();

            return uploadMeterReadingsResponse;
        }

        private List<MeterReading> GetMeterReadingsFromFile(IFormFile csvFile)
        {
            var meterReadings = new List<MeterReading>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using (var reader = new StreamReader(csvFile.OpenReadStream()))
            using (var csvReader = new CsvReader(reader, config))
            {
                csvReader.Context.RegisterClassMap<MeterReadingMapping>();
                meterReadings = csvReader.GetRecords<MeterReading>().ToList();
            }

            return meterReadings;
        }

        public IEnumerable<MeterReading> GetAllMeterReadings()
        {
            return _context.MeterReadings.ToList();
        }

        public MeterReading GetMeterReadingById(int id)
        {
            return _context.MeterReadings.FirstOrDefault(plat => plat.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
