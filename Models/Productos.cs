namespace TiendaNamespace;

public class Productos
{
    int idProducto;
    string descripcion;
    int precio;

    public int IdProducto { get => idProducto; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Precio { get => precio; set => precio = value; }
}
