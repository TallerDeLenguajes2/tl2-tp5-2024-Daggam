namespace TiendaNamespace;

public class PresupuestoDetalle
{
    Productos producto;
    int cantidad;

    public Productos Producto { get => producto; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}