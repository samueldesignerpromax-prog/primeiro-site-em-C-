using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PadariaWeb.Data;
using PadariaWeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PadariaWeb.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string categoria, string busca)
        {
            var produtos = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(categoria) && categoria != "Todos")
            {
                produtos = produtos.Where(p => p.Categoria == categoria);
            }

            if (!string.IsNullOrEmpty(busca))
            {
                produtos = produtos.Where(p => p.Nome.Contains(busca) || p.Descricao.Contains(busca));
            }

            ViewBag.Categorias = await _context.Produtos
                .Select(p => p.Categoria)
                .Distinct()
                .ToListAsync();

            var produtosList = await produtos.Where(p => p.Estoque > 0).ToListAsync();

            return View(produtosList);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
    }
}
