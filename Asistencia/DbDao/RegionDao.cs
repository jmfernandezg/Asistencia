using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class RegionDao : AbstractNHibernateDao<Region, Int32>, IRegionDao

    {

        public Region GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Id", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public List<Region> GetListado()
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));
            return GetByCriteria(lista.ToArray());
        }
        public Region GetByNombre(string nombre)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Nombre", nombre));
            lista.Add(Restrictions.Eq("Habilitado", true));

            return GetUniqueByCriteria(lista.ToArray());
        }

        public long GetConteo()
        {
            return GetListado().Count;
        }

        public Region DoBloquear(Region u)
        {
            u.Activo = false;
            return SaveOrUpdate(u);
        }

        public Region DoEliminar(Region u)
        {
            u.Habilitado = false;
            return SaveOrUpdate(u);
        }
    }
}