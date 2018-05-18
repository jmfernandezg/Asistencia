namespace Asistencia.DbDao
{
    /// <summary>
    /// Exposes access to NHibernate DAO classes.  Motivation for this DAO
    /// framework can be found at http://www.hibernate.org/328.html.
    /// </summary>
    public class NHibernateDaoFactory : IDaoFactory
    {

        public IUsuarioDao GetUsuarioDao()
        {
            return new UsuarioDao();
        }

        public IOficinaDao GetOficinaDao()
        {
            return new OficinaDao();
        }

        public IEmpleadoDao GetEmpleadoDao()
        {
            return new EmpleadoDao();
        }

        public IControlAccesoDao GetControlAccesoDao()
        {
            return new ControlAccesoDao();
        }

        public IPerfilDao GetPerfilDao()
        {
            return new PerfilDao();
        }

        public IZonaDao GetZonaDao()
        {
            return new ZonaDao();
        }

        public IRegionDao GetRegionDao()
        {
            return new RegionDao();
        }

        public IPlazaDao GetPlazaDao()
        {
            return new PlazaDao();
        }

        public IDetalleCatalogoDao GetDetalleCatalogoDao()
        {
            return new DetalleCatalogoDao();
        }

        public IColectorMovimientoDao GetColectorMovimientosDao()
        {
            return new ColectorMovimientoDao();
        }

        public IColectorMovimientoIncidenciaDao GetColectorMovimientosIncidenciaDao()
        {
            return new ColectorMovimientoIncidenciaDao();
        }

        public IIncidenciaDao GetIncidenciaDao()
        {
            return new IncidenciaDao();
        }

        public IPlantillaDao GetPlantillaDao()
        {
            return new PlantillaDao();
        }

        public IEmpleadoControlAccesoDao GetEmpleadoControlAccesoDao()
        {
            return new EmpleadoControlAccesoDao();
        }
    }
}
