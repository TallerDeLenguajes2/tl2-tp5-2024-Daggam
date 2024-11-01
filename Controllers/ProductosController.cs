using Microsoft.AspNetCore.Mvc;
using TiendaNamespace;
using ProductoRepositoryNamespace;
namespace tl2_tp5_2024_Daggam.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{
    ProductoRepository ProductoRepositorio;

    public ProductosController()
    {
        ProductoRepositorio = new SQLiteProductoRepository();
    }

    [HttpPost()]
    public ActionResult CrearProducto([FromBody] Producto producto){
        if(!ProductoRepositorio.crearProducto(producto.Descripcion,producto.Precio)) return BadRequest();
        return Created();
    }
    [HttpPut("{id}")]
    public ActionResult ModificarProducto(int id,[FromBody] Producto producto){
        if(!ProductoRepositorio.modificarProducto(id,producto.Descripcion,producto.Precio)) return BadRequest();
        return Accepted();
    }
}
