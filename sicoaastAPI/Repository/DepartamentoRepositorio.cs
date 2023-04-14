using Microsoft.EntityFrameworkCore;
using sicoaastAPI.Data;
using sicoaastAPI.Models;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Repository
{
    public class DepartamentoRepositorio : IDepartamentoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public DepartamentoRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ActualizarDepartamento(Departamento departamento)
        {
            departamento.FechaCreacion = DateTime.Now;
            _db.Departamentos.Update(departamento);
            return Guardar();
        }

        public bool BorrarDepartamento(Departamento departamento)
        {
            _db.Departamentos.Remove(departamento);
            return Guardar();
        }

        public ICollection<Departamento> BuscarDepartamento(string nombre)
        {
            IQueryable<Departamento> query = _db.Departamentos;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Name.Contains(nombre) || e.Descripcion.Contains(nombre));
            }
            return query.ToList();
        }

        public bool CrearDepartamento(Departamento departamento)
        {
            departamento.FechaCreacion = DateTime.Now;
            _db.Departamentos.Add(departamento);
            return Guardar();
        }

        public bool ExisteDepartamento(int id)
        {
            return _db.Departamentos.Any(e => e.Id == id);
        }

        public bool ExisteDepartamento(string name)
        {
            bool valor = _db.Departamentos.Any(e => e.Name.ToLower().Trim() == name.ToLower().Trim());
            return valor;
        }

        public Departamento GetDepartamento(int DepartamentoId)
        {
            return _db.Departamentos.FirstOrDefault(e => e.Id == DepartamentoId);
        }

        public ICollection<Departamento> GetDepartamentoEnCcosto(int ccosId)
        {
            return _db.Departamentos.Include(em => em.Ccosto).Where(em => em.ccostoId == ccosId).ToList();
        }

        public ICollection<Departamento> GetDepartamentos()
        {
            return _db.Departamentos.OrderBy(e => e.Name).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }

}
