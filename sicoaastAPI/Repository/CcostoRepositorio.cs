using Microsoft.EntityFrameworkCore;
using sicoaastAPI.Data;
using sicoaastAPI.Models;
using sicoaastAPI.Repository.IRepository;

namespace sicoaastAPI.Repository
{
    public class CcostoRepositorio : ICcostoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public CcostoRepositorio(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ActualizarCcosto(Ccosto ccosto)
        {
            ccosto.FechaCreacion = DateTime.Now;
            _db.Ccostos.Update(ccosto);
            return Guardar();
        }

        public bool BorrarCcosto(Ccosto ccosto)
        {
            _db.Ccostos.Remove(ccosto);
            return Guardar();
        }

        public ICollection<Ccosto> BuscarCcosto(string nombre)
        {
            IQueryable<Ccosto> query = _db.Ccostos;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Name.Contains(nombre) || e.Descripcion.Contains(nombre));
            }
            return query.ToList();
        }

        public bool CrearCcosto(Ccosto ccosto)
        {
            ccosto.FechaCreacion = DateTime.Now;
            _db.Ccostos.Add(ccosto);
            return Guardar();
        }

        public bool ExisteCcosto(int id)
        {
            return _db.Ccostos.Any(e => e.Id == id);
        }

        public bool ExisteCcosto(string name)
        {
            bool valor = _db.Ccostos.Any(e => e.Name.ToLower().Trim() == name.ToLower().Trim());
            return valor;
        }

        public Ccosto GetCcosto(int CcostoId)
        {
            return _db.Ccostos.FirstOrDefault(e => e.Id == CcostoId);
        }

        public ICollection<Ccosto> GetCcostoEnOrganismo(int orgId)
        {
            return _db.Ccostos.Include(em => em.Organismo).Where(em => em.organismoId== orgId).ToList();
        }

        public ICollection<Ccosto> GetCcostos()
        {
            return _db.Ccostos.OrderBy(e => e.Name).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }

}
