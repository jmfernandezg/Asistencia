using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class EmpleadoDao : AbstractNHibernateDao<Empleado, Int32>, IEmpleadoDao

    {

        public Empleado GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("CveEmpleado", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public Empleado GetByNumeroEmpleado(Int32 NumeroEmpleado)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("NoEmpleado", NumeroEmpleado));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public List<Empleado> GetListado()
        {
            return GetListado(false);
        }
        public List<Empleado> GetListado(Boolean soloactivo)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));

            if (soloactivo)
            {
                lista.Add(Restrictions.Eq("Activo", true));
            }
            return GetByCriteria(lista.ToArray());
        }
        public long GetConteo()
        {
            return GetListado().Count;
        }

        public Empleado DoBloquear(Empleado u)
        {
            u.Activo = false;
            return SaveOrUpdate(u);
        }

        public Empleado DoEliminar(Empleado u)
        {
            u.Habilitado = false;
            return SaveOrUpdate(u);
        }

    }
}