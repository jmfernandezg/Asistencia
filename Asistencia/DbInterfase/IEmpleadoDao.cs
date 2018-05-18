using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IEmpleadoDao : IDao<Empleado, Int32>
    {
        Empleado GetById(Int32 id);

        Empleado GetByNumeroEmpleado(Int32 NumeroEmpleado);

        List<Empleado> GetListado();

        List<Empleado> GetListado(Boolean soloActivo);

        Int64 GetConteo();

        Empleado DoBloquear(Empleado u);

        Empleado DoEliminar(Empleado u);

    }
}
