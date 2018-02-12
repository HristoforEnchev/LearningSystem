namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Web.Areas.Admin.Models.Courses;
    using LearningSystem.Web.Controllers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesController : BaseAdminController
    {
        private readonly UserManager<User> userManager;

        public CoursesController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create()
        {
            return View(new AddCourseFormModel()
            {
                Trainers = await GetTrainersAsync(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCourseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Trainers = await GetTrainersAsync();
                return View(model);
            }





            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private async Task<IEnumerable<SelectListItem>> GetTrainersAsync()
        {
            var trainers = await userManager.GetUsersInRoleAsync(WebConstants.TrainerRole);

            return trainers
                .Select(t => new SelectListItem
                {
                    Text = t.UserName,
                    Value = t.Id

                })
                .ToList();
        }

    }
}
