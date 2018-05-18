using System;
using Asistencia.Clases;

namespace Asistencia.Catalogo
{
    public partial class OficinaAbm : PaginaExtensible
    {
        Asistencia.DbInterfase.IOficinaDao oficinaDao;
        Asistencia.DbInterfase.IPlazaDao plazaDao;
        Asistencia.DbInterfase.IZonaDao zonaDao;
        Asistencia.DbInterfase.IRegionDao regionDao;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cargamos los datos
            oficinaDao = daoFactory.GetOficinaDao();
            plazaDao = daoFactory.GetPlazaDao();
            zonaDao = daoFactory.GetZonaDao();
            regionDao = daoFactory.GetRegionDao();


            if (!IsPostBack)
            {

                // Cargamos el DropDown de Region
                txtRegion.Items.Clear();
                txtRegion.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                foreach (DbDominio.Region obj in listaDeRegion())
                {
                    txtRegion.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                }

                // Cargamos el DropDown de Zona
                txtZona.Items.Clear();
                txtZona.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                foreach (DbDominio.Zona obj in listaDeZona())
                {
                    txtZona.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                }

                // Cargamos el DropDown de Plaza
                txtPlaza.Items.Clear();
                txtPlaza.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                foreach (DbDominio.Plaza obj in listaDePlaza())
                {
                    txtPlaza.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                }

                // Validacion si es un registro nuevo o una edicion
                if (Session[Constantes.WEB_VARIABLE_SESSION_ID] == null)
                {
                    Title = "Alta de Registro";
                    txtId.Value = null;
                    txtNombre.Text = null;
                    txtCodigoPlanta.Text = null;

                }
                else
                {
                    try
                    {
                        Asistencia.DbDominio.Oficina obj = oficinaDao.GetById(Int32.Parse(Session[Constantes.WEB_VARIABLE_SESSION_ID].ToString()));
                        Title = "Edición de Registro";
                        txtId.Value = obj.CveOficina.ToString();
                        txtNombre.Text = obj.Nombre;
                        txtCodigoPlanta.Text = obj.CodigoPlanta;


                        if (obj.Plaza != null)
                        {
                            txtPlaza.SelectedValue = obj.Plaza.Id.ToString();
                        }

                        if (obj.Region != null)
                        {
                            txtRegion.SelectedValue = obj.Region.Id.ToString();
                        }
                        if (obj.Zona != null)
                        {
                            txtZona.SelectedValue = obj.Zona.Id.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        log.Error(String.Format("Error al intentar Obtener los datos De la Oficina a Editar. Mensaje: [{0}] ", ex.Message));
                        ManejarExcepcion(ex);
                    }

                }
            }

        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                if (!ValidarObjeto(txtCodigoPlanta.Text))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo Codigo Planta");
                    return;
                }

                if (!ValidarObjeto(txtNombre.Text))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo Codigo Nombre");
                    return;
                }

