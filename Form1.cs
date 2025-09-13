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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refrescar();
        }

        #region
        private void Refrescar()
        {
            using (CrudWindowsFormEntities db = new CrudWindowsFormEntities())
            {
                var lst = from d in db.entity
                          select d;
                dataGridView1.DataSource = lst.ToList();
            }
        }
        #endregion

        private int? GetId()
        {
            try
            {
                return int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmTabla frm = new FrmTabla();
            frm.ShowDialog();

            Refrescar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                FrmTabla frm = new FrmTabla(id);
                frm.ShowDialog();
                Refrescar();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                using (CrudWindowsFormEntities db = new CrudWindowsFormEntities())
                {
                    entity obj = db.entity.Find(id);
                    db.entity.Remove(obj);
                    db.SaveChanges();
                }
                Refrescar();
            }
        }
    }
}
