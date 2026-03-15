using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GymSystem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using GymSystemBLL.Services.Classes;
using GymSystemBLL.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GymSystem.Controllers;

[Authorize]
public class HomeController(IAnalyticsService _analyticsService) : Controller
{

    public async Task<IActionResult> Index()
    {
        var Analytics = await _analyticsService.GetAnalyticsData();
        return View(Analytics);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
