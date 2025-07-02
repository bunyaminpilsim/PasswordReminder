using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordReminder.Business.Interfaces;
using PasswordReminder.Entities;
using PasswordReminder.UI.Models;
using System.Security.Claims;

namespace PasswordReminder.UI.Controllers
{
    [Authorize]
    public class PasswordsController : Controller
    {
        private readonly IPasswordService _passwordService;
        private readonly ICategoryService _categoryService;

        public PasswordsController(IPasswordService passwordService, ICategoryService categoryService)
        {
            _passwordService = passwordService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var passwords = await _passwordService.GetPasswordsByUserEmailAsync(email ?? string.Empty);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                passwords = passwords.Where(p =>
                    p.Title.ToLower().Contains(search) ||
                    p.Username.ToLower().Contains(search) ||
                    p.Url.ToLower().Contains(search)).ToList();
            }

            var model = passwords.Select(p => new PasswordCardViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Url = p.Url,
                Username = p.Username,
                CategoryName = p.Category?.Name
            }).ToList();

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories;
            return View(new PasswordCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PasswordCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(model);
            }

            var email = User.FindFirstValue(ClaimTypes.Name);
            var user = await _passwordService.GetUserByEmailAsync(email??string.Empty); 

            var category = await _categoryService.AddIfNotExistsAsync(model.CategoryName);

            var newRecord = new PasswordRecord
            {
                Title = model.Title,
                Url = model.Url,
                Username = model.Username,
                Password = model.Password,
                UserId = user.Id,
                CategoryId = category.Id
            };

            await _passwordService.CreateAsync(newRecord);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var record = await _passwordService.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            var model = new PasswordCreateViewModel
            {
                Title = record.Title,
                Url = record.Url,
                Username = record.Username,
                Password = record.Password,
                CategoryName = record.Category?.Name ?? string.Empty
            };

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PasswordCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(model);
            }

            var record = await _passwordService.GetByIdAsync(id);
            if (record == null)
                return NotFound();

            var email = User.FindFirstValue(ClaimTypes.Name);
            var user = await _passwordService.GetUserByEmailAsync(email??string.Empty);
            var category = await _categoryService.AddIfNotExistsAsync(model.CategoryName);

            record.Title = model.Title;
            record.Url = model.Url;
            record.Username = model.Username;
            record.Password = model.Password;
            record.CategoryId = category.Id;
            record.UserId = user.Id;

            await _passwordService.UpdateAsync(record);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _passwordService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
