using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia.DbInterfase
{
    public interface IControlAccesoDao : IDao<ControlAcceso, Int32>
    {

        ControlAcceso GetById(Int32 id);

        ControlAcceso GetByIdControl(Int32 idControl);

        ControlAcceso GetByDireccionIp(String DireccionIp);

        List<ControlAcceso> GetListado();
        List<ControlAcceso> GetListado(Boolean soloActivo, Plaza plaza, DbDao.ControlAccesoDao.Ordenamiento orden);

        Int64 GetConteo();

        Int32 GetMaxIdControl();

        ControlAcceso DoBloquear(ControlAcceso u);

        ControlAcceso DoEliminar(ControlAcceso u);

    }
}
