using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IRegionDao : IDao<Region, Int32>
    {
        Region GetById(Int32 id);

        Region GetByNombre(String nombre);


        List<Region> GetListado();

        Int64 GetConteo();

        Region DoBloquear(Region u);

        Region DoEliminar(Region u);

    }
}
