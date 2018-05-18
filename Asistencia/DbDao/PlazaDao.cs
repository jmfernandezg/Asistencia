using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class PlazaDao : AbstractNHibernateDao<Plaza, Int32>, IPlazaDao

    {

        public Plaza GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Id", id));
            return GetUniqueByCriteria(lista.ToArray());
        }
        public Plaza GetByNombre(string nombre)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));
            lista.Add(Restrictions.Eq("Nombre", nombre));
            return GetUniqueByCriteria(lista.ToArray());
        }


        public List<Plaza> GetListado()
        {
            return GetListado(false);
        }
        public List<Plaza> GetListado(Boolean soloActivo)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));

            if (soloActivo)
            {
                lista.Add(Restrictions.Eq("Activo", true));
            }
            return GetByCriteria(lista.ToArray());
        }

        public long GetConteo()
        {
            return GetListado().Count;
        }

        public Plaza DoBloquear(Plaza u)
        {
            u.Activo = false;
            return SaveOrUpdate(u);
        }

        public Plaza DoEliminar(Plaza u)
        {
            u.Habilitado = false;
            return SaveOrUpdate(u);
        }

    }
}