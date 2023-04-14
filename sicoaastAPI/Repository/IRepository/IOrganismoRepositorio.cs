using sicoaastAPI.Models;

namespace sicoaastAPI.Repository.IRepository
{
    public interface IOrganismoRepositorio
    {
        ICollection<Organismo> GetOrganismos();

        Organismo GetOrganismo(int organismoId);

        bool ExisteOrganismo (int id);

        bool ExisteOrganismo(string name);

        bool CrearOrganismo(Organismo organismo);

        bool ActualizarOrganismo(Organismo organismo);

        bool BorrarOrganismo(Organismo organismo);
        bool Guardar();

        ICollection<Organismo>GetOrganismoEnEmpresa(int empId);
        ICollection<Organismo> BuscarOrganismo(string nombre);
    }
}
