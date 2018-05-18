using System;
using NHibernate.Criterion;
using System.Collections.Generic;
using PasswordHash;
using System.Linq;
using System.Web;

namespace Asistencia.DbDao
{
    public class DetalleCatalogoDao : AbstractNHibernateDao<DetalleCatalogo, Int32>, IDetalleCatalogoDao

    {

        public DetalleCatalogo GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("CveDetalleCatalogos", id));
            return GetUniqueByCriteria(lista.ToArray());
        }



        public List<DetalleCatalogo> GetListado()
        {
            List<ICriterion> lista = new List<ICriterion>();
            return GetByCriteria(lista.ToArray());
        }

    }
}