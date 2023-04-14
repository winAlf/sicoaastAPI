using Microsoft.EntityFrameworkCore;
using sicoaastAPI.Data;
using sicoaastAPI.Models;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Repository
{
    public class OrganismoRepositorio : IOrganismoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public OrganismoRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ActualizarOrganismo(Organismo organismo)
        {
            organismo.FechaCreacion = DateTime.Now;
            _db.Organismos.Update(organismo);
            return Guardar();
        }

        public bool BorrarOrganismo(Organismo organismo)
        {
            _db.Organismos.Remove(organismo);
            return Guardar();
        }

        public ICollection<Organismo> BuscarOrganismo(string nombre)
        {
            IQueryable<Organismo> query = _db.Organismos;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Name.Contains(nombre) || e.Descripcion.Contains(nombre));
            }
            return query.ToList();
        }

        public bool CrearOrganismo(Organismo organismo)
        {
            organismo.FechaCreacion = DateTime.Now;
            _db.Organismos.Add(organismo);
            return Guardar();
        }

        public bool ExisteOrganismo(int id)
        {
            return _db.Organismos.Any(e => e.Id == id);
        }

        public bool ExisteOrganismo(string name)
        {
            bool valor = _db.Organismos.Any(e => e.Name.ToLower().Trim() == name.ToLower().Trim());
            return valor;
        }

        public Organismo GetOrganismo(int OrganismoId)
        {
            return _db.Organismos.FirstOrDefault(e => e.Id == OrganismoId);
        }

        public ICollection<Organismo> GetOrganismoEnEmpresa(int empId)
        {
            return _db.Organismos.Include(em => em.Empresa).Where(em => em.empresaId== empId).ToList();
        }

        public ICollection<Organismo> GetOrganismos()
        {
            return _db.Organismos.OrderBy(e => e.Name).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }

}
