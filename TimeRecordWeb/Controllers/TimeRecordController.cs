using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeRecordWeb.Helpers;
using TimeRecordWeb.Models;

namespace TimeRecordWeb.Controllers
{
    [Authorize]
    public class TimeRecordController : Controller
    {
        private ITimeRecordAPIClient _timeRecordAPIClient;

        public TimeRecordController(ITimeRecordAPIClient timeRecordAPIClient)
        {
            _timeRecordAPIClient = timeRecordAPIClient;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _timeRecordAPIClient.GetAllTimeRecordAsync();
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimeRecordModel timeRecordEntry)
        {
            if (ModelState.IsValid)
            {
                var viewModel = await _timeRecordAPIClient.CreateTimeRecord(timeRecordEntry);                
                return RedirectToAction(nameof(Index));
            }
            return View(timeRecordEntry);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _timeRecordAPIClient.GetTimeRecordById(id);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TimeRecordModel timeRecordEntry)
        {
            var viewModel = await _timeRecordAPIClient.UpdateTimeRecord(id, timeRecordEntry);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _timeRecordAPIClient.GetTimeRecordById(id);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viewModel = await _timeRecordAPIClient.DeleteTimeRecordById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
