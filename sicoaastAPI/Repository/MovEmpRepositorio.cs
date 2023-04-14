using Microsoft.EntityFrameworkCore;
using sicoaastAPI.Data;
using sicoaastAPI.Models;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Repository
{
    public class MovEmpRepositorio : IMovEmpRepositorio
    {
        private readonly ApplicationDbContext _db;
        public MovEmpRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ActualizarMovEmp(MovEmp movEmp)
        {
            movEmp.FechaActualizacion = DateTime.Now;
            _db.MovEmp.Update(movEmp);
            return Guardar();
        }

        public bool BorrarMovEmp(MovEmp movEmp)
        {
            movEmp.activo = 0;
            movEmp.FechaBaja = DateTime.Now;
            _db.MovEmp.Update(movEmp);
            return Guardar(); 
        }

        public bool ReactivarMovEmp(MovEmp movEmp)
        {
            movEmp.activo = 1;
            movEmp.FechaReactivacion = DateTime.Now;
            _db.MovEmp.Update(movEmp);
            return Guardar();
        }

        public ICollection<MovEmp> BuscarMovEmp(string nombre)
        {
            IQueryable<MovEmp> query = _db.MovEmp;
            if (!string.IsNullOrEmpty(nombre))
            {
                //query = query.Where(e => e.Nombre.Contains(nombre) || e.Apaterno.Contains(nombre) || e.Amaterno.Contains(nombre) && e.activo == 1);
                query = query.Where(e => e.NombreCompleto.Contains(nombre) && e.activo == 1);
            }
            return query.ToList();
        }

        public bool CrearMovEmp(MovEmp movEmp)
        {
            movEmp.FechaAlta = DateTime.Now;
            movEmp.Folio = 1002;
            movEmp.activo = 1;
            movEmp.NombreCompleto = movEmp.Nombre + " " + movEmp.Apaterno + " " + movEmp.Amaterno;
            _db.MovEmp.Add(movEmp);
            return Guardar();
        }

        public bool ExisteMovEmp(int id)
        {
            return _db.MovEmp.Any(e => e.Id == id);
        }

        public bool ExisteMovEmp(string name)
        {
            bool valor = _db.MovEmp.Any(e => e.Nombre.ToLower().Trim() == name.ToLower().Trim());
            return valor;
        }

        public MovEmp GetMovEmp(int MovEmpId)
        {
            return _db.MovEmp.FirstOrDefault(em => em.Id == MovEmpId && em.activo == 1);
        }

        public MovEmp GetMovEmpActivo(int movEmpId)
        {
            return _db.MovEmp.FirstOrDefault(em => em.Id == movEmpId);
        }

        public ICollection<MovEmp> GetMovEmpEnEmpresa(int empId)
        {
            return _db.MovEmp.Include(em => em.Empresa).Where(em => em.empresaId == empId && em.activo == 1).ToList();
        }

        public ICollection<MovEmp> GetMovEmpEnOrganismo(int orgId)
        {
            return _db.MovEmp.Include(em => em.Organismo).Where(em => em.organismoId == orgId).ToList();
        }

        public ICollection<MovEmp> GetMovEmpEnCcosto(int ccosId)
        {
            return _db.MovEmp.Include(em => em.Ccosto).Where(em => em.ccostoId == ccosId).ToList();
        }

        public ICollection<MovEmp> GetMovEmpEnDepartamento(int depId)
        {
            return _db.MovEmp.Include(em => em.Departamento).Where(em => em.departamentoId == depId).ToList();
        }

        public ICollection<MovEmp> GetMovEmps()
        {
            return _db.MovEmp.Where(em => em.activo == 1).OrderBy(em => em.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }

}
