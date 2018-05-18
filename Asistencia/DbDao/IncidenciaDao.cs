using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class IncidenciaDao : AbstractNHibernateDao<Incidencia, Int32>, IIncidenciaDao

    {

        public Incidencia GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("CveIncidencia", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public Incidencia GetByEmpleadoFecha(Empleado empleado, DateTime fecha)
        {
            if (empleado == null || fecha == null)
            {
                return null;
            }
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Empleado", empleado));
            lista.Add(Restrictions.Eq("FechaHoraIncidencia", fecha));
            return GetUniqueByCriteria(lista.ToArray());

        }

        public Incidencia GetByEmpleadoControlFechaInOutMode(Empleado empleado, ControlAcceso control, DateTime fecha, int inOutMode)
        {
            if (empleado == null || fecha == null || control == null)
            {
                return null;
            }
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Empleado", empleado));
            lista.Add(Restrictions.Eq("ControlAcceso", control));
            lista.Add(Restrictions.Eq("FechaHoraIncidencia", fecha));
            lista.Add(Restrictions.Eq("InOutMode", inOutMode));


            return GetUniqueByCriteria(lista.ToArray());
        }

        public List<Incidencia> GetListado()
        {
            return GetListado(3);
        }

        public List<Incidencia> GetListado(short EnviadoWS)
        {
            List<ICriterion> lista = new List<ICriterion>();

            if (EnviadoWS != 3)
            {
                lista.Add(Restrictions.Eq("EnviadoWs", EnviadoWS));
            }
            return GetByCriteria(lista.ToArray());
        }

        public List<Incidencia> GetListado(Plaza plaza, DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio == null || fechaInicio == null)
            {
                return null;
            }
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Between("FechaHoraIncidencia", fechaInicio, fechaFin));

            Order ord = Order.Desc("FechaHoraIncidencia");

            List<KeyValuePair<String, String>> aliases = new List<KeyValuePair<string, string>>();
            aliases.Add(new KeyValuePair<string, string>("Empleado", "emp"));

            if (plaza != null)
            {
                lista.Add(Restrictions.Eq("emp.Plaza", plaza));
            }


            return GetByCriteria(lista.ToArray(), new Order[] { ord }, aliases);

        }

        public void DoEliminar(Incidencia u)
        {
            Delete(u);
        }


    }
}