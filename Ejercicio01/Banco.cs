using System;
using System.Collections.Generic;
using System.Linq;

public class Banco
{
    public string Nombre { get; } = "Banco Nacional";
    private RClientes repositorioClientes;
    private List<Cuenta> cuentas;

    public Banco()
    {
        repositorioClientes = new RClientes();
        cuentas = new List<Cuenta>();
    }

    // Gestión de Clientes - Delegada a RClientes
    public void AgregarCliente(Cliente cliente)
    {
        repositorioClientes.AgregarCliente(cliente);
    }

    public Cliente BuscarClientePorDNI(string dni)
    {
        return repositorioClientes.BuscarClientePorDNI(dni);
    }

    public List<Cliente> BuscarClientesPorNombre(string nombre)
    {
        return repositorioClientes.BuscarClientesPorNombre(nombre);
    }

    public List<Cliente> ObtenerTodosLosClientes()
    {
        return repositorioClientes.ObtenerTodosLosClientes();
    }

    public void ModificarCliente(string dni, string nuevoNombre = null, string nuevoTelefono = null, string nuevoEmail = null)
    {
        repositorioClientes.ModificarCliente(dni, nuevoNombre, nuevoTelefono, nuevoEmail);
    }

    public void EliminarCliente(string dni)
    {
        // Validar que no tenga cuentas asociadas antes de eliminar
        repositorioClientes.ValidarEliminacionCliente(dni, TieneCuentasAsociadas);
        repositorioClientes.EliminarCliente(dni);
    }

    // Método auxiliar para verificar si un cliente tiene cuentas
    private bool TieneCuentasAsociadas(string dni)
    {
        return cuentas.Any(c => c.Titular.DNI == dni);
    }

    // Gestión de Cuentas
    public void CrearCuentaAhorros(string numeroCuenta, string dniTitular)
    {
        if (cuentas.Any(c => c.NumeroCuenta == numeroCuenta))
            throw new DatosInvalidosException($"Ya existe una cuenta con número {numeroCuenta}.");

        var titular = repositorioClientes.BuscarClientePorDNI(dniTitular);
        var cuenta = new CuentaAhorros(numeroCuenta, titular);
        cuentas.Add(cuenta);
    }

    public void CrearCuentaCorriente(string numeroCuenta, string dniTitular, decimal limiteDescubierto = 5000m)
    {
        if (cuentas.Any(c => c.NumeroCuenta == numeroCuenta))
            throw new DatosInvalidosException($"Ya existe una cuenta con número {numeroCuenta}.");

        var titular = repositorioClientes.BuscarClientePorDNI(dniTitular);
        var cuenta = new CuentaCorriente(numeroCuenta, titular, limiteDescubierto);
        cuentas.Add(cuenta);
    }

    public Cuenta ObtenerCuenta(string numeroCuenta)
    {
        var cuenta = cuentas.FirstOrDefault(c => c.NumeroCuenta == numeroCuenta);
        if (cuenta == null)
            throw new DatosInvalidosException($"No se encontró la cuenta {numeroCuenta}.");
        return cuenta;
    }

    public List<Cuenta> ObtenerTodasLasCuentas()
    {
        return new List<Cuenta>(cuentas);
    }

    public List<Cuenta> ObtenerCuentasDeCliente(string dni)
    {
        return cuentas.Where(c => c.Titular.DNI == dni).ToList();
    }

    public void CambiarTitular(string numeroCuenta, string nuevoDniTitular)
    {
        var cuenta = ObtenerCuenta(numeroCuenta);
        var nuevoTitular = repositorioClientes.BuscarClientePorDNI(nuevoDniTitular);
        cuenta.Titular = nuevoTitular;
    }

    public void EliminarCuenta(string numeroCuenta)
    {
        var cuenta = ObtenerCuenta(numeroCuenta);
        if (cuenta.Saldo != 0)
            throw new DatosInvalidosException("No se puede eliminar una cuenta con saldo diferente de cero.");

        cuentas.Remove(cuenta);
    }

    // Operaciones bancarias
    public void RealizarDeposito(string numeroCuenta, decimal monto)
    {
        var cuenta = ObtenerCuenta(numeroCuenta);
        cuenta.Depositar(monto);
    }

    public void RealizarRetiro(string numeroCuenta, decimal monto)
    {
        var cuenta = ObtenerCuenta(numeroCuenta);
        cuenta.Retirar(monto);
    }

    public decimal ConsultarSaldo(string numeroCuenta)
    {
        var cuenta = ObtenerCuenta(numeroCuenta);
        return cuenta.Saldo;
    }

    // Métodos adicionales que aprovechan RClientes
    public int ObtenerCantidadClientes()
    {
        return repositorioClientes.ObtenerCantidadClientes();
    }

    public List<Cliente> BuscarClientesPorRangoEdad(int edadMinima, int edadMaxima)
    {
        return repositorioClientes.BuscarClientesPorRangoEdad(edadMinima, edadMaxima);
    }
}   