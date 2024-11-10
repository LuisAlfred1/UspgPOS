namespace UspgPOS.Models
{
    public class DetallesEmpleados
    {
        public string Departamento { get; set; }
        public int NivelExperiencia { get; set; } // En años de experiencia
        public double CalificacionRendimiento { get; set; } // Escala de 0 a 10
        public decimal SalarioPromedio { get; set; }
        public double TasaRotacion { get; set; } // Porcentaje de rotacion anual
    }
}
