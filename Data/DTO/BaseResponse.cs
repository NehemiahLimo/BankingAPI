﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Data.DTO
{
    public class BaseResponse
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}
