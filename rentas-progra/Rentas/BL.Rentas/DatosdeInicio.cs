using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class DatosdeInicio : CreateDatabaseIfNotExists<Contexto>
    {
        protected override void Seed(Contexto contexto)
        {

            var usuarioAdmin = new Usuario();
            usuarioAdmin.Nombre = "admin";
            usuarioAdmin.Contrasena = "123";

            contexto.Usuarios.Add(usuarioAdmin);

            //CategoriasProductos
            var categoria1 = new Categoria();
            categoria1.Descripcion = "Acción y Aventura";
            contexto.Categorias.Add(categoria1);

            var categoria2 = new Categoria();
            categoria2.Descripcion = "Deportes";
            contexto.Categorias.Add(categoria2);

            var categoria3 = new Categoria();
            categoria3.Descripcion = "Disparo";
            contexto.Categorias.Add(categoria3);

            var categoria4 = new Categoria();
            categoria4.Descripcion = "Educativos";
            contexto.Categorias.Add(categoria4);

            //CiudadCliente
            var CiudadCliente1 = new CiudadCliente();
            CiudadCliente1.Descripcion = "San Pedro Sula";
            contexto.CiudadCliente.Add(CiudadCliente1);

            var CiudadCliente2 = new CiudadCliente();
            CiudadCliente2.Descripcion = "Tegucigalpa";
            contexto.CiudadCliente.Add(CiudadCliente2);

            base.Seed(contexto);
        }
    }
}
