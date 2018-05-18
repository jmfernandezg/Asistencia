using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class ColectorMovimientoIncidenciaDao : AbstractNHibernateDao<ColectorMovimientoIncidencia, Int32>, IColectorMovimientoIncidenciaDao

    {

        public ColectorMovimientoIncidencia GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Id", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public List<ColectorMovimientoIncidencia> GetListado(DateTime fechaInicio, DateTime fechaTermino, Boolean conEmpleado = true)
        {

            List<ICriterion> lista = new List<ICriterion>();

            if (fechaInicio != null && fechaTermino != null)
            {
                lista.Add(Restrictions.Between("Fecha", fechaInicio, fechaTermino));
            }

            if (conEmpleado)
            {

                lista.Add(Restrictions.IsNotNull("CveEmpleado"));
            }

            return GetByCriteria(lista.ToArray());
        }

        public void DoEliminar(ColectorMovimientoIncidencia u)
        {
            Delete(u);
        }
    }
}