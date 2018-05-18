using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IPlazaDao : IDao<Plaza, Int32>
    {
        Plaza GetById(Int32 id);
        Plaza GetByNombre(String nombre);

        List<Plaza> GetListado();
        List<Plaza> GetListado(Boolean soloActivo);

        Int64 GetConteo();

        Plaza DoBloquear(Plaza u);

        Plaza DoEliminar(Plaza u);

    }
}
