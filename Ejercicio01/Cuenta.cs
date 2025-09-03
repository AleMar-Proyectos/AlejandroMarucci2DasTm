using System;

public abstract class Cuenta
{
    public string NumeroCuenta { get; protected set; }
    public Cliente Titular { get; set; }
    public decimal Saldo { get; protected set; }

    protected Cuenta(string numeroCuenta, Cliente titular)
    {
        if (string.IsNullOrWhiteSpace(numeroCuenta))
            throw new DatosInvalidosException("El número de cuenta es obligatorio.");
        
        NumeroCuenta = numeroCuenta;
        Titular = titular ?? throw new DatosInvalidosException("El titular es obligatorio.");
        Saldo = 0;
    }

    public virtual void Depositar(decimal monto)
    {
        if (monto <= 0)
            throw new DatosInvalidosException("El monto debe ser mayor a cero.");
        
        Saldo += monto;
    }

    public abstract void Retirar(decimal monto);
    public abstract string ObtenerTipoCuenta();
}