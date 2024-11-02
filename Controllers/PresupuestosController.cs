using Microsoft.AspNetCore.Mvc;
using TiendaNamespace;
using PresupuestoRepositoryNamespace;
namespace tl2_tp5_2024_Daggam.Controllers;

[ApiController]
[Route("[controller]")]
public class PresupuestosController:ControllerBase{
    PresupuestoRepository presupuestoRepository;
    public PresupuestosController()
    {
        presupuestoRepository = new SQLitePresupuestoRepository();
    }

    [HttpPost()]
    public ActionResult crearPresupuesto([FromBody] Presupuesto presupuesto){
        if(!presupuestoRepository.crearPresupuesto(presupuesto.NombreDestinatario)) return BadRequest();
        return Created();
    }

    [HttpGet()]
    public ActionResult<List<Presupuesto>> obtenerPresupuestos(){
        List<Presupuesto>? presupuestos = presupuestoRepository.obtenerPresupuestos();
        if(presupuestos==null) return BadRequest();
        return Ok(presupuestos);
    }

    [HttpGet("{id}")]
    public ActionResult<Presupuesto> obtenerPresupuesto(int id){
        Presupuesto? presupuesto = presupuestoRepository.obtenerPresupuesto(id);
        if(presupuesto==null) return NotFound();
        return Ok(presupuesto);
    }
}