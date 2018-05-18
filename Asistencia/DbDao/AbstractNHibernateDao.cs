using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;


namespace Asistencia.DbDao
{
    public abstract class AbstractNHibernateDao<T, IdT> : IDao<T, IdT> where T : class
    {
        /// <summary>
        /// Loads an instance of type TypeOfListItem from the DB based on its ID.
        /// </summary>
        public T GetById(IdT id, bool shouldLock)
        {
            T entity;

            if (shouldLock)
            {
                entity = (T)NHibernateSession.Load(persitentType, id, LockMode.Upgrade);
            }
            else
            {
                entity = (T)NHibernateSession.Load(persitentType, id);
            }

            return entity;
        }

        /// <summary>
        /// Loads every instance of the requested type with no filtering.
        /// </summary>
        public List<T> GetAll()
        {
            return GetByCriteria();
        }

        /// <summary>
        /// Loads every instance of the requested type using the supplied <see cref="ICriterion" />.
        /// If no <see cref="ICriterion" /> is supplied, this behaves like <see cref="GetAll" />.
        /// </summary>
        public List<T> GetByCriteria(params ICriterion[] criterion)
        {
            return GetByCriteria(criterion, null, null);
        }

        public List<T> GetByCriteria(ICriterion[] criterion, Order[] ords)
        {
            return GetByCriteria(criterion, ords, null);
        }

        public List<T> GetByCriteria(ICriterion[] criterion, Order[] ords, List<KeyValuePair<String, String>> aliases)
        {
            ICriteria criteria = NHibernateSession.CreateCriteria(persitentType);
            if (criterion != null)
            {
                foreach (ICriterion criterium in criterion)
                {
                    criteria.Add(criterium);
                }
            }

            if (ords != null)
            {
                foreach (Order orden in ords)
                {
                    criteria.AddOrder(orden);
                }
            }

            if (aliases != null && aliases.Count > 0)
            {
                foreach (KeyValuePair<String, String> par in aliases)
                {
                    criteria.CreateAlias(par.Key, par.Value);
                }
            }

            var l = criteria.List<T>();

            if (l != null && l.Count > 0)
            {
                return criteria.List<T>() as List<T>;
            }
            return null;
        }


        /// <summary>
        /// Loads every instance of the requested type using the supplied <see cref="ICriterion" />.
        /// If no <see cref="ICriterion" /> is supplied, this behaves like <see cref="GetAll" />.
        /// </summary>
        public T GetUniqueByCriteria(params ICriterion[] criterion)
        {
            ICriteria criteria = NHibernateSession.CreateCriteria(persitentType);

            if (criterion != null)
            {
                foreach (ICriterion criterium in criterion)
                {
                    criteria.Add(criterium);
                }
            }

            try
            {
                return criteria.UniqueResult<T>();
            }
            catch
            {
                return null;
            }

        }

        public int GetUniqueProjection(params IProjection[] proj)
        {

            ICriteria criteria = NHibernateSession.CreateCriteria(persitentType);

            foreach (IProjection criterium in proj)
            {
                criteria.SetProjection(criterium);
            }

            return criteria.UniqueResult<int>();
        }

        /// <summary>
        /// For entities that have assigned ID's, you must explicitly call Save to add a new one.
        /// See http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
        /// </summary>
        public T Save(T entity)
        {
            NHibernateSession.Save(entity);
            return entity;
        }

        /// <summary>
        /// For entities with automatatically generated IDs, such as identity, SaveOrUpdate may 
        /// be called when saving a new entity.  SaveOrUpdate can also be called to update any 
        /// entity, even if its ID is assigned.
        /// </summary>
        public T SaveOrUpdate(T entity)
        {
            try
            {
                NHibernateSession.SaveOrUpdate(entity);
                CommitChanges();
            }
            catch (StaleObjectStateException)
            {
                try
                {
                    NHibernateSession.Merge(entity);
                }
                catch
                {
                    throw;

                }

            }
            catch (StaleStateException)
            {
                try
                {
                    NHibernateSession.Merge(entity);
                }
                catch
                {
                    throw;

                }

            }

            return entity;
        }

        public void Delete(T entity)
        {
            NHibernateSession.Delete(entity);
        }

        /// <summary>
        /// Commits changes regardless of whether there's an open transaction or not
        /// </summary>
        public void CommitChanges()
        {
            if (NHibernateSessionManager.Instance.HasOpenTransaction())
            {
                NHibernateSessionManager.Instance.CommitTransaction();
            }
            else
            {
                // If there's no transaction, just flush the changes
                NHibernateSessionManager.Instance.GetSession().Flush();
            }
        }

        /// <summary>
        /// Exposes the ISession used within the DAO.
        /// </summary>
        private ISession NHibernateSession
        {
            get
            {
                return NHibernateSessionManager.Instance.GetSession();
            }
        }

        private Type persitentType = typeof(T);
    }
}
