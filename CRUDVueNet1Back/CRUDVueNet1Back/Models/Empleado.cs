using System;
using System.Collections.Generic;

namespace CRUDVueNet1Back.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string NombreEmpleado { get; set; } = null!;

    public int IdDepartamento { get; set; }

    public bool? Activo { get; set; }

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
