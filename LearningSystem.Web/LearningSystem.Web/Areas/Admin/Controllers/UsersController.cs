namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using LearningSystem.Services.Admin;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class UsersController : Controller
    {
        private readonly IAdminUserService users;

        public UsersController(IAdminUserService users)
        {
            this.users = users;
        }


        [Area("Admin")]
        [Authorize(Roles = AdministratorRole)]
        public IActionResult Index()
        {
            return View(this.users.All());
        }
    }
}
