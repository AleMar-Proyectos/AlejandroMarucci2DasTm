using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity_y_Sql_Server.Models;

namespace Entity_y_Sql_Server
{
    public partial class FrmTabla : Form
    {

        public int? id;
        public FrmTabla(int? id = null)
        {
            InitializeComponent();
            this.id = id;
            if (id != null)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            if (id != null)
            {
                using (CrudWindowsFormEntities db = new CrudWindowsFormEntities())
                {
                    entity obj = db.entity.Find(id);
                    txtNombre.Text = obj.nombre;
                    txtCorreo.Text = obj.correo;
                    dtpFechaNacimiento.Value = obj.fechanacimiento;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (CrudWindowsFormEntities db = new CrudWindowsFormEntities())
            {
                entity obj = new entity();
                obj.nombre = txtNombre.Text;
                obj.correo = txtCorreo.Text;
                obj.fechanacimiento = dtpFechaNacimiento.Value;
                db.entity.Add(obj);
                db.SaveChanges();
                this.Close();
            }
        }
    }
}
