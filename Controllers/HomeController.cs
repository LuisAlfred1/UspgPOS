using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using UspgPOS.Models;

namespace UspgPOS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Layout"] = "_LayoutLogged"; // Layout para usuarios logueados
                var detallesEmpleados = new List<DetallesEmpleados>
                {
                    new DetallesEmpleados { Departamento = "IT", NivelExperiencia = 5, CalificacionRendimiento = 8.5, SalarioPromedio = 80000, TasaRotacion = 12.5 },
                    new DetallesEmpleados { Departamento = "RRHH", NivelExperiencia = 3, CalificacionRendimiento = 7.2, SalarioPromedio = 60000, TasaRotacion = 8.3 },
                    new DetallesEmpleados { Departamento = "Marketing", NivelExperiencia = 4, CalificacionRendimiento = 7.8, SalarioPromedio = 70000, TasaRotacion = 10.1 },
                    new DetallesEmpleados { Departamento = "Ventas", NivelExperiencia = 6, CalificacionRendimiento = 8.0, SalarioPromedio = 75000, TasaRotacion = 15.0 }
                };
                return View(detallesEmpleados);
            }
            else
            {
                ViewData["Layout"] = "_Layout"; // Layout para usuarios no logueados
                return View();
            }
        }

        public IActionResult Dashboard()
        {
			var empleados = new List<Empleado>
			{
				new Empleado { IdEmpleado = 1, Nombre = "John Doe", Departamento = "Ingeniería", Posicion = "Ingeniero de Software", AñosExperiencia = 5, Edad = 30, Salario = 75000, NivelSatisfaccion = 0.8, CalificacionDesempeno  = 4.5, HorasTrabajo = 40, FechaContratacion = DateTime.Parse("2018-05-15") },
				new Empleado { IdEmpleado = 2, Nombre = "Jane Smith", Departamento = "Marketing", Posicion = "Especialista en Marketing", AñosExperiencia = 3, Edad = 28, Salario = 55000, NivelSatisfaccion = 0.6, CalificacionDesempeno  = 3.8, HorasTrabajo = 35, FechaContratacion = DateTime.Parse("2020-07-10") },
				new Empleado { IdEmpleado = 3, Nombre = "Michael Johnson", Departamento = "Ventas", Posicion = "Gerente de Ventas", AñosExperiencia = 7, Edad = 35, Salario = 82000, NivelSatisfaccion = 0.7, CalificacionDesempeno  = 4.2, HorasTrabajo = 45, FechaContratacion = DateTime.Parse("2016-03-20") },
				new Empleado { IdEmpleado = 4, Nombre = "Emily Davis", Departamento = "Ingeniería", Posicion = "Ingeniero DevOps", AñosExperiencia = 4, Edad = 29, Salario = 70000, NivelSatisfaccion = 0.9, CalificacionDesempeno  = 4.8, HorasTrabajo = 40, FechaContratacion = DateTime.Parse("2019-09-25") },
				new Empleado { IdEmpleado = 5, Nombre = "David Brown", Departamento = "Recursos Humanos", Posicion = "Especialista en RRHH", AñosExperiencia = 2, Edad = 32, Salario = 52000, NivelSatisfaccion = 0.75, CalificacionDesempeno  = 4.1, HorasTrabajo = 37, FechaContratacion = DateTime.Parse("2021-01-15") },
				new Empleado { IdEmpleado = 6, Nombre = "Jessica Wilson", Departamento = "Finanzas", Posicion = "Analista Financiero", AñosExperiencia = 6, Edad = 33, Salario = 67000, NivelSatisfaccion = 0.65, CalificacionDesempeno  = 4.3, HorasTrabajo = 38, FechaContratacion = DateTime.Parse("2018-10-30") },
				new Empleado { IdEmpleado = 7, Nombre = "Chris Martinez", Departamento = "Ventas", Posicion = "Representante de Ventas", AñosExperiencia = 3, Edad = 27, Salario = 48000, NivelSatisfaccion = 0.5, CalificacionDesempeno  = 3.5, HorasTrabajo = 40, FechaContratacion = DateTime.Parse("2020-06-15") },
				new Empleado { IdEmpleado = 8, Nombre = "Patricia Taylor", Departamento = "Marketing", Posicion = "Gerente de Redes Sociales", AñosExperiencia = 5, Edad = 31, Salario = 58000, NivelSatisfaccion = 0.85, CalificacionDesempeno  = 4.6, HorasTrabajo = 36, FechaContratacion = DateTime.Parse("2017-11-20") },
				new Empleado { IdEmpleado = 9, Nombre = "Daniel White", Departamento = "Ingeniería", Posicion = "Desarrollador Backend", AñosExperiencia = 8, Edad = 34, Salario = 80000, NivelSatisfaccion = 0.75, CalificacionDesempeno  = 4.4, HorasTrabajo = 42, FechaContratacion = DateTime.Parse("2015-04-18") },
				new Empleado { IdEmpleado = 10, Nombre = "Laura Harris", Departamento = "Finanzas", Posicion = "Contador", AñosExperiencia = 4, Edad = 29, Salario = 62000, NivelSatisfaccion = 0.9, CalificacionDesempeno  = 4.9, HorasTrabajo = 40, FechaContratacion = DateTime.Parse("2019-05-05") },
				new Empleado { IdEmpleado = 11, Nombre = "Mark Lee", Departamento = "Ingeniería", Posicion = "Desarrollador Frontend", AñosExperiencia = 4, Edad = 28, Salario = 68000, NivelSatisfaccion = 0.7, CalificacionDesempeno  = 4.2, HorasTrabajo = 38, FechaContratacion = DateTime.Parse("2019-03-12") },
				new Empleado { IdEmpleado = 12, Nombre = "Anna Walker", Departamento = "Ventas", Posicion = "Asociado de Ventas", AñosExperiencia = 2, Edad = 25, Salario = 45000, NivelSatisfaccion = 0.55, CalificacionDesempeno  = 3.6, HorasTrabajo = 40, FechaContratacion = DateTime.Parse("2021-08-25") },
				new Empleado { IdEmpleado = 13, Nombre = "Carlos Hernandez", Departamento = "Ingeniería", Posicion = "Científico de Datos", AñosExperiencia = 5, Edad = 32, Salario = 90000, NivelSatisfaccion = 0.8, CalificacionDesempeno  = 4.7, HorasTrabajo = 40, FechaContratacion = DateTime.Parse("2017-05-10") },
				new Empleado { IdEmpleado = 14, Nombre = "Megan King", Departamento = "Recursos Humanos", Posicion = "Gerente de RRHH", AñosExperiencia = 6, Edad = 36, Salario = 75000, NivelSatisfaccion = 0.85, CalificacionDesempeno  = 4.4, HorasTrabajo = 37, FechaContratacion = DateTime.Parse("2016-02-14") },
				new Empleado { IdEmpleado = 15, Nombre = "Tom Clark", Departamento = "Marketing", Posicion = "Especialista en SEO", AñosExperiencia = 3, Edad = 27, Salario = 52000, NivelSatisfaccion = 0.65, CalificacionDesempeno  = 4.0, HorasTrabajo = 35, FechaContratacion = DateTime.Parse("2020-01-30") },
				new Empleado { IdEmpleado = 16, Nombre = "Linda Martin", Departamento = "Finanzas", Posicion = "Contador", AñosExperiencia = 4, Edad = 29, Salario = 63000, NivelSatisfaccion = 0.9, CalificacionDesempeno  = 4.8, HorasTrabajo = 40, FechaContratacion = DateTime.Parse("2018-06-11") },
				new Empleado { IdEmpleado = 17, Nombre = "Sam Lewis", Departamento = "Ingeniería", Posicion = "Arquitecto de Software", AñosExperiencia = 10, Edad = 40, Salario = 120000, NivelSatisfaccion = 0.85, CalificacionDesempeno  = 4.9, HorasTrabajo = 42, FechaContratacion = DateTime.Parse("2014-11-01") },
				new Empleado { IdEmpleado = 18, Nombre = "Rachel Turner", Departamento = "Marketing", Posicion = "Redactor de Contenidos", AñosExperiencia = 3, Edad = 26, Salario = 48000, NivelSatisfaccion = 0.7, CalificacionDesempeno  = 3.9, HorasTrabajo = 36, FechaContratacion = DateTime.Parse("2020-04-22") },
				new Empleado { IdEmpleado = 19, Nombre = "Jason Scott", Departamento = "Finanzas", Posicion = "Controlador Financiero", AñosExperiencia = 8, Edad = 38, Salario = 85000, NivelSatisfaccion = 0.75, CalificacionDesempeno  = 4.3, HorasTrabajo = 39, FechaContratacion = DateTime.Parse("2015-08-17") },
				new Empleado { IdEmpleado = 20, Nombre = "Sophia Young", Departamento = "Ventas", Posicion = "Ejecutivo de Cuentas", AñosExperiencia = 4, Edad = 30, Salario = 65000, NivelSatisfaccion = 0.8, CalificacionDesempeno  = 4.5, HorasTrabajo = 40, FechaContratacion = DateTime.Parse("2018-09-05") },
				new Empleado { IdEmpleado = 21, Nombre = "David Wright", Departamento = "Ingeniería", Posicion = "Ingeniero de QA", AñosExperiencia = 5, Edad = 34, Salario = 70000, NivelSatisfaccion = 0.78, CalificacionDesempeno  = 4.3, HorasTrabajo = 40, FechaContratacion = DateTime.Parse("2017-07-14") },
				new Empleado { IdEmpleado = 22, Nombre = "Laura Baker", Departamento = "Recursos Humanos", Posicion = "Reclutador", AñosExperiencia = 3, Edad = 27, Salario = 54000, NivelSatisfaccion = 0.85, CalificacionDesempeno  = 4.6, HorasTrabajo = 36, FechaContratacion = DateTime.Parse("2019-12-19") },
				new Empleado { IdEmpleado = 23, Nombre = "James Roberts", Departamento = "Marketing", Posicion = "Gerente de Marca", AñosExperiencia = 6, Edad = 34, Salario = 71000, NivelSatisfaccion = 0.8, CalificacionDesempeno  = 4.2, HorasTrabajo = 38, FechaContratacion = DateTime.Parse("2016-08-25") },
				new Empleado { IdEmpleado = 24, Nombre = "Sarah Collins", Departamento = "Ventas", Posicion = "Consultor de Ventas", AñosExperiencia = 5, Edad = 32, Salario = 61000, NivelSatisfaccion = 0.68, CalificacionDesempeno  = 4.1, HorasTrabajo = 40, FechaContratacion = DateTime.Parse("2018-03-18") },
				new Empleado { IdEmpleado = 25, Nombre = "Joshua Adams", Departamento = "Ingeniería", Posicion = "Desarrollador Full Stack", AñosExperiencia = 6, Edad = 31, Salario = 88000, NivelSatisfaccion = 0.82, CalificacionDesempeno  = 4.7, HorasTrabajo = 42, FechaContratacion = DateTime.Parse("2017-10-29") },
				new Empleado { IdEmpleado = 26, Nombre = "Amber Kelly", Departamento = "Finanzas", Posicion = "Especialista en Impuestos", AñosExperiencia = 4, Edad = 28, Salario = 67000, NivelSatisfaccion = 0.88, CalificacionDesempeno  = 4.5, HorasTrabajo = 38, FechaContratacion = DateTime.Parse("2019-11-07") },
				new Empleado { IdEmpleado = 27, Nombre = "Jonathan Price", Departamento = "Ingeniería", Posicion = "Ingeniero de Machine Learning", AñosExperiencia = 3, Edad = 29, Salario = 95000, NivelSatisfaccion = 0.72, CalificacionDesempeno  = 4.3, HorasTrabajo = 41, FechaContratacion = DateTime.Parse("2020-01-20") },
				new Empleado { IdEmpleado = 28, Nombre = "Victoria Foster", Departamento = "Recursos Humanos", Posicion = "Coordinador de Capacitacion", AñosExperiencia = 4, Edad = 33, Salario = 58000, NivelSatisfaccion = 0.83, CalificacionDesempeno  = 4.6, HorasTrabajo = 37, FechaContratacion = DateTime.Parse("2018-06-27") },
				new Empleado { IdEmpleado = 29, Nombre = "Brandon Cook", Departamento = "Marketing", Posicion = "Diseñador Gráfico", AñosExperiencia = 2, Edad = 24, Salario = 49000, NivelSatisfaccion = 0.7, CalificacionDesempeno  = 3.7, HorasTrabajo = 35, FechaContratacion = DateTime.Parse("2021-05-05") },
				new Empleado { IdEmpleado = 30, Nombre = "Emily Rivera", Departamento = "Ventas", Posicion = "Coordinadora de Ventas", AñosExperiencia = 3, Edad = 29, Salario = 57000, NivelSatisfaccion = 0.75, CalificacionDesempeno  = 4.2, HorasTrabajo = 39, FechaContratacion = DateTime.Parse("2020-09-09") }
			};

			var empleadosPorDepartamento = empleados
				.GroupBy(empleado=>empleado.Departamento)
				.Select(grupo=> new
				{
					Departamento = grupo.Key,
					Cantidad = grupo.Count()
				})
				.ToList();

			var promediosPorDepartamento = empleados
				.GroupBy(empleado => empleado.Departamento)
				.Select(grupo => new {
					Departamento = grupo.Key,
					PromedioDesempeno = grupo.Average(empleado => empleado.CalificacionDesempeno),
					PromedioSatisfaccion = grupo.Average(empleado => empleado.NivelSatisfaccion) * 5
				})
				.ToList();


			var puntajesPorDepartamento = empleados
				.GroupBy(empleado => empleado.Departamento)
				.Select(grupo => new {
					Departamento = grupo.Key,
					PuntajeDesempeno = grupo.Sum(empleado => empleado.CalificacionDesempeno),
					PuntajeSatisfaccion = grupo.Sum(empleado => empleado.NivelSatisfaccion * 5)
				})
				.ToList();

			var salarioPromedioPorDepartamento = empleados
				.GroupBy(empleado => empleado.Departamento)
				.Select(grupo => new
				{
					Departamento = grupo.Key,
					SalarioPromedio = grupo.Average(empleado => empleado.Salario)
				})
				.ToList();

			var promedioPorPosicion = empleados
				.GroupBy (empleado => empleado.Posicion)
				.Select(grupo => new
				{
					Posicion = grupo.Key,
					PromedioExperiencia = grupo.Average(empleado => empleado.AñosExperiencia)
				})
				.ToList();

			var contratadosPorAnio = empleados
				.GroupBy(e => e.FechaContratacion.Year)
				.Select(g => new 
				{ 
					Año = g.Key, Cantidad = g.Count() 
				})
				.OrderBy(d => d.Año)
				.ToList();

			ViewBag.Departamentos = JsonConvert.SerializeObject(promediosPorDepartamento.Select(grupo => grupo.Departamento).ToList());
			ViewBag.PromedioDesempeno = JsonConvert.SerializeObject(promediosPorDepartamento.Select(grupo => grupo.PromedioDesempeno).ToList());
			ViewBag.PromedioSatisfaccion = JsonConvert.SerializeObject(promediosPorDepartamento.Select(grupo => grupo.PromedioSatisfaccion).ToList());
			ViewBag.PuntajeDesempeno = JsonConvert.SerializeObject(puntajesPorDepartamento.Select(grupo => grupo.PuntajeDesempeno).ToList());
			ViewBag.PuntajeSatisfaccion = JsonConvert.SerializeObject(puntajesPorDepartamento.Select(grupo => grupo.PuntajeSatisfaccion).ToList());
			ViewBag.EmpleadosPorDepartamento = JsonConvert.SerializeObject(empleadosPorDepartamento);
			ViewBag.DepartamentosSal = JsonConvert.SerializeObject(salarioPromedioPorDepartamento.Select(grupo => grupo.Departamento).ToList());
			ViewBag.salarioPromedioPorDepartamento = JsonConvert.SerializeObject(salarioPromedioPorDepartamento.Select(grupo => grupo.SalarioPromedio).ToList()); //AGREGADO AHORITA
			ViewBag.Posicion = JsonConvert.SerializeObject(promedioPorPosicion.Select(g => g.Posicion).ToList());
			ViewBag.PromedioPorPosicion = JsonConvert.SerializeObject(promedioPorPosicion.Select(g => g.PromedioExperiencia).ToList());
			ViewBag.ContratadosPorAnio = contratadosPorAnio;




			return View();
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
}
