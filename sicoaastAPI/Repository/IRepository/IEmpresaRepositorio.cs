using sicoaastAPI.Models;

namespace sicoaastAPI.Repository.IRepository
{
    public interface IEmpresaRepositorio
    {
        ICollection<Empresa> GetEmpresas();

        Empresa GetEmpresa(int empresaId);

        bool ExisteEmpresa (int id);

        bool ExisteEmpresa(string name);

        bool CrearEmpresa(Empresa empresa);

        bool ActualizarEmpresa(Empresa empresa);

        bool BorrarEmpresa(Empresa empresa);
        bool Guardar();
    }
}
