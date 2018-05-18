using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IEmpleadoControlAccesoDao : IDao<EmpleadoControlAcceso, Int32>
    {
        EmpleadoControlAcceso GetById(Int32 id);

        EmpleadoControlAcceso GetByControlAcceso(ControlAcceso control, Empleado empleado);

        List<EmpleadoControlAcceso> GetByEmpleado(Empleado empleado);

        List<EmpleadoControlAcceso> GetListado(List<ControlAcceso> controles, ControlAcceso control);

        void DoEliminar(EmpleadoControlAcceso u);

    }
}