                if (!ValidarObjeto(txtZona.SelectedValue))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo Zona");
                    return;
                }
                if (!ValidarObjeto(txtRegion.SelectedValue))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo region");
                    return;
                }
                if (!ValidarObjeto(txtPlaza.SelectedValue))
                {
                    MostrarExcepcion(Constantes.MENSAJE_CAMPO_REQUERIDO_TITULO, "Es requerido seleccionar el campo plaza");
                    return;
                }


                Asistencia.DbDominio.Oficina obj = ValidarObjeto(txtId.Value) ? oficinaDao.GetById(Int32.Parse(txtId.Value)) : null;

                if (obj == null)
                {
                    log.Info(String.Format("Se intenta insertar un registro nuevo de registro de Oficina por el usuario [{0}]", UsuarioActual.Nombre));

                    obj = new DbDominio.Oficina();
                    obj.Usuario_creado_por = UsuarioActual;
                    obj.DetalleCatalogo = daoFactory.GetDetalleCatalogoDao().GetById(Constantes.DETALLE_CATALOGO_OFICINA);
                }
                else
                {
                    log.Info(String.Format("Se intenta actualizar el registro de Oficina con ID [{0}] por el usuario [{1}]", txtId.Value, UsuarioActual.Nombre));
                }

                obj.Usuario_modificado_por = UsuarioActual;
                obj.FechaModificacion = DateTime.Now;
                obj.Nombre = txtNombre.Text;
                obj.CodigoPlanta = txtCodigoPlanta.Text;


                obj.Plaza = daoFactory.GetPlazaDao().GetById(Int32.Parse(txtPlaza.SelectedValue));
                obj.Region = daoFactory.GetRegionDao().GetById(Int32.Parse(txtRegion.SelectedValue));
                obj.Zona = daoFactory.GetZonaDao().GetById(Int32.Parse(txtZona.SelectedValue));

                oficinaDao.SaveOrUpdate(obj);
                MostrarExito("Proceso Correcto", "El proceso de guardado se completo con exito");

                if (!ValidarObjeto(txtId.Value))
                {
                    Redirigir(Constantes.WEB_PAGINA_CATALOGO_OFICINA);
                }

            }
            catch (Exception ex)
            {
                log.Error(String.Format("Error al intentar Guardar los datos De la Oficina. Mensaje: [{0}] ", ex.Message));
                ManejarExcepcion(ex);
            }
        }


        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Redirigir(Constantes.WEB_PAGINA_CATALOGO_OFICINA);
        }

        protected void btnNuevaZona_Click(object sender, EventArgs e)
        {
            if (ValidarObjeto(txtNuevaZona.Text))
            {
                try
                {
                    log.Info(String.Format("Se intenta insertar un registro nuevo de registro de Zona por el usuario [{0}], con nombre [{1}]", UsuarioActual.Nombre, txtNuevaZona.Text));

                    DbDominio.Zona zona = zonaDao.GetByNombre(txtNuevaZona.Text);
                    if (zona == null)
                    {
                        zona = new DbDominio.Zona();
                        zona.Usuario_creado_por = UsuarioActual;

                    }

                    zona.Nombre = txtNuevaZona.Text;
                    zona.Usuario_modificado_por = UsuarioActual;
                    zona.FechaModificacion = DateTime.Now;
                    zona.Habilitado = true;
                    zona.Activo = true;
                    zonaDao.SaveOrUpdate(zona);

                    // Cargamos nuevamente el DropDown
                    txtZona.Items.Clear();
                    txtZona.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                    foreach (DbDominio.Zona obj in listaDeZona())
                    {
                        txtZona.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                    }


                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error al intentar Guardar los datos De la Zona. Mensaje: [{0}] ", ex.Message));
                    ManejarExcepcion(ex);
                }
            }
            txtNuevaZona.Text = null;
        }

        protected void btnNuevaRegion_Click(object sender, EventArgs e)
        {
            if (ValidarObjeto(txtNuevaRegion.Text))
            {
                log.Info(String.Format("Se intenta insertar un registro nuevo de registro de Region por el usuario [{0}], con nombre [{1}]", UsuarioActual.Nombre, txtNuevaRegion.Text));

                try
                {
                    DbDominio.Region region = regionDao.GetByNombre(txtNuevaRegion.Text);
                    if (region == null)
                    {
                        region = new DbDominio.Region();
                        region.Usuario_creado_por = UsuarioActual;
                    }

                    region.Nombre = txtNuevaRegion.Text;
                    region.Usuario_modificado_por = UsuarioActual;
                    region.FechaModificacion = DateTime.Now;
                    region.Habilitado = true;
                    region.Activo = true;
                    regionDao.SaveOrUpdate(region);

                    // Cargamos nuevamente el dropdown
                    txtRegion.Items.Clear();
                    txtRegion.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                    foreach (DbDominio.Region obj in listaDeRegion())
                    {
                        txtRegion.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                    }

                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error al intentar Guardar los datos De la Region. Mensaje: [{0}] ", ex.Message));
                    ManejarExcepcion(ex);
                }
            }
            txtNuevaRegion.Text = null;
        }

        protected void btnNuevaPlaza_Click(object sender, EventArgs e)
        {
            if (ValidarObjeto(txtNuevaPlaza.Text))
            {
                log.Info(String.Format("Se intenta insertar un registro nuevo de registro de Plaza por el usuario [{0}], con nombre [{1}]", UsuarioActual.Nombre, txtNuevaPlaza.Text));

                try
                {
                    DbDominio.Plaza plaza = plazaDao.GetByNombre(txtNuevaPlaza.Text);
                    if (plaza == null)
                    {

                        plaza = new DbDominio.Plaza();
                        plaza.Usuario_creado_por = UsuarioActual;

                    }
                    plaza.Nombre = txtNuevaPlaza.Text;
                    plaza.Habilitado = true;
                    plaza.Activo = true;
                    plaza.Usuario_modificado_por = UsuarioActual;
                    plaza.FechaModificacion = DateTime.Now;
                    plazaDao.SaveOrUpdate(plaza);

                    // Cargamos nuevamente el dropdown
                    txtPlaza.Items.Clear();
                    txtPlaza.Items.Add(NuevoListItem(Constantes.TEXTO_SELECCION, Constantes.TEXTO_BLANCO));
                    foreach (DbDominio.Plaza obj in listaDePlaza())
                    {
                        txtPlaza.Items.Add(NuevoListItem(obj.Nombre, obj.Id.ToString()));
                    }


                }
                catch (Exception ex)
                {
                    log.Error(String.Format("Error al intentar Guardar los datos De la Plaza. Mensaje: [{0}] ", ex.Message));
                    ManejarExcepcion(ex);
                }
            }
            txtNuevaPlaza.Text = null;
        }
    }
}