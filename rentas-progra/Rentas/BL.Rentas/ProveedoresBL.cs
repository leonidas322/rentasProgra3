using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class ProveedoresBL
    {
        Contexto _contexto;
        public BindingList<Proveedores> ListaProveedores { get; set; }

        public ProveedoresBL()
        {
            _contexto = new Contexto();
            ListaProveedores = new BindingList<Proveedores>();
        }

        public BindingList<Proveedores> ObtenerProveedores()
        {
            _contexto.Proveedores.Load();
            ListaProveedores = _contexto.Proveedores.Local.ToBindingList();

            return ListaProveedores;
        }

        //Guardar Proveedor
        public Resultado GuardarProveedores(Proveedores proveedores)
        {
            var resultado = ValidarProveedor(proveedores);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            _contexto.SaveChanges();
            resultado.Exitoso = true;
            return resultado;

        }

        //Agregar Proveedor
        public void AgregarProveedor()
        {
            var NuevoProveedor = new Proveedores();
            ListaProveedores.Add(NuevoProveedor);
        }

        //Eliminar Proveedor
        public bool EliminarProveedor(int id)
        {
            foreach (var proveedor in ListaProveedores)
            {
                if (proveedor.Id == id)
                {
                    ListaProveedores.Remove(proveedor);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        //Validar Proveedor
        public Resultado ValidarProveedor(Proveedores proveedores)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (string.IsNullOrEmpty(proveedores.Nombre) == true)
            {
                resultado.Mensaje = "Debe ingresar un nombre";
                resultado.Exitoso = false;
            }

            return resultado;
        }
        
    }

    public class Proveedores
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
    }
}

    

       

