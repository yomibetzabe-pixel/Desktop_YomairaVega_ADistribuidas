using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Examen_Doctores_Especialidades.Models;

namespace Examen_Doctores_Especialidades.Controllers
{
    public class DoctorController : Controller
    {
        private readonly HttpClient client = new();

        // Vista principal con Razor
        public async Task<IActionResult> Index()
        {
            var doctores = await client.GetFromJsonAsync<List<Doctor>>("http://localhost:8080/api/doctor");
            return View(doctores);
        }

        // ✅ Acción para AJAX: devuelve JSON
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctores = await client.GetFromJsonAsync<List<Doctor>>("http://localhost:8080/api/doctor");
            return Json(doctores);
        }

        // Cargar formulario de registro
        public async Task<IActionResult> Create()
        {
            var especialidades = await client.GetFromJsonAsync<List<Especialidad>>("http://localhost:8080/api/especialidad");
            ViewBag.Especialidades = especialidades;
            return View();
        }

        // Recibir doctor desde AJAX
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] Doctor doctor)
        {
            var response = await client.PostAsJsonAsync("http://localhost:8080/api/doctor", doctor);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, $"Error al registrar el doctor: {error}");
            }
        }

        // Cargar formulario de edición
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var doctorResponse = await client.GetAsync($"http://localhost:8080/api/doctor/{id}");

                if (!doctorResponse.IsSuccessStatusCode)
                {
                    var error = await doctorResponse.Content.ReadAsStringAsync();
                    return StatusCode((int)doctorResponse.StatusCode, $"Error al obtener el doctor: {error}");
                }

                var doctor = await doctorResponse.Content.ReadFromJsonAsync<Doctor>();

                var especialidades = await client.GetFromJsonAsync<List<Especialidad>>("http://localhost:8080/api/especialidad");
                ViewBag.Especialidades = especialidades;

                return View(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Excepción inesperada: {ex.Message}");
            }
        }

        // Recibir doctor actualizado desde AJAX
        [HttpPost]
        public async Task<IActionResult> EditDoctor([FromBody] Doctor doctor)
        {
            var response = await client.PutAsJsonAsync($"http://localhost:8080/api/doctor/{doctor.IdDoctor}", doctor);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, $"Error al actualizar el doctor: {error}");
            }
        }
    }
}




