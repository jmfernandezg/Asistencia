using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IOficinaDao : IDao<Oficina, Int32>
    {
        Oficina GetById(Int32 id);

        List<Oficina> GetListado();

        Int64 GetConteo();

        Oficina DoBloquear(Oficina u);

        Oficina DoEliminar(Oficina u);
        Oficina GetByCodigoPlanta(string codigo);
    }
}
