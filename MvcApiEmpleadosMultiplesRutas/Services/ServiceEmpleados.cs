using NugetApiEmpleados;
using System.Net.Http.Headers;

namespace MvcApiEmpleadosMultiplesRutas.Services
{
    public class ServiceEmpleados
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string ApiUrl;

        public ServiceEmpleados(IConfiguration configuration)
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.ApiUrl = configuration.GetValue<string>
                ("ApiUrls:ApiEmpleados");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string request = "/api/empleados";
            List<Empleado> empleados =
                await this.CallApiAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            string request = "/api/empleados/oficios";
            List<string> oficios =
                await this.CallApiAsync<List<string>>(request);
            return oficios;
        }

        public async Task<Empleado> FindEmpleadoAsync(int idempleado)
        {
            string request = "/api/empleados/" + idempleado;
            Empleado empleado = await this.CallApiAsync<Empleado>(request);
            return empleado;
        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            string request = "/api/empleados/empleadosoficio/" + oficio;
            List<Empleado> empleados =
                await this.CallApiAsync<List<Empleado>>(request);
            return empleados;
        }
    }

}
