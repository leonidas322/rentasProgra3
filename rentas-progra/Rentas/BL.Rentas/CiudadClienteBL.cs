using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class CiudadClienteBL
    {
        Contexto _contexto;

        public BindingList<CiudadCliente> ListaCiudadCliente { get; set; }

        public CiudadClienteBL()
        {
            _contexto = new Contexto();
            ListaCiudadCliente = new BindingList<CiudadCliente>();
        }

        public BindingList<CiudadCliente> ObtenerCiudadCliente()
        {
            _contexto.CiudadCliente.Load();
            ListaCiudadCliente = _contexto.CiudadCliente.Local.ToBindingList();

            return ListaCiudadCliente;
        }
    }
    public class CiudadCliente
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }   
}


