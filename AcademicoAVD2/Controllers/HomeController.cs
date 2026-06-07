using Microsoft.AspNetCore.Mvc;

namespace AcademicoAVD2.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
