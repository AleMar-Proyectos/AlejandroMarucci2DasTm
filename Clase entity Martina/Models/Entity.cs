using System;
using System.Collections.Generic;

namespace Clase_entity_Martina.Models;

public partial class Entity
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public DateTime Fechanacimiento { get; set; }
}
