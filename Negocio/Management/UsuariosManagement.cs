using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Datos.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Repositories;
using Datos.Infraestructures;

using Negocio.EntitiesDTO;
namespace Negocio.Management

{
	public class UsuariosManagement
	{

		public UsuariosDTO userToUserDTO(usuarios user)
		{
			if (user == null) { return new UsuariosDTO(); }

            try
			{
				var usuarioDTO = new UsuariosDTO();
				usuarioDTO.DNI = user.DNI;
				usuarioDTO.GMAIL = user.EMAIL;
				usuarioDTO.NOMBRE = user.NOMBRE;
				usuarioDTO.PASS = user.PASS;
				usuarioDTO.APELLIDOS = user.APELLIDOS;
				usuarioDTO.TELEFONO = user.TELEFONO;
				usuarioDTO.DIRECCION = user.DIRECCION;
				usuarioDTO.CCC = user.CCC;
				usuarioDTO.PRIVILEGIO = user.PRIVILEGIO;

				return usuarioDTO;

			} catch(Exception e)
			{
				UsuariosDTO nullUser = new UsuariosDTO();
				return nullUser;
			}
        }

		public List<UsuariosDTO> ObtenerUsuarios() {
			
			List<usuarios> usuarios = new Datos.Repositories.UsuariosRepositories().ObtenerUsuarios();
			List<UsuariosDTO> usuariosDTO = new List<UsuariosDTO>();
			//Hacemos el Cast
			foreach (var item in usuarios)
			{
				usuariosDTO.Add(userToUserDTO(item));
			}
			return usuariosDTO;
		}

        public UsuariosDTO ExisteUsuarioEmail(string email)
        {
			usuarios usu = new Datos.Repositories.UsuariosRepositories().ExisteUsuarioEmail(email);
			UsuariosDTO usuDTO = null;

			usuDTO = userToUserDTO(usu);

			return usuDTO;

        }

        public UsuariosDTO ExisteUsuarioDNI(string dni)
        {
            usuarios usu = new Datos.Repositories.UsuariosRepositories().ExisteUsuarioDNI(dni);
            UsuariosDTO usuDTO = null;

            usuDTO = userToUserDTO(usu);

            return usuDTO;

        }

        public void AltaUsuario(UsuariosDTO usuDTO)
		{
			usuarios user = new usuarios();
			user.EMAIL = usuDTO.GMAIL;
			user.NOMBRE = usuDTO.NOMBRE;
			user.APELLIDOS = usuDTO.APELLIDOS;
			user.PASS = Hasher.HashPassword(usuDTO.PASS); // Constraseña Hasheada
			user.DNI = usuDTO.DNI;
			user.CCC = usuDTO.CCC;
			user.TELEFONO = usuDTO.TELEFONO;
			user.DIRECCION = usuDTO.DIRECCION;
			user.PRIVILEGIO = usuDTO.PRIVILEGIO;

			// Añadir hash de registro

			new Datos.Repositories.UsuariosRepositories().AltaUsuario(user);
		}

	}
}

