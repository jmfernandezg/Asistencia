
namespace Asistencia.DbInterfase
{
    /// <summary>
    /// Provides an interface for retrieving DAO objects
    /// </summary>
    public interface IDaoFactory
    {
        IUsuarioDao GetUsuarioDao();

        IOficinaDao GetOficinaDao();

        IEmpleadoDao GetEmpleadoDao();

        IControlAccesoDao GetControlAccesoDao();

        IPerfilDao GetPerfilDao();

        IZonaDao GetZonaDao();

        IRegionDao GetRegionDao();

        IPlazaDao GetPlazaDao();

        IDetalleCatalogoDao GetDetalleCatalogoDao();

        IColectorMovimientoDao GetColectorMovimientosDao();
        IColectorMovimientoIncidenciaDao GetColectorMovimientosIncidenciaDao();

        IIncidenciaDao GetIncidenciaDao();

        IPlantillaDao GetPlantillaDao();

        IEmpleadoControlAccesoDao GetEmpleadoControlAccesoDao();



    }


}
