using System;

public class Cliente
{
    public string DNI { get; set; }
    public string NombreCompleto { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public DateTime FechaNacimiento { get; set; }

    public Cliente(string dni, string nombreCompleto, string telefono, string email, DateTime fechaNacimiento)
    {
        DNI = dni;
        NombreCompleto = nombreCompleto;
        Telefono = telefono;
        Email = email;
        FechaNacimiento = fechaNacimiento;
    }

    public int CalcularEdad()
    {
        var hoy = DateTime.Today;
        var edad = hoy.Year - FechaNacimiento.Year;
        if (FechaNacimiento.Date > hoy.AddYears(-edad))
            edad--;
        return edad;
    }

    public override string ToString()
    {
        return $"DNI: {DNI}, Nombre: {NombreCompleto}, Edad: {CalcularEdad()} años";
    }
}