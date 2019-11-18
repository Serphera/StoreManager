using StoreManager.Data;
using StoreManager.Data.Validation;
using StoreManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreManager.Controller {

    public class StoresController : Microsoft.AspNetCore.Mvc.Controller {

        private readonly IStoresService<Stores> _service;


        public StoresController(StoreManagerContext repo) {

            _service = new StoresService<Stores>(repo, new Geodcoder("601d0423a05044"), new StoreValidator());
        }


        // GET: Stores
        public async Task<IActionResult> Index() {

            return View(await _service.ListEntities());
        }


        // GET: Stores/Details/5
        public async Task<IActionResult> Details(Guid? id) {

            return View(await _service.FirstOrDefault(m => m.Id == id));
        }


        // GET: Stores/Create
        public IActionResult Create() {

            var viewModel = new StoreViewModel {
                Companies = new SelectList(_service.GetCompanies().Result, "Id", "Name")
            };

            return View(viewModel);
        }


        // POST: Stores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreViewModel viewModel) {

            if (await _service.Create(viewModel)) {

                return RedirectToAction(nameof(Index));
            }

            viewModel.Companies = new SelectList(_service.GetCompanies().Result, "Id", "Name");
            return View(viewModel);
        }


        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(Guid id) {

            var viewModel = new StoreViewModel {
                Store = await ((StoresService<Stores>)_service).Get(id),
                Companies = new SelectList(await _service.GetCompanies(), "Id", "Name")
            };

            viewModel.SelectedCompany = viewModel.Store.CompanyId;
            return View(viewModel);
        }


        // POST: Stores/Edit/5   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StoreViewModel viewModel) {
                        
            if (await _service.Update(viewModel)) {

                return RedirectToAction(nameof(Index));                
            }

            viewModel.SelectedCompany = viewModel.Store.CompanyId;
            return View(viewModel);
        }


        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {

            return View(await _service.FirstOrDefault(m => m.Id == id));
        }


        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Stores store) {

            await _service.Remove(store);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [HttpGet]
        public async Task<object> Geocoding(string searchParam) {

            return await _service.Geocoding(searchParam);
        }


        [HttpPost]
        [HttpGet]
        public async Task<object> ReverseGeocoding(string lat, string lng) {

            return await _service.ReverseGeocoding(lat, lng);    
        }

    }
}
