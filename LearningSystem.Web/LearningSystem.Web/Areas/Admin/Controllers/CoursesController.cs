using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    public class CoursesController : BaseAdminController
    {
        public IActionResult Create() => View();

    }
}
