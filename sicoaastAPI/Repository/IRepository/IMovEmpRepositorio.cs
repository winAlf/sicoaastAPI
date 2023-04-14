using sicoaastAPI.Models;

namespace sicoaastAPI.Repository.IRepository
{
    public interface IMovEmpRepositorio
    {
        ICollection<MovEmp> GetMovEmps();

        MovEmp GetMovEmp(int movEmpId);
        MovEmp GetMovEmpActivo(int movEmpId);
        bool ExisteMovEmp (int id);

        bool ExisteMovEmp(string name);

        bool CrearMovEmp(MovEmp movEmp);

        bool ActualizarMovEmp(MovEmp movEmp);

        bool BorrarMovEmp(MovEmp movEmp);
        bool ReactivarMovEmp(MovEmp movEmp);
        bool Guardar();

        ICollection<MovEmp> GetMovEmpEnEmpresa(int empId);
        ICollection<MovEmp> GetMovEmpEnOrganismo(int orgId);
        ICollection<MovEmp> GetMovEmpEnCcosto(int ccosId);
        ICollection<MovEmp> GetMovEmpEnDepartamento(int depId);
        ICollection<MovEmp> BuscarMovEmp(string textoBuscar);
    }
}
