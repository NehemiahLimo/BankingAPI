using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.DTO
{
    public class TransferHistoryResDTO
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
