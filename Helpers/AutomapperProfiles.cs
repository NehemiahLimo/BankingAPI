using AutoMapper;
using BankingAPI.Data.DTO;
using BankingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Helpers
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Accounts, AccountUpdateDTO>().ReverseMap();
        }
        
    }
}
