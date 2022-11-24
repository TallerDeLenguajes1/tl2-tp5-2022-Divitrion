using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class CadeteriaWeb
    {
    protected string nombre;
    protected string telefono;
    public List<Cadete> ListadoCadetes;

    public CadeteriaWeb()
    {
        ListadoCadetes = new List<Cadete>();
    }

    public void AgregarCadete(string nombre,string telefono,string direccion)
    {
        var cadete = new Cadete();
        cadete.Nombre = nombre;
        cadete.Telefono = telefono;
        cadete.Direccion = direccion;
        ListadoCadetes.Add(cadete);
    }

    public void RemoverCadete(int id)
    {
        var cadeteBorrable = ListadoCadetes.Find(cadete => cadete.Id == id);
        ListadoCadetes.Remove(cadeteBorrable);
    }

    }
}