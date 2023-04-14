using Microsoft.AspNetCore.Mvc;
using MvcApiEmpleadosMultiplesRutas.Services;
using NugetApiEmpleados;

namespace MvcApiEmpleadosMultiplesRutas.Controllers
{
    public class EmpleadosController : Controller
    {
        private ServiceEmpleados service;

        public EmpleadosController(ServiceEmpleados service)
        {
            this.service = service;
        }

        public async Task<IActionResult> EmpleadosOficio()
        {
            ViewData["OFICIOS"] = await this.service.GetOficiosAsync();
            return View(await this.service.GetEmpleadosAsync());
        }

        [HttpPost]
        public async Task<IActionResult> EmpleadosOficio(string oficio)
        {
            ViewData["OFICIOS"] = await this.service.GetOficiosAsync();
            return View(await this.service.GetEmpleadosOficioAsync(oficio));
        }

        public async Task<IActionResult> Details(int id)
        {
            Empleado empleado = await this.service.FindEmpleadoAsync(id);
            return View(empleado);
        }
    }
}
