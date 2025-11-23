using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Modelos
{
  public class Evento
  {
      public int Id { get; set; }
      public string? Titulo { get; set; }
      public string? Descripcion { get; set; }
      public DateTime FechaInicio { get; set; }
      public DateTime FechaFin { get; set; }
      public List<string> Etiquetas { get; set; } = new List<string>();
  }
    
}

