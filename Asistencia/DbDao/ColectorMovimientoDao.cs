using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class ColectorMovimientoDao : AbstractNHibernateDao<ColectorMovimiento, Int32>, IColectorMovimientoDao

    {

        public ColectorMovimiento GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("CveColectorMovimientos", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public List<ColectorMovimiento> GetListado()
        {
            List<ICriterion> lista = new List<ICriterion>();
            return GetByCriteria(lista.ToArray());
        }

        public void DoEliminar(ColectorMovimiento u)
        {
            Delete(u);
        }
    }
}