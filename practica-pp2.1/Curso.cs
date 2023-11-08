using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace practica_pp2._1
{
    [Serializable]
    class Curso
    {
        public string Nombre { get; private set; }
        private List<Alumno> lstAlumnos = new List<Alumno>();

        public Curso(string nombre)
        {
            this.Nombre = nombre;
        }

        public void AgregarAlumno(Alumno nuevoAlumno)
        {
            lstAlumnos.Add(nuevoAlumno); //agrega un nuevo alumno a la lista
        }

        public void AgregarNota(int nota,long dni)
        {
            Alumno alumno = BuscarAlumno(dni);
            if (alumno != null)
            {
                alumno.Nota = nota;
            }
            else
            {
                throw new Exception("Alumno no encontrado");
            }
        }

        public Alumno BuscarAlumno(long dni)
        {
            Alumno buscado = new Alumno("", dni);
            lstAlumnos.Sort();
            int orden = lstAlumnos.BinarySearch(buscado);
            if (orden >= 0)
            {
                buscado = lstAlumnos[orden];
            }
            else
            {
                buscado = null;
            }
            return buscado;
        }
    }
}
