using System;
using System.Collections.Generic;
using System.Linq;

public class RClientes
{
    private List<Cliente> clientes;

    public RClientes()
    {
        clientes = new List<Cliente>();
    }

    /// <summary>
    /// Agrega un nuevo cliente al repositorio
    /// </summary>
    public void AgregarCliente(Cliente cliente)
    {
        if (cliente == null)
            throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");

        if (ExisteDNI(cliente.DNI))
            throw new DniDuplicadoException($"Ya existe un cliente con DNI {cliente.DNI}.");

        clientes.Add(cliente);
    }

    /// <summary>
    /// Busca un cliente por su DNI
    /// </summary>
    public Cliente BuscarClientePorDNI(string dni)
    {
        if (string.IsNullOrWhiteSpace(dni))
            throw new ArgumentException("El DNI no puede estar vacío.", nameof(dni));

        var cliente = clientes.FirstOrDefault(c => c.DNI == dni);
        if (cliente == null)
            throw new ClienteNoEncontradoException($"No se encontró un cliente con DNI {dni}.");
        
        return cliente;
    }

    /// <summary>
    /// Busca clientes por nombre (búsqueda parcial)
    /// </summary>
    public List<Cliente> BuscarClientesPorNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return new List<Cliente>();

        return clientes.Where(c => c.NombreCompleto.ToLower().Contains(nombre.ToLower())).ToList();
    }

    /// <summary>
    /// Obtiene todos los clientes registrados
    /// </summary>
    public List<Cliente> ObtenerTodosLosClientes()
    {
        return new List<Cliente>(clientes);
    }

    /// <summary>
    /// Modifica los datos de un cliente existente
    /// </summary>
    public void ModificarCliente(string dni, string nuevoNombre = null, string nuevoTelefono = null, string nuevoEmail = null)
    {
        var cliente = BuscarClientePorDNI(dni);
        
        if (!string.IsNullOrWhiteSpace(nuevoNombre))
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre.Trim()))
                throw new DatosInvalidosException("El nombre no puede estar vacío.");
            cliente.NombreCompleto = nuevoNombre.Trim();
        }
        
        if (!string.IsNullOrWhiteSpace(nuevoTelefono))
        {
            if (string.IsNullOrWhiteSpace(nuevoTelefono.Trim()))
                throw new DatosInvalidosException("El teléfono no puede estar vacío.");
            cliente.Telefono = nuevoTelefono.Trim();
        }
        
        if (!string.IsNullOrWhiteSpace(nuevoEmail))
        {
            if (string.IsNullOrWhiteSpace(nuevoEmail.Trim()))
                throw new DatosInvalidosException("El email no puede estar vacío.");
            cliente.Email = nuevoEmail.Trim();
        }
    }

    /// <summary>
    /// Elimina un cliente del repositorio
    /// </summary>
    public void EliminarCliente(string dni)
    {
        var cliente = BuscarClientePorDNI(dni);
        clientes.Remove(cliente);
    }

    /// <summary>
    /// Verifica si existe un cliente con el DNI especificado
    /// </summary>
    public bool ExisteDNI(string dni)
    {
        if (string.IsNullOrWhiteSpace(dni))
            return false;

        return clientes.Any(c => c.DNI == dni);
    }

    /// <summary>
    /// Obtiene la cantidad total de clientes registrados
    /// </summary>
    public int ObtenerCantidadClientes()
    {
        return clientes.Count;
    }

    /// <summary>
    /// Busca clientes por rango de edad
    /// </summary>
    public List<Cliente> BuscarClientesPorRangoEdad(int edadMinima, int edadMaxima)
    {
        return clientes.Where(c => 
        {
            var edad = c.CalcularEdad();
            return edad >= edadMinima && edad <= edadMaxima;
        }).ToList();
    }

    /// <summary>
    /// Valida que un cliente pueda ser eliminado (sin cuentas asociadas)
    /// </summary>
    public void ValidarEliminacionCliente(string dni, Func<string, bool> tieneCuentasAsociadas)
    {
        var cliente = BuscarClientePorDNI(dni);
        
        if (tieneCuentasAsociadas(dni))
            throw new DatosInvalidosException("No se puede eliminar un cliente que tiene cuentas asociadas.");
    }
}