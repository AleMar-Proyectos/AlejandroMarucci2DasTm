using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase_entity_Martina.Models
{
    public class RepositorioAlumnos
    {
       private CrudWindowsFormContext context;

        public RepositorioAlumnos()
        {
            context = new CrudWindowsFormContext();
        }

        public List<Alumno> GetAlumnos()
        {
            return context.Alumnos.ToList();
        }

        public void AddAlumno(Alumno alumno)
        {
            context.Alumnos.Add(alumno);
            context.SaveChanges();
        }

        public void UpdateAlumno(Alumno alumno)
        {
            context.Alumnos.Update(alumno);
            context.SaveChanges();
        }

        public void DeleteAlumno(int id)
        {
            var alumno = context.Alumnos.Find(id);
            if (alumno != null)
            {
                context.Alumnos.Remove(alumno);
                context.SaveChanges();
            }
        }
    }
}
