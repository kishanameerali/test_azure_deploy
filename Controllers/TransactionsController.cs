using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using bank_accounts.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace bank_accounts.Controllers
{
    public class TransactionsController : Controller
    {
        private BankContext _context;

        public TransactionsController(BankContext context)
        {
            _context = context;
        }

        [Route("account/{id}")]
        public IActionResult Show(int id)
        {
            int? Userid = HttpContext.Session.GetInt32("userid");
            if(id != Userid)
            {
                return Redirect($"account/{Userid}");
            }

            User CurrentUser = _context.users
                                .Include(u => u.transactions)
                                .Where(u => u.userid == id).SingleOrDefault();

            CurrentUser.transactions = CurrentUser.transactions.OrderByDescending(tr => tr.created_at).ToList();

            ViewBag.CurrentUser = CurrentUser;
            return View();
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(Transaction cash)
        {
            int? id = HttpContext.Session.GetInt32("userid");
            User CurrentUser = _context.users.SingleOrDefault(u => u.userid == id);
            if(cash.amount < (CurrentUser.balance * -1))
            {
                ModelState.AddModelError("amount", "The amount you are trying to withdrawal exceeds the limit");
            }
            if(ModelState.IsValid)
            {
                Transaction newTransaction = new Transaction();
                {
                    newTransaction.amount = cash.amount;
                    newTransaction.created_at = DateTime.Now;
                    newTransaction.updated_at = DateTime.Now;
                    newTransaction.userid = CurrentUser.userid;
                    newTransaction.user = CurrentUser;
                    CurrentUser.balance += newTransaction.amount;
                    _context.transactions.Add(newTransaction);
                    _context.SaveChanges();
                    return Redirect($"account/{id}");
                };
            }
            return View("Show", cash);
        }
    }
}
