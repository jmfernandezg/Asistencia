using NHibernate.Criterion;
using PasswordHash;
using System;
using System.Collections.Generic;

namespace Asistencia.DbDao
{
    public class UsuarioDao : AbstractNHibernateDao<Usuario, Int32>, IUsuarioDao

    {


        public Usuario GetByUsuario(String usuario)
        {
            return GetByUsuarioContrasena(usuario, null);
        }

        public Usuario GetByUsuarioContrasena(String usuario, String contrasena)
        {

            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Activo", true));
            lista.Add(Restrictions.Eq("Habilitado", true));
            lista.Add(Restrictions.Eq("Username", usuario));

            if (contrasena != null)
            {
                lista.Add(Restrictions.Eq("Password", Encrypt.MD5(contrasena)));

            }

            return GetUniqueByCriteria(lista.ToArray());
        }


        public List<Usuario> GetListado()
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("Habilitado", true));
            return GetByCriteria(lista.ToArray());
        }

        public long GetConteo()
        {
            return GetListado().Count;
        }


        public Usuario DoBloquear(Usuario u)
        {
            u.Activo = false;
            return SaveOrUpdate(u);
        }

        public Usuario DoEliminar(Usuario u)
        {
            u.Habilitado = false;
            return SaveOrUpdate(u);
        }

        public Usuario GetById(Int32 id)
        {
            List<ICriterion> lista = new List<ICriterion>();
            lista.Add(Restrictions.Eq("CveUsuario", id));
            return GetUniqueByCriteria(lista.ToArray());
        }

    }
}