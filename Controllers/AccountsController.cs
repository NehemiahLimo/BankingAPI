using AutoMapper;
using BankingAPI.Data;
using BankingAPI.Data.DTO;
using BankingAPI.Data.Interfaces;
using BankingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BankingAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        
        private readonly IMapper mapper;
        public AccountsController(IMapper mapper, IUnitOfWork uow)
        {
            this.uow = uow;
            this.mapper = mapper;
            
        }

        //GET api/accounts/ --List all accounts
        [AllowAnonymous]
        [HttpGet("fetchaccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            
            var accounts = await uow.AccountsRepository.GetAllAccounts();
            return Ok(accounts);
        }

        //post api/accounts/CreateAccount
        [HttpPost("createaccount")]
        public async Task<IActionResult> CreateAccount(Accounts account)
        {
            if (await uow.AccountsRepository.AccountNoAlreadyExist(account.AccountNo))
                return BadRequest("Account Already exist.");
            //Accounts acc = new Accounts();
            account.Balance = account.Deposit;
            await uow.AccountsRepository.CreateBankAccount(account);
            await uow.SaveAsync();
            var res = new BaseResponse
            {
                ResponseCode = (int)HttpStatusCode.Created,
                ResponseMessage = "Successfully Created"
            };
            return StatusCode(201);
        }


        [HttpPut("updateAccount/{AccountNo}")]
        public async Task<IActionResult> UpdateAccount(string AccountNo, AccountUpdateDTO accDto)
        {
            var acc = await uow.AccountsRepository.FindAccount(AccountNo);
            if (acc == null)
                return BadRequest("The Account Number does not exist");
            acc.AccountName = accDto.AccountName;
            acc.AccountType = accDto.AccountType;
            acc.Balance += accDto.TopUpAmount;

            mapper.Map(accDto, acc);
                      
            await uow.SaveAsync();
            var res = new BaseResponse
            {
                ResponseCode = (int)HttpStatusCode.OK,
                ResponseMessage = "Successfully Updated"
            };
            return Ok(res);

            
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            uow.AccountsRepository.DeleteAccount(id);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpGet("findaccount/{AccountNo}")]
        public async Task<IActionResult> FetchAccountInfo(string AccountNo)
        {
            var account = await uow.AccountsRepository.FindAccount(AccountNo);
            

            return Ok(account);
        }

        [HttpGet("retrievebalance/{AccountNo}")]
        public async Task<IActionResult> FetchAccountBalance(string AccountNo)
        {
            var account = await uow.AccountsRepository.FindAccount(AccountNo);
            var balance = account.Balance;
            var res = new AccountBalanceResponseDTO
            {
                AccountNo = AccountNo,
                Balance = balance
            };


            return Ok(res);
        }

        //post api/accounts/transfer
        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(TransferHistoryDTO req)
        {
            if (await uow.AccountsRepository.CheckBalanceIsEnoughtToTransfer(req.TransferingAccountNo, req.Amount))
                return BadRequest("Cannot Initiate this transfer at the moment. Customer does not have enough money to complete transaction");
            
            var res= await uow.AccountsRepository.TransferMoney(req);
            var response = new BaseResponse
            {
                ResponseCode = (int)HttpStatusCode.Created,
                ResponseMessage = "The transfer was successfull",
            };
            
            return Ok(response);

        }



        

        [HttpGet("retrievehistory/{AccountNo}")]
        public async Task<IActionResult> RetrieveTransferHistories(string AccountNo)
        {
            var data = await uow.AccountsRepository.RetrieveTransferHistories(AccountNo);
            if (data ==null)
            {
                return NotFound();
            }
            else
            {
                
                return Ok(data);
            }
            
        }



    }
}
