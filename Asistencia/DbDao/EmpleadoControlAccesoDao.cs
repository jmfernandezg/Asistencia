using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class EmpleadoControlAccesoDao : AbstractNHibernateDao<EmpleadoControlAcceso, Int32>, IEmpleadoControlAccesoDao

    {

        public EmpleadoControlAcceso GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Id", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public EmpleadoControlAcceso GetByControlAcceso(ControlAcceso control, Empleado empleado)
        {
            if (control == null || empleado == null)
            {
                return null;
            }

            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("ControlAcceso", control));
            lista.Add(Restrictions.Eq("Empleado", empleado));
            return GetUniqueByCriteria(lista.ToArray());

        }


        public List<EmpleadoControlAcceso> GetByEmpleado(Empleado empleado)
        {
            if (empleado == null)
            {
                return null;
            }

            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Empleado", empleado));
            return GetByCriteria(lista.ToArray());

        }


        public List<EmpleadoControlAcceso> GetListado(List<ControlAcceso> controles, ControlAcceso ctrl)
        {
            if (controles == null && ctrl == null)
            {
                return new List<EmpleadoControlAcceso>();
            }

            List<ICriterion> lista = new List<ICriterion>();

            if (controles != null && controles.Count > 0)
            {
                lista.Add(Restrictions.In("ControlAcceso", controles));
            }

            if (ctrl != null)
            {
                lista.Add(Restrictions.Eq("ControlAcceso", ctrl));

            }
            return GetByCriteria(lista.ToArray());
        }


        public void DoEliminar(EmpleadoControlAcceso u)
        {
            Delete(u);
        }
    }
}