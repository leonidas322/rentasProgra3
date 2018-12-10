using BL.Rentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Rentas
{
    public partial class FormClientes : Form
    {

        ClientesBL _Clientes;
        CiudadClienteBL _CiudadCliente;

        public FormClientes()
        {
            InitializeComponent();

            _Clientes = new ClientesBL();
            listaClientesBindingSource.DataSource = _Clientes.ObtenerClientes();

            _CiudadCliente = new CiudadClienteBL();
            listaCiudadClienteBindingSource.DataSource = _CiudadCliente.ObtenerCiudadCliente();
        }

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;

        }

        private void Eliminar(int id)
        {
            var resultado = _Clientes.EliminarCliente(id);

            if (resultado == true)
            {
                listaClientesBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el Cliente");
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _Clientes.AgregarCliente();
            listaClientesBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")
            {
                var id = Convert.ToInt32(idTextBox.Text);
                Eliminar(id);
            }
        }

        private void listaClientesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaClientesBindingSource.EndEdit();
            var cliente = (Clientes)listaClientesBindingSource.Current;

            if(fotoPictureBox.Image != null)
            {
                cliente.Foto = Program.imageToByteArray(fotoPictureBox.Image);
            }
            else
            {
                cliente.Foto = null;
            }

            var resultado = _Clientes.GuardarClientes(cliente);

            if (resultado.Exitoso == true)
            {
                listaClientesBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Cliente guardado");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cliente = (Clientes)listaClientesBindingSource.Current;

            if(cliente != null)
            {
                openFileDialog1.ShowDialog();
                var archivo = openFileDialog1.FileName;

                if (archivo != "")
                {
                    var fileInfo = new FileInfo(archivo);
                    var fileStream = fileInfo.OpenRead();

                    fotoPictureBox.Image = Image.FromStream(fileStream);
                }
            }
            else
            {
                MessageBox.Show("Favor crear un producto antes de asignar imagen");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fotoPictureBox.Image = null;
        }
    }
}
