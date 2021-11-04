using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekExercise.Dtos
{
    public class UploadMeterReadingsResponse
    {
        public int SuccessfulUploadCount { get; set; }

        public int UnsuccessfulUploadCount { get; set; }

    }
}
