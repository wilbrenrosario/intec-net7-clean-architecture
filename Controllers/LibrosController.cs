using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INTEC.CORE.Model;
using INTEC.INFRASTRUCTURE.Persistence;

namespace INTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public LibrosController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libros>>> GetLibros()
        {
          if (_context.Libros == null)
          {
              return NotFound();
          }
            return await _context.Libros.ToListAsync();
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libros>> GetLibros(int id)
        {
          if (_context.Libros == null)
          {
              return NotFound();
          }
            var libros = await _context.Libros.FindAsync(id);

            if (libros == null)
            {
                return NotFound();
            }

            return libros;
        }

        [HttpGet("LibrosFiltro/{data}")]
        public async Task<List<Libros>> GetLibrosFiltros(string data)
        {
            var libros = _context.Libros.Where(x => x.codigoeditorial.Equals(data)).ToList();

            return libros;
        }


        // PUT: api/Libros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibros(int id, Libros libros)
        {
            if (id != libros.id)
            {
                return BadRequest();
            }

            _context.Entry(libros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibrosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Libros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Libros>> PostLibros(Libros libros)
        {
          if (_context.Libros == null)
          {
              return Problem("Entity set 'INTECContext.Libros'  is null.");
          }
            _context.Libros.Add(libros);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibros", new { id = libros.id }, libros);
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibros(int id)
        {
            if (_context.Libros == null)
            {
                return NotFound();
            }
            var libros = await _context.Libros.FindAsync(id);
            if (libros == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libros);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibrosExists(int id)
        {
            return (_context.Libros?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
