using System;
using System.Collections.Generic;
using Asistencia.Clases;

namespace Asistencia.DbInterfase
{
    public interface IPlantillaDao : IDao<Plantilla, Int32>
    {
        Plantilla GetById(Int32 id);

        Plantilla GetByControlAccesoEnrollNumberFingerIndex(ControlAcceso control, String EnrollNumber, Int32 Fingerindex);

        List<Plantilla> GetListado(List<ControlAcceso> controles, ControlAcceso control);

        List<GrupoPlantilla> GetGrupo(List<ControlAcceso> controles);

        void DoEliminar(Plantilla u);

    }
}
