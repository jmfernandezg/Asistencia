using System;
using System.Collections.Generic;

namespace Asistencia.DbInterfase
{
    public interface IColectorMovimientoIncidenciaDao : IDao<ColectorMovimientoIncidencia, Int32>
    {
        ColectorMovimientoIncidencia GetById(Int32 id);

        List<ColectorMovimientoIncidencia> GetListado(DateTime fechaInicio, DateTime fechaTermino, Boolean conEmpleado = true);

        void DoEliminar(ColectorMovimientoIncidencia u);

    }
}
