using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PadariaWeb.Data;
using PadariaWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PadariaWeb.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrinhoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var carrinho = GetCarrinho();
            return View(carrinho);
        }

        public async Task<IActionResult> Adicionar(int id, int quantidade = 1)
        {
            var produto = await _context.Produtos.FindAsync(id);
            
            if (produto != null && produto.Estoque >= quantidade)
            {
                var carrinho = GetCarrinho();
                var item = carrinho.FirstOrDefault(i => i.ProdutoId == id);

                if (item == null)
                {
                    carrinho.Add(new ItemCarrinho
                    {
                        ProdutoId = produto.Id,
                        ProdutoNome = produto.Nome,
                        Preco = produto.Preco,
                        Quantidade = quantidade,
                        ImagemUrl = produto.ImagemUrl
                    });
                }
                else
                {
                    item.Quantidade += quantidade;
                }

                SaveCarrinho(carrinho);
                TempData["Mensagem"] = $"{produto.Nome} adicionado ao carrinho!";
            }

            return RedirectToAction("Index", "Produtos");
        }

        public IActionResult Remover(int id)
        {
            var carrinho = GetCarrinho();
            var item = carrinho.FirstOrDefault(i => i.ProdutoId == id);
            
            if (item != null)
            {
                carrinho.Remove(item);
                SaveCarrinho(carrinho);
            }

            return RedirectToAction("Index");
        }

        public IActionResult AtualizarQuantidade(int id, int quantidade)
        {
            var carrinho = GetCarrinho();
            var item = carrinho.FirstOrDefault(i => i.ProdutoId == id);
            
            if (item != null)
            {
                if (quantidade <= 0)
                {
                    carrinho.Remove(item);
                }
                else
                {
                    item.Quantidade = quantidade;
                }
                SaveCarrinho(carrinho);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Limpar()
        {
            SaveCarrinho(new List<ItemCarrinho>());
            return RedirectToAction("Index");
        }

        private List<ItemCarrinho> GetCarrinho()
        {
            var carrinho = HttpContext.Session.Get<List<ItemCarrinho>>("Carrinho");
            return carrinho ?? new List<ItemCarrinho>();
        }

        private void SaveCarrinho(List<ItemCarrinho> carrinho)
        {
            HttpContext.Session.Set("Carrinho", carrinho);
        }
    }

    public class ItemCarrinho
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string ImagemUrl { get; set; }
        public decimal Subtotal => Preco * Quantidade;
    }
}
