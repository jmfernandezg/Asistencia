﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using NHibernate template.
// Code is generated on: 8/11/2015 12:15:01 AM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Asistencia.DbDominio
{

    /// <summary>
    /// There are no comments for Asistencia.DbDominio.Plantilla, Asistencia in the schema.
    /// </summary>
    public partial class Plantilla
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for Plantilla constructor in the schema.
        /// </summary>
        public Plantilla()
        {
            FechaCreacion = DateTime.Now;
            FechaModificacion = DateTime.Now;
            this.Status = true;
            OnCreated();
        }


        /// <summary>
        /// There are no comments for Id in the schema.
        /// </summary>
        public virtual int Id
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for IpControlTemplate in the schema.
        /// </summary>
        public virtual string IpControlTemplate
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Enrollnumber in the schema.
        /// </summary>
        public virtual string Enrollnumber
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Nombre in the schema.
        /// </summary>
        public virtual string Nombre
        {
            get;
            set;
        }

        public virtual string NombreControl
        {
            get
            {
                return ControlAcceso != null ? ControlAcceso.Nombre : null;
            }

        }


        /// <summary>
        /// There are no comments for Fingerindex in the schema.
        /// </summary>
        public virtual System.Nullable<int> Fingerindex
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Tmpdata in the schema.
        /// </summary>
        public virtual string Tmpdata
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Privilege in the schema.
        /// </summary>
        public virtual System.Nullable<int> Privilege
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Password in the schema.
        /// </summary>
        public virtual string Password
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Enabled in the schema.
        /// </summary>
        public virtual System.Nullable<bool> Enabled
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Flag in the schema.
        /// </summary>
        public virtual System.Nullable<int> Flag
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for FechaCreacion in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> FechaCreacion
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for FechaModificacion in the schema.
        /// </summary>
        public virtual System.Nullable<System.DateTime> FechaModificacion
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Status in the schema.
        /// </summary>
        public virtual System.Nullable<bool> Status
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Usuario_creado_por in the schema.
        /// </summary>
        public virtual Usuario Usuario_creado_por
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Usuario_modificado_por in the schema.
        /// </summary>
        public virtual Usuario Usuario_modificado_por
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for ControlAcceso in the schema.
        /// </summary>
        public virtual ControlAcceso ControlAcceso
        {
            get;
            set;
        }
    }

}