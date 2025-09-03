using System;

public class ClienteNoEncontradoException : Exception
{
    public ClienteNoEncontradoException(string mensaje) : base(mensaje) { }
}