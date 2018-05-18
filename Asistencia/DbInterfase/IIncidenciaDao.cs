using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IIncidenciaDao : IDao<Incidencia, Int32>
    {
        Incidencia GetById(Int32 id);
        Incidencia GetByEmpleadoFecha(Empleado empleado, DateTime fecha);
        Incidencia GetByEmpleadoControlFechaInOutMode(Empleado empleado, ControlAcceso control, DateTime fecha, Int32 inOutMode);

        List<Incidencia> GetListado();

        List<Incidencia> GetListado(short EnviadoWs);


        List<Incidencia> GetListado(Plaza plaza, DateTime fechaInicio, DateTime fechaFin);

        void DoEliminar(Incidencia u);

    }
}
