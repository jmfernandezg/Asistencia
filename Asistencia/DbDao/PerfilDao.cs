using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class PerfilDao : AbstractNHibernateDao<Perfil, Int32>, IPerfilDao

    {

        public Perfil GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Id", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public List<Perfil> GetListado()
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));
            return GetByCriteria(lista.ToArray());
        }
        public long GetConteo()
        {
            return GetListado().Count;
        }

        public Perfil DoBloquear(Perfil u)
        {
            u.Activo = false;
            return SaveOrUpdate(u);
        }

        public Perfil DoEliminar(Perfil u)
        {
            u.Habilitado = false;
            return SaveOrUpdate(u);
        }
    }
}