using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IDetalleCatalogoDao : IDao<DetalleCatalogo, Int32>
    {
        DetalleCatalogo GetById(Int32 id);
        List<DetalleCatalogo> GetListado();

    }
}
