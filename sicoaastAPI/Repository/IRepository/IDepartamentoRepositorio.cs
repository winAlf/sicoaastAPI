using sicoaastAPI.Models;

namespace sicoaastAPI.Repository.IRepository
{
    public interface IDepartamentoRepositorio
    {
        ICollection<Departamento> GetDepartamentos();

        Departamento GetDepartamento(int departamentoId);

        bool ExisteDepartamento (int id);

        bool ExisteDepartamento(string name);

        bool CrearDepartamento(Departamento departamento);

        bool ActualizarDepartamento(Departamento departamento);

        bool BorrarDepartamento(Departamento departamento);
        bool Guardar();

        ICollection<Departamento>GetDepartamentoEnCcosto(int ccosId);
        ICollection<Departamento> BuscarDepartamento(string nombre);
    }
}
