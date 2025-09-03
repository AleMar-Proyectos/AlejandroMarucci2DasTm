using System;

public class CuentaCorriente : Cuenta
{
    public decimal LimiteDescubierto { get; set; }

    public CuentaCorriente(string numeroCuenta, Cliente titular, decimal limiteDescubierto = 5000m) 
        : base(numeroCuenta, titular)
    {
        LimiteDescubierto = limiteDescubierto;
    }

    public override void Retirar(decimal monto)
    {
        if (monto <= 0)
            throw new DatosInvalidosException("El monto debe ser mayor a cero.");

        if (Saldo - monto < -LimiteDescubierto)
            throw new FondosInsuficientesException($"No puede retirar más del límite de descubierto (${LimiteDescubierto:N2}).");

        Saldo -= monto;
    }

    public decimal ObtenerSaldoDisponible()
    {
        return Saldo + LimiteDescubierto;
    }

    public override string ObtenerTipoCuenta()
    {
        return "Cuenta Corriente";
    }
}