using System.Text.Json.Serialization;

namespace TiendaNamespace;

public class Presupuesto
{
    int idPresupuesto;
    string nombreDestinatario;
    List<PresupuestoDetalle> detalle;

    public int IdPresupuesto { get => idPresupuesto; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    // public List<PresupuestoDetalle> Detalle { get => detalle;}
    public List<PresupuestoDetalle> Detalle {get;} = new List<PresupuestoDetalle>();
    
    
    public void MontoPresupuesto()
    {

    }
    public void MontoPresupuestoConIva()
    {

    }
    public int CantidadProductos()
    {
        return 0;
    }
}
