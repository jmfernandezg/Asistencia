using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IColectorMovimientoDao : IDao<ColectorMovimiento, Int32>
    {
        ColectorMovimiento GetById(Int32 id);

        List<ColectorMovimiento> GetListado();

        void DoEliminar(ColectorMovimiento u);

    }
}
