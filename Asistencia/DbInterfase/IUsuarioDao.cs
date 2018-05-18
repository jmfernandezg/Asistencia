using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IUsuarioDao : IDao<Usuario, Int32>
    {

        Usuario GetById(Int32 id);

        Usuario GetByUsuarioContrasena(String usuario, String contrasena);

        Usuario GetByUsuario(String usuario);

        List<Usuario> GetListado();

        Int64 GetConteo();

        Usuario DoBloquear(Usuario u);

        Usuario DoEliminar(Usuario u);

    }
}
