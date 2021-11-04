using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekExercise.Models.Mappings
{
    public class MeterReadingMapping : ClassMap<MeterReading>
    {
        public MeterReadingMapping()
        {
            Map(m => m.AccountId);
            Map(m => m.MeterReadingDateTime).TypeConverterOption.Format("dd/MM/yyyy HH:mm");
            Map(m => m.MeterReadValue);
        }
    }
}
