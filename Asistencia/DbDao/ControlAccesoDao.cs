using System;
using NHibernate.Criterion;
using System.Collections.Generic;
using PasswordHash;
using System.Linq;
using System.Web;

namespace Asistencia.DbDao
{
    public class ControlAccesoDao : AbstractNHibernateDao<ControlAcceso, Int32>, IControlAccesoDao

    {
        public enum Ordenamiento
        {
            Nombre,
            FechaUltimaConexion
        };

        public ControlAcceso GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("CveControlAcceso", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public ControlAcceso GetByIdControl(int idControl)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));
            lista.Add(Restrictions.Eq("IdControl", idControl));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public ControlAcceso GetByDireccionIp(String DireccionIp)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));
            lista.Add(Restrictions.Eq("DireccionIp", DireccionIp));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public List<ControlAcceso> GetListado()
        {
            return GetListado(false, null, Ordenamiento.Nombre);
        }
        public List<ControlAcceso> GetListado(Boolean soloActivo, Plaza plaza, Ordenamiento ordenamiento)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));

            if (soloActivo)
            {
                lista.Add(Restrictions.Eq("Activo", true));
            }
            if (plaza != null)
            {
                lista.Add(Restrictions.Eq("of.Plaza", plaza));
            }

            Order ord = null;

            if (ordenamiento == Ordenamiento.Nombre)
            {
                ord = Order.Asc("Nombre");
            }
            if (ordenamiento == Ordenamiento.FechaUltimaConexion)
            {
                ord = Order.Desc("FechaUltimaConexion");
            }

            List<KeyValuePair<String, String>> aliases = new List<KeyValuePair<string, string>>();
            aliases.Add(new KeyValuePair<string, string>("Oficina", "of"));

            return GetByCriteria(lista.ToArray(), new Order[] { ord }, aliases);
        }

        public int GetMaxIdControl()
        {
            List<IProjection> lista = new List<IProjection>();
            lista.Add(Projections.Max("IdControl"));
            return GetUniqueProjection(lista.ToArray());
        }

        public long GetConteo()
        {
            return GetListado().Count();
        }

        public ControlAcceso DoBloquear(ControlAcceso u)
        {
            u.Activo = false;
            return SaveOrUpdate(u);
        }

        public ControlAcceso DoEliminar(ControlAcceso u)
        {
            u.Habilitado = false;
            return SaveOrUpdate(u);
        }

    }
}