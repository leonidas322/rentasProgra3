using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class ClientesBL
    {
        Contexto _Contexto;
        public BindingList<Clientes> ListaClientes { get; set; }

        public ClientesBL()
        {
            _Contexto = new Contexto();
            ListaClientes = new BindingList<Clientes>();
        }

        public BindingList<Clientes> ObtenerClientes()
        {
            _Contexto.Clientes.Load();
            ListaClientes = _Contexto.Clientes.Local.ToBindingList();

            return ListaClientes;
        }

        //Guardar Cliente
        public Resultado GuardarClientes(Clientes cliente)
        {
            var resultado = ValidarCliente(cliente);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            //if (cliente.Id == 0)
            //{   
            //    cliente.Id = ListaClientes.Max(item => item.Id);
            //}

            _Contexto.SaveChanges();
            resultado.Exitoso = true;
            return resultado;

        }
        //Agregar Cliente
        public void AgregarCliente()
        {
            var NuevoCliente = new Clientes();
            ListaClientes.Add(NuevoCliente);
        }

        //Eliminar Cliente
        public bool EliminarCliente(int id)
        {
            foreach (var cliente in ListaClientes)
            {
                if (cliente.Id == id)
                {
                    ListaClientes.Remove(cliente);
                    _Contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }
        //Validar Cliente
        public Resultado ValidarCliente(Clientes cliente)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (string.IsNullOrEmpty(cliente.Nombre) == true)
            {
                resultado.Mensaje = "Debe ingresar un nombre";
                resultado.Exitoso = false;
            }

            if (string.IsNullOrEmpty(cliente.Telefono) == true)
            {
                resultado.Mensaje = "Debe ingresar un teléfono";
                resultado.Exitoso = false;
            }

            if (cliente.LimiteCredito < 0)
            {
                resultado.Mensaje = "No puede ingresar valores negativos al limite de crédito";
                resultado.Exitoso = false;
            }

            if(cliente.CiudadClienteId == 0)
            {
                resultado.Mensaje = "Seleccione una ciudad";
                resultado.Exitoso = false;

            }
            return resultado;
        }
    }
    public class Clientes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public byte[] Foto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int CiudadClienteId { get; set; }
        public CiudadCliente CiudadCliente { get; set; }
        public int LimiteCredito { get; set; }
        public bool Activo { get; set; }

    }

    public class ResultadoClientes
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
    }
}
