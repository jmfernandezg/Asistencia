using Asistencia.Clases;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class PlantillaDao : AbstractNHibernateDao<Plantilla, Int32>, IPlantillaDao

    {

        public Plantilla GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Id", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

        public Plantilla GetByControlAccesoEnrollNumberFingerIndex(ControlAcceso control, String EnrollNumber, Int32 fingerindex)
        {
            if (string.IsNullOrEmpty(EnrollNumber) || control == null)
            {
                return null;
            }

            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("ControlAcceso", control));
            lista.Add(Restrictions.Eq("Enrollnumber", EnrollNumber));
            lista.Add(Restrictions.Eq("Fingerindex", fingerindex));
            return GetUniqueByCriteria(lista.ToArray());

        }

        public List<Plantilla> GetListado(List<ControlAcceso> controles, ControlAcceso ctrl)
        {
            if (controles == null && ctrl == null)
            {
                return new List<Plantilla>();
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

        public List<GrupoPlantilla> GetGrupo(List<ControlAcceso> controles)
        {
            return NHibernateSessionManager.Instance.GetSession().CreateCriteria<Plantilla>()
                   .Add(Restrictions.In("ControlAcceso", controles))
                   .SetProjection(Projections.ProjectionList()
                   .Add(Projections.RowCount(), "Conteo")
                   .Add(Projections.GroupProperty("ControlAcceso"), "Control")
                   ).SetResultTransformer(Transformers.AliasToBean(typeof(GrupoPlantilla)))
                   .List<GrupoPlantilla>() as List<GrupoPlantilla>;
        }


        public void DoEliminar(Plantilla u)
        {
            Delete(u);
        }

    }
}