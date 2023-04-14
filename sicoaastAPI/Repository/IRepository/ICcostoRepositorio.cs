using sicoaastAPI.Models;

namespace sicoaastAPI.Repository.IRepository
{
    public interface ICcostoRepositorio
    {
        ICollection<Ccosto> GetCcostos();

        Ccosto GetCcosto(int ccostoId);

        bool ExisteCcosto (int id);

        bool ExisteCcosto(string name);

        bool CrearCcosto(Ccosto ccosto);

        bool ActualizarCcosto(Ccosto ccosto);

        bool BorrarCcosto(Ccosto ccosto);
        bool Guardar();

        ICollection<Ccosto>GetCcostoEnOrganismo(int orgId);
        ICollection<Ccosto> BuscarCcosto(string nombre);
    }
}
