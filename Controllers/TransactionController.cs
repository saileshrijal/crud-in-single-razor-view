using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SinglePageCrud.Data;
using ViewModels;
using Models;

namespace AjaxMVC.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            if (_context.Transactions == null)
            {
                return NotFound();
            }

            var transactionVM = new TransactionVM
            {
                Transactions = await _context.Transactions.ToListAsync()
            };
            if (id > 0)
            {
                var ExistingTransaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id);
                if (ExistingTransaction != null)
                {
                    transactionVM.Id = ExistingTransaction.Id;
                    transactionVM.AccountNumber = ExistingTransaction.AccountNumber;
                    transactionVM.BeneficiaryName = ExistingTransaction.BeneficiaryName;
                    transactionVM.BankName = ExistingTransaction.BankName;
                    transactionVM.SWIFTCode = ExistingTransaction.SWIFTCode;
                    transactionVM.Amount = ExistingTransaction.Amount;
                }
                else
                {
                    return NotFound();
                }
            }
            return View(transactionVM);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(TransactionVM model)
        {
            if (ModelState.IsValid)
            {
                var transaction = new TransactionModel()
                {
                    Id = model.Id,
                    AccountNumber = model.AccountNumber,
                    BeneficiaryName = model.BeneficiaryName,
                    BankName = model.BankName,
                    Amount = model.Amount,
                    SWIFTCode = model.SWIFTCode
                };
                if (transaction.Id == 0)
                {
                    await _context.AddAsync(transaction);
                }
                else
                {
                    _context.Update(transaction);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                if (_context.Transactions != null)
                {
                    var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);

                    if (transaction != null)
                    {
                        _context.Transactions.Remove(transaction);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

