using Microsoft.AspNetCore.Mvc;
using TiendaNamespace;
using ProductoRepositoryNamespace;
namespace tl2_tp5_2024_Daggam.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{
    ProductoRepository productoRepositorio;

    public ProductosController()
    {
        productoRepositorio = new SQLiteProductoRepository();
    }

    [HttpPost()]
    public ActionResult CrearProducto([FromBody] Producto producto){
        if(!productoRepositorio.crearProducto(producto.Descripcion,producto.Precio)) return BadRequest();
        return Created();
    }
    [HttpPut("{id}")]
    public ActionResult ModificarProducto(int id,[FromBody] Producto producto){
        if(!productoRepositorio.modificarProducto(id,producto.Descripcion,producto.Precio)) return BadRequest();
        return Accepted();
    }
    [HttpGet()]
    public ActionResult<List<Producto>?> obtenerProductos(){
        List<Producto>? productos = productoRepositorio.obtenerProductos();
        if(productos==null) return NotFound();
        return Ok(productos);
    }
    [HttpGet("{id}")]
    public ActionResult<Producto?> obtenerProducto(int id){
        Producto? producto = productoRepositorio.obtenerProducto(id);
        if(producto == null) return NotFound();
        return Ok(producto);
    }   
}
