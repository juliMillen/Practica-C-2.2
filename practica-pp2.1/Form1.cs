using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace practica_pp2._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Alumno> lstAlumnos = new List<Alumno>();
        List<Curso> lstCurso = new List<Curso>();
        Curso curso;
        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {


                    FileStream fS = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    StreamReader sR = new StreamReader(fS);
                    string[] datos;
                    string renglon;
                    while (!sR.EndOfStream)
                    {

                        renglon = sR.ReadLine();
                        datos = renglon.Split(';');
                        string nombre = datos[0];
                        long dni = Convert.ToInt32(datos[1]);
                        int nota = Convert.ToInt32(datos[2]);

                        Alumno nuevoA = new Alumno(nombre, dni);
                        nuevoA.Nota = nota;
                        lstAlumnos.Add(nuevoA);

                        Curso nuevoC = new Curso(tBCurso.Text);
                        lstCurso.Add(nuevoC);

                    }
                    sR.Close();
                    fS.Close();
                }
                catch (IOException)
                {
                    MessageBox.Show("Error en el archivo");
                }

            }

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream files = null;
                StreamWriter sW = null;

                try
                {
                    files = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    sW = new StreamWriter(files);
                    foreach(Alumno alumno in lstAlumnos)
                    {
                        sW.WriteLine($"{alumno.Dni};{alumno.Nombre};{alumno.Nota}");

                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error desconocido!" + ex);
                }
                finally
                {
                    if (files != null)
                    {
                        if (sW != null)
                        {
                            sW.Close();
                            files.Close();
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileStream filee = null;
            string ruta = Application.StartupPath;
            string rutaBin = Path.Combine(ruta, "sistema.dato");
            try
            {
                filee = new FileStream(rutaBin, FileMode.OpenOrCreate, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter();

                curso = (Curso)formatter.Deserialize(filee);
            }
            finally
            {
                filee.Close();
            }
            if (curso == null)
            {
                curso = new Curso("Matemática");
                Alumno alumno1 = new Alumno("juan domingo", 24324232);
                Alumno alumno2 = new Alumno("fortunato perez", 58233232);
                curso.AgregarAlumno(alumno1);
                curso.AgregarAlumno(alumno2);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string ruta = Application.StartupPath;
            string rutaBin = Path.Combine(ruta, "sistema.dato");
            FileStream filess = null;

            try
            {
                filess = new FileStream(rutaBin, FileMode.Create, FileAccess.Write);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(filess, lstCurso);
            }
            finally
            {
                if (filess != null)
                {
                    filess.Close();
                }
            }
        }
    }
}
