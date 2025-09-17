namespace Clase_entity_Martina
{
    public partial class Form1 : Form
    {
        private Models.RepositorioAlumnos repositorio;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            repositorio = new Models.RepositorioAlumnos();
            CargarAlumnos();
        }

        private void CargarAlumnos()
        {
            var alumnos = repositorio.GetAlumnos();
            dataGridView1.DataSource = alumnos;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var alumno = new Models.Alumno
            {
                Nonbre = txtNombre.Text,
                Nota = int.Parse(txtNota.Text)
            };
            repositorio.AddAlumno(alumno);
            CargarAlumnos();
            LimpiarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var alumno = new Models.Alumno
            {
                Nonbre = txtNombre.Text,
                Nota = int.Parse(txtNota.Text)
            };
            if (dataGridView1.CurrentRow != null)
            {
                alumno.Id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                repositorio.UpdateAlumno(alumno);
                CargarAlumnos();
                LimpiarCampos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                repositorio.DeleteAlumno(id);
                CargarAlumnos();
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtNota.Text = "";
        }

    }
}
