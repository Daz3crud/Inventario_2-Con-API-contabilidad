using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventario_2.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;


namespace Inventario_2.Controllers
{
    public class AsientosContablesController : Controller
    {
        private readonly ContabilidadService _contabilidadService;

        public AsientosContablesController(ContabilidadService contabilidadService)
        {
            _contabilidadService = contabilidadService;
        }



        public async Task<IActionResult> Index()
        {
            int auxiliarId = 4; // Cambiar el ID del auxiliar según sea necesario
            var entradasContables = await _contabilidadService.ObtenerEntradasContablesxAuxAsync(auxiliarId);
            return View(entradasContables);
        }

        // Otros métodos del controlador
    



        public async Task<IActionResult> Crear()
        {
            var estados = await _contabilidadService.ObtenerEstadosContablesAsync();
            var monedas = await _contabilidadService.ObtenerMonedasAsync();
            var tiposMovimiento = await _contabilidadService.ObtenerTiposMovimientoAsync();
            var cuentasContables = await _contabilidadService.ObtenerCuentasContablesAsync();

            // Pasar objetos completos en lugar de solo descripciones
            ViewData["Estados"] = estados;
            ViewData["Monedas"] = monedas;
            ViewData["TiposMovimiento"] = tiposMovimiento;
            ViewData["CuentasContables"] = cuentasContables;

            var asiento = new AsientoContable();
            return View(asiento);
        }


        [HttpPost]
        public async Task<IActionResult>Crear(AsientoContable asiento)
        {
            // Procesar el formulario de creación
            if (ModelState.IsValid)
            {
                var response = await _contabilidadService.CrearAsientoContableAsync(asiento);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear el asiento contable.");
                }
            }

            // Obtener datos necesarios para los selectores
            var estados = await _contabilidadService.ObtenerEstadosContablesAsync();
            var monedas = await _contabilidadService.ObtenerMonedasAsync();
            var tiposMovimiento = await _contabilidadService.ObtenerTiposMovimientoAsync();
            var cuentasContables = await _contabilidadService.ObtenerCuentasContablesAsync();

            // Pasar los datos a la vista
            ViewData["Estados"] = estados.Select(e => e.Descripcion).ToList();
            ViewData["Monedas"] = monedas.Select(m => m.Descripcion).ToList();
            ViewData["TiposMovimiento"] = tiposMovimiento.Select(t => t.Descripcion).ToList();
            ViewData["CuentasContables"] = cuentasContables.Select(c => c.Descripcion).ToList();

            // Si hay errores de validación o el proceso de creación falla, regresar a la vista de creación
            return View(asiento);
        }


        public async Task<IActionResult> Editar(int id)
        {
            var asiento = await _contabilidadService.ObtenerAsientoContableAsync(id);
            return View(asiento);
        }

      

        public async Task<IActionResult> Eliminar(int id)
        {
            var asiento = await _contabilidadService.ObtenerAsientoContableAsync(id);
            return View(asiento);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            var response = await _contabilidadService.EliminarAsientoContableAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al eliminar el asiento contable.");
                return RedirectToAction("Eliminar", new { id });
            }
        }
    }
}