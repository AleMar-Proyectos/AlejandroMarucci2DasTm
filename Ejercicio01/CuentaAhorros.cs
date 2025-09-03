using System;

public class CuentaAhorros : Cuenta
{
    public int RetirosRealizados { get; private set; }
    public decimal MontoMaximoPorRetiro { get; set; } = 10000m;
    private const int MaxRetirosPorMes = 3;

    public CuentaAhorros(string numeroCuenta, Cliente titular) : base(numeroCuenta, titular)
    {
        RetirosRealizados = 0;
    }

    public override void Retirar(decimal monto)
    {
        if (monto <= 0)
            throw new DatosInvalidosException("El monto debe ser mayor a cero.");

        if (monto > Saldo)
            throw new FondosInsuficientesException("Saldo insuficiente para realizar el retiro.");

        if (monto > MontoMaximoPorRetiro)
            throw new DatosInvalidosException($"El monto máximo por retiro es ${MontoMaximoPorRetiro:N2}.");

        if (RetirosRealizados >= MaxRetirosPorMes)
            throw new LimiteRetirosExcedidoException($"Ha excedido el límite de {MaxRetirosPorMes} retiros por mes.");

        Saldo -= monto;
        RetirosRealizados++;
    }

    public void ReiniciarContadorRetiros()
    {
        RetirosRealizados = 0;
    }

    public override string ObtenerTipoCuenta()
    {
        return "Cuenta de Ahorros";
    }
}