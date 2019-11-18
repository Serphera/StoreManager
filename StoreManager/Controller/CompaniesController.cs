using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManager.Models;
using StoreManager.Data;
using StoreManager.Data.Validation;

namespace StoreManager.Controller
{
    public class CompaniesController : Microsoft.AspNetCore.Mvc.Controller {


        private readonly ICompanyService<Companies> _service;


        public CompaniesController(StoreManagerContext repo) {

            _service = new CompanyService<Companies>(repo, new CompanyValidator());
        }


        // GET: Companies
        public async Task<IActionResult> Index() {

            return View(await _service.ListEntities());
        }


        // GET: Companies/Details/5
        public async Task<IActionResult> Details(Guid? id) {

            return View(await _service.Get(id));
        }


        // GET: Companies/Create
        public IActionResult Create() {
            return View();
        }


        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OrganizationNumber,Notes")] Companies companies) {

            if (await _service.Create(companies)) {

                return RedirectToAction(nameof(Index));
            }

            return View(companies);
        }


        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {

            return View(await _service.Get(id));
        }


        // POST: Companies/Edit/5   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("Id,Name,OrganizationNumber,Notes")] Companies companies) {

            if (!await _service.Update(companies)) {

                return View(companies);
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {

            return View(await _service.Get(id));
        }


        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id, Companies company) {

            await _service.Remove(company);

            return RedirectToAction(nameof(Index));
        }


        private bool CompaniesExists(Guid id)
            => _service.FirstOrDefault(e => e.Id == id).Result != null;


    }
}
