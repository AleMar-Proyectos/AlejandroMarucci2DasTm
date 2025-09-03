using System;

public class DniDuplicadoException : Exception
{
    public DniDuplicadoException(string mensaje) : base(mensaje) { }
}