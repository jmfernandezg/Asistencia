using System;
using NHibernate.Criterion;
using System.Collections.Generic;
using PasswordHash;
using System.Linq;
using System.Web;

namespace Asistencia.DbDao
{
    public class ZonaDao : AbstractNHibernateDao<Zona, Int32>, IZonaDao

    {

        public Zona GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Id", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public Zona GetByNombre(string nombre)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));
            lista.Add(Restrictions.Eq("Nombre", nombre));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public List<Zona> GetListado()
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));
            return GetByCriteria(lista.ToArray());
        }
        public long GetConteo()
        {
            return GetListado().Count;
        }

        public Zona DoBloquear(Zona u)
        {
            u.Activo = false;
            return SaveOrUpdate(u);
        }

        public Zona DoEliminar(Zona u)
        {
            u.Habilitado = false;
            return SaveOrUpdate(u);
        }
    }
}