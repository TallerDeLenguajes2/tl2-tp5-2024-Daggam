namespace TiendaNamespace;

public class PresupuestoDetalle
{
    Producto producto;
    int cantidad;

    public Producto Producto { get => producto; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}