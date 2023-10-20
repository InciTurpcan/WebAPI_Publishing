using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Publishing.Database;

namespace WebAPI_Publishing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext context; //this.context

        public ProductsController(DataContext context)
        {
            this.context = context;
        }

        //GET-LİSTELE
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            // return Ok(urunler);
            return Ok(await context.tbl_Products.ToListAsync());
        }


        //delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            /*
            var urun = urunler.Find(p => p.Id == id);
            if (urun == null)
            {
                return BadRequest("Ürün not found");
            }
            urunler.Remove(urun);
 
            return Ok(urunler);
            */
            var urun = await context.tbl_Products.FindAsync(id);
            if (urun == null)
            {
                return BadRequest("Ürün not found");
            }
            context.tbl_Products.Remove(urun);
            await context.SaveChangesAsync();

            return Ok(await context.tbl_Products.ToListAsync());
        }


        //update
        [HttpPut]
        public async Task<ActionResult<List<Product>>> UpdateProduct(Product pr)
        {
            /*
            var urun = urunler.Find(p => p.Id == pr.Id);
            if (urun == null)
            {
                return BadRequest("Ürün not found");
            }
            urun.ProductName = pr.ProductName;
            urun.Price = pr.Price;
            urun.Stock = pr.Stock;
 
            return Ok(urunler);
            */
            var urun = await context.tbl_Products.FindAsync(pr.Id);
            if (urun == null)
            {
                return BadRequest("Ürün not found");
            }
            urun.ProductName = pr.ProductName;
            urun.Price = pr.Price;
            urun.Stock = pr.Stock;

            await context.SaveChangesAsync();
            return Ok(await context.tbl_Products.ToListAsync());
        }


        [HttpPost] //insert
        public async Task<ActionResult<List<Product>>> AddProduct(Product pr)
        {
            /*
            urunler.Add(pr);
            return Ok(urunler);
            */
            context.tbl_Products.Add(pr);
            await context.SaveChangesAsync();
            return Ok(await context.tbl_Products.ToListAsync());
        }

        //Details
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            /*
            var urun = urunler.Find(p => p.Id == id);
            if (urun == null)
            {
                return BadRequest("Ürün not found");
            }
            return Ok(urun);
            */
            var urun = await context.tbl_Products.FindAsync(id);
            if (urun == null)
            {
                return BadRequest("Ürün not found");
            }
            return Ok(urun);
        }


        private static List<Product> urunler = new List<Product>()
        {
            new Product
            {
                Id = 1,
                ProductName = "Ürün1",
                Price = 1000,
                Stock = 35
            },
            new Product
            {
                Id = 2,
                ProductName = "Ürün2",
                Price = 2000,
                Stock = 20
            } ,
             new Product
            {
                Id = 3,
                ProductName = "Ürün3",
                Price = 3000,
                Stock = 48
            }
        };
    }
}
