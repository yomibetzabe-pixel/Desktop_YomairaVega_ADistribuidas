namespace Examen_Doctores_Especialidades.Models
{
    public class Doctor
    {
        public int IdDoctor { get; set; }
        public string NombreDoctor { get; set; }
        public string ApellidosDoctor { get; set; }
        public string EmailDoctor { get; set; }
        public int EdadDoctor { get; set; }
        public string EstadoDoctor { get; set; }

        public Especialidad FkIdEspecialidad { get; set; }
    }

}