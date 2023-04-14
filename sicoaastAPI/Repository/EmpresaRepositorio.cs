using sicoaastAPI.Data;
using sicoaastAPI.Models;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Repository
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public EmpresaRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ActualizarEmpresa(Empresa empresa)
        {
            empresa.FechaCreacion = DateTime.Now;
            _db.Empresas.Update(empresa);
            return Guardar();
        }

        public bool BorrarEmpresa(Empresa empresa)
        {
            _db.Empresas.Remove(empresa);
            return Guardar();
        }

        public bool CrearEmpresa(Empresa empresa)
        {
            empresa.FechaCreacion = DateTime.Now;
            _db.Empresas.Add(empresa);
            return Guardar();
        }

        public bool ExisteEmpresa(int id)
        {
            return _db.Empresas.Any(e => e.Id == id);
        }

        public bool ExisteEmpresa(string name)
        {
            bool valor = _db.Empresas.Any(e => e.Name.ToLower().Trim() == name.ToLower().Trim());
            return valor;
        }

        public Empresa GetEmpresa(int empresaId)
        {
            return _db.Empresas.FirstOrDefault(e => e.Id == empresaId);
        }

        public ICollection<Empresa> GetEmpresas()
        {
            return _db.Empresas.OrderBy(e => e.Name).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
