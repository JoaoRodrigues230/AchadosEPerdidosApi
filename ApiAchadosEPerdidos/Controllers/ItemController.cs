using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAchadosEPerdidos.Models;

namespace ApiAchadosEPerdidos.Controllers
{
    [Route("api/listar-itens")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly AchadosContext _context;

        public ItemController(AchadosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items
                .AsNoTracking()
                .Include(i => i.Categoria)
                .Include(i => i.Local)
                .Include(i => i.Usuarioqueachou)
                .Include(i => i.Imagem)
                .OrderByDescending(i => i.Dataachado)
                .Take(12)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = await _context.Items
                .AsNoTracking()
                .Include(i => i.Categoria)
                .Include(i => i.Local)
                .Include(i => i.Usuarioqueachou)
                .Include(i => i.Imagem)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(Item item)
        {
            item.Id = Guid.NewGuid();
            item.Dataachado = DateTime.UtcNow;
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }
    }
}
