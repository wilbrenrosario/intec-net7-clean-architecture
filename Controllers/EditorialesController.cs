using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INTEC.CORE.Model;
using INTEC.INFRASTRUCTURE.Persistence;

namespace INTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialesController : ControllerBase
    {
        private readonly IApplicationDBContext _context;

        public EditorialesController(IApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Editoriales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Editoriales>>> GetEditoriales()
        {
          if (_context.Editoriales == null)
          {
              return NotFound();
          }
            return await _context.Editoriales.ToListAsync();
        }

        // GET: api/Editoriales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Editoriales>> GetEditoriales(int id)
        {
          if (_context.Editoriales == null)
          {
              return NotFound();
          }
            var editoriales = await _context.Editoriales.FindAsync(id);

            if (editoriales == null)
            {
                return NotFound();
            }

            return editoriales;
        }

        // PUT: api/Editoriales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEditoriales(int id, Editoriales editoriales)
        {
            if (id != editoriales.id)
            {
                return BadRequest();
            }

            _context.Editoriales.Entry(editoriales).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditorialesExists(id))
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

        // POST: api/Editoriales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Editoriales>> PostEditoriales(Editoriales editoriales)
        {
          if (_context.Editoriales == null)
          {
              return Problem("Entity set 'INTECContext.Editoriales'  is null.");
          }
            _context.Editoriales.Add(editoriales);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEditoriales", new { id = editoriales.id }, editoriales);
        }

        // DELETE: api/Editoriales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEditoriales(int id)
        {
            if (_context.Editoriales == null)
            {
                return NotFound();
            }
            var editoriales = await _context.Editoriales.FindAsync(id);
            if (editoriales == null)
            {
                return NotFound();
            }

            _context.Editoriales.Remove(editoriales);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EditorialesExists(int id)
        {
            return (_context.Editoriales?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
