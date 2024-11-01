using Datos.Infraestructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class UsuariosRepositories
    {
        public Boolean IsEmailRegistered(string email)
        {
            var contexto = new equipo_c_dbEntities();

            // Verifica si existe al menos un usuario con ese correo
            bool emailExists = contexto.usuarios.Any(u => u.EMAIL == email);


			/*
			 * var gmail=contexto.usuarios.Where(u => u.EMAIL==email);
			 * 
			 * if(var!=null){
			 *		return true;
			 * }else{
			 *		return false;
			 * }
			 * 
			 */


            return emailExists;
        }

        

        public List<usuarios> ObtenerUsuarios()
		{
			List<usuarios> products = new List<usuarios>();
			try
			{
				using (var contexto = new equipo_c_dbEntities())
				{
					products = contexto.usuarios.ToList();
				}
				return products;
			} catch (Exception)
			{
				return products;
			}
		}

        public usuarios ExisteUsuarioEmail(string email)
        {
            usuarios usu=null;

            try
            {
                using (var contexto = new equipo_c_dbEntities())
                {
                    usu = contexto.usuarios.Where(u => u.EMAIL == email).FirstOrDefault();
                }
                return usu;
            }
            catch (Exception)
            {
                return usu;
            }

        }

        public usuarios ExisteUsuarioDNI(string dni)
        {
            usuarios usu = null;

            try
            {
                using (var contexto = new equipo_c_dbEntities())
                {
                    usu = contexto.usuarios.Where(u => u.DNI == dni).FirstOrDefault();
                }
                return usu;
            }
            catch (Exception)
            {
                return usu;
            }

        }



        public void AltaUsuario(usuarios user)
        {
            try
            {
				using (var contexto = new equipo_c_dbEntities())
				{
					contexto.usuarios.Add(user);
					contexto.SaveChanges();
				}
			} catch (Exception e) { Console.WriteLine(e.ToString()); }
        }

    }
}
