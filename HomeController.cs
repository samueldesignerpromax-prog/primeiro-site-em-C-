using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PadariaWeb.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PadariaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var produtosEmDestaque = await _context.Produtos
                .Where(p => p.Destaque && p.Estoque > 0)
                .Take(6)
                .ToListAsync();

            return View(produtosEmDestaque);
        }

        public IActionResult Sobre()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contato(string nome, string email, string mensagem)
        {
            // Lógica para enviar email ou salvar mensagem
            TempData["Mensagem"] = "Mensagem enviada com sucesso!";
            return RedirectToAction("Contato");
        }
    }
}
