using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IPerfilDao : IDao<Perfil, Int32>
    {
        Perfil GetById(Int32 id);

        List<Perfil> GetListado();

        Int64 GetConteo();

        Perfil DoBloquear(Perfil u);

        Perfil DoEliminar(Perfil u);

    }
}
