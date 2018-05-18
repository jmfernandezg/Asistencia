using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class OficinaDao : AbstractNHibernateDao<Oficina, Int32>, IOficinaDao

    {

        public Oficina GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("CveOficina", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public Oficina GetByCodigoPlanta(string codigo)
        {

            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));
            lista.Add(Restrictions.Eq("CodigoPlanta", codigo));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public List<Oficina> GetListado()
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));
            return GetByCriteria(lista.ToArray());
        }
        public long GetConteo()
        {
            return GetListado().Count;
        }

        public Oficina DoBloquear(Oficina u)
        {
            u.Activo = false;
            return SaveOrUpdate(u);
        }

        public Oficina DoEliminar(Oficina u)
        {
            u.Habilitado = false;
            return SaveOrUpdate(u);
        }
    }
}