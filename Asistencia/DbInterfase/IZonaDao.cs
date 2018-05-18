using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IZonaDao : IDao<Zona, Int32>
    {
        Zona GetById(Int32 id);
        Zona GetByNombre(String nombre);
        List<Zona> GetListado();

        Int64 GetConteo();

        Zona DoBloquear(Zona u);

        Zona DoEliminar(Zona u);

    }
}
