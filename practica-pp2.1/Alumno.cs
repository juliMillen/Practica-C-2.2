using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_pp2._1
{
    [Serializable]
    class Alumno:IComparable
    {
        private long dni;
        private string nombre;
        private int nota;
        public string Nombre {
            get { return nombre; }
            set { nombre = value; } 
        }
        public long Dni
        {
            get { return dni; }
            set
            {
                if (value < 5000000) throw new Exception("Error, dni no valido!");
                dni = value;
            }
        }

        public int Nota
        {
            get { return nota; }
            set { nota = value; }
            
        }

        public Alumno(string nombre, long dni)
        {
            this.nombre = nombre;
            this.dni = dni;
        }


        public int CompareTo(Object obj)
        {
            return this.nota.CompareTo(((Alumno)obj).nota); //compara alumnos por notas
        }
    }
}
