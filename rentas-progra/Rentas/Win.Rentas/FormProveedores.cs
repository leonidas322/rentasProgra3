using BL.Rentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Rentas
{
    public partial class FormProveedores : Form
    {

        ProveedoresBL _proveedores;

        public FormProveedores()
        {
            InitializeComponent();

            _proveedores = new ProveedoresBL();
            listaProveedoresBindingSource.DataSource = _proveedores.ObtenerProveedores();
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
            var resultado = _proveedores.EliminarProveedor(id);

            if (resultado == true)
            {
                listaProveedoresBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el proveedor");
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _proveedores.AgregarProveedor();
            listaProveedoresBindingSource.MoveLast();

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

        private void listaProveedoresBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaProveedoresBindingSource.EndEdit();
            var proveedor = (Proveedores)listaProveedoresBindingSource.Current;

            var resultado = _proveedores.GuardarProveedores(proveedor);

            if (resultado.Exitoso == true)
            {
                listaProveedoresBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Proveedor guardado");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }
    }
}
