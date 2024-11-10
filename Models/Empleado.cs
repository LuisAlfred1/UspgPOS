namespace UspgPOS.Models
{
	public class Empleado
	{
		public int IdEmpleado { get; set; }
		public string Nombre { get; set; }
		public string Departamento { get; set; }
		public string Posicion { get; set; }
		public int AñosExperiencia { get; set; }
		public int Edad { get; set; }
		public decimal Salario { get; set; }
		public double NivelSatisfaccion { get; set; }
		public double CalificacionDesempeno { get; set; }
		public int HorasTrabajo { get; set; }
		public DateTime FechaContratacion { get; set; }
	}
}
