using System;
using System.Globalization;

class Program
{
    private static Banco banco;

    static void Main(string[] args)
    {
        banco = new Banco();
        Console.WriteLine("=== SISTEMA BANCARIO ===");
        Console.WriteLine($"Bienvenido a {banco.Nombre}");
        Console.WriteLine();

        bool continuar = true;

        while (continuar)
        {
            try
            {
                MostrarMenu();
                int opcion = LeerOpcion();
                continuar = ProcesarOpcion(opcion);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    static void MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine("=== MENÚ PRINCIPAL ===");
        Console.WriteLine("--- GESTIÓN DE CLIENTES ---");
        Console.WriteLine("1. Registrar Nuevo Cliente");
        Console.WriteLine("2. Buscar Cliente por DNI");
        Console.WriteLine("3. Buscar Cliente por Nombre");
        Console.WriteLine("4. Modificar Datos de Cliente");
        Console.WriteLine("5. Eliminar Cliente");
        Console.WriteLine("6. Listar Todos los Clientes");
        Console.WriteLine();
        Console.WriteLine("--- GESTIÓN DE CUENTAS ---");
        Console.WriteLine("7. Crear Cuenta de Ahorros");
        Console.WriteLine("8. Crear Cuenta Corriente");
        Console.WriteLine("9. Cambiar Titular de Cuenta");
        Console.WriteLine("10. Eliminar Cuenta");
        Console.WriteLine("11. Listar Todas las Cuentas");
        Console.WriteLine("12. Listar Cuentas de un Cliente");
        Console.WriteLine();
        Console.WriteLine("--- OPERACIONES BANCARIAS ---");
        Console.WriteLine("13. Realizar Depósito");
        Console.WriteLine("14. Realizar Retiro");
        Console.WriteLine("15. Consultar Saldo");
        Console.WriteLine();
        Console.WriteLine("16. Salir");
        Console.WriteLine();
        Console.Write("Seleccione una opción: ");
    }

    static int LeerOpcion()
    {
        try
        {
            return int.Parse(Console.ReadLine()!);
        }
        catch
        {
            throw new DatosInvalidosException("Debe ingresar un número válido.");
        }
    }

    static bool ProcesarOpcion(int opcion)
    {
        try
        {
            switch (opcion)
            {
                case 1:
                    RegistrarNuevoCliente();
                    break;
                case 2:
                    BuscarClientePorDNI();
                    break;
                case 3:
                    BuscarClientePorNombre();
                    break;
                case 4:
                    ModificarCliente();
                    break;
                case 5:
                    EliminarCliente();
                    break;
                case 6:
                    ListarTodosLosClientes();
                    break;
                case 7:
                    CrearCuentaAhorros();
                    break;
                case 8:
                    CrearCuentaCorriente();
                    break;
                case 9:
                    CambiarTitularCuenta();
                    break;
                case 10:
                    EliminarCuenta();
                    break;
                case 11:
                    ListarTodasLasCuentas();
                    break;
                case 12:
                    ListarCuentasDeCliente();
                    break;
                case 13:
                    RealizarDeposito();
                    break;
                case 14:
                    RealizarRetiro();
                    break;
                case 15:
                    ConsultarSaldo();
                    break;
                case 16:
                    Console.WriteLine("¡Gracias por usar el sistema bancario!");
                    return false;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }
        catch (FondosInsuficientesException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (LimiteRetirosExcedidoException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (DatosInvalidosException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (ClienteNoEncontradoException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (DniDuplicadoException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }

        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
        return true;
    }

    // ========== FUNCIONES DE GESTIÓN DE CLIENTES ==========

    static void RegistrarNuevoCliente()
    {
        Console.WriteLine("\n=== REGISTRAR NUEVO CLIENTE ===");

        Console.Write("DNI: ");
        string dni = Console.ReadLine()!;

        Console.Write("Nombre y Apellido: ");
        string nombreCompleto = Console.ReadLine()!;

        Console.Write("Teléfono: ");
        string telefono = Console.ReadLine()!;

        Console.Write("Email: ");
        string email = Console.ReadLine()!;

        Console.Write("Fecha de Nacimiento (dd/mm/yyyy): ");
        DateTime fechaNacimiento = DateTime.ParseExact(Console.ReadLine()!, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        var cliente = new Cliente(dni, nombreCompleto, telefono, email, fechaNacimiento);
        banco.AgregarCliente(cliente);
        
        // ESTAS LÍNEAS FALTABAN:
        Console.WriteLine("Cliente registrado exitosamente.");
        Console.WriteLine($"Edad calculada: {cliente.CalcularEdad()} años");
    }

    static void BuscarClientePorDNI()
    {
        Console.WriteLine("\n=== BUSCAR CLIENTE POR DNI ===");
        Console.Write("Ingrese el DNI: ");
        string dni = Console.ReadLine()!;

        var cliente = banco.BuscarClientePorDNI(dni);
        MostrarDetalleCliente(cliente);
    }

    static void BuscarClientePorNombre()
    {
        Console.WriteLine("\n=== BUSCAR CLIENTE POR NOMBRE ===");
        Console.Write("Ingrese el nombre a buscar: ");
        string nombre = Console.ReadLine()!;

        var clientes = banco.BuscarClientesPorNombre(nombre);

        if (clientes.Count == 0)
        {
            Console.WriteLine("No se encontraron clientes con ese nombre.");
            return;
        }

        Console.WriteLine($"\nSe encontraron {clientes.Count} cliente(s):");
        foreach (var cliente in clientes)
        {
            Console.WriteLine($"- {cliente}");
        }
    }

    static void ModificarCliente()
    {
        Console.WriteLine("\n=== MODIFICAR CLIENTE ===");
        Console.Write("DNI del cliente a modificar: ");
        string dni = Console.ReadLine()!;

        // Verificar que el cliente existe
        var cliente = banco.BuscarClientePorDNI(dni);
        MostrarDetalleCliente(cliente);

        Console.WriteLine("\nIngrese los nuevos datos (deje en blanco para no modificar):");

        Console.Write($"Nombre y Apellido [{cliente.NombreCompleto}]: ");
        string nuevoNombre = Console.ReadLine()!;

        Console.Write($"Teléfono [{cliente.Telefono}]: ");
        string nuevoTelefono = Console.ReadLine()!;

        Console.Write($"Email [{cliente.Email}]: ");
        string nuevoEmail = Console.ReadLine()!;

        banco.ModificarCliente(dni,
            string.IsNullOrWhiteSpace(nuevoNombre) ? null : nuevoNombre,
            string.IsNullOrWhiteSpace(nuevoTelefono) ? null : nuevoTelefono,
            string.IsNullOrWhiteSpace(nuevoEmail) ? null : nuevoEmail);

        Console.WriteLine("Cliente modificado exitosamente.");
    }

    static void EliminarCliente()
    {
        Console.WriteLine("\n=== ELIMINAR CLIENTE ===");
        Console.Write("DNI del cliente a eliminar: ");
        string dni = Console.ReadLine()!;

        // Mostrar cliente antes de eliminar
        var cliente = banco.BuscarClientePorDNI(dni);
        MostrarDetalleCliente(cliente);

        // Mostrar cuentas asociadas
        var cuentas = banco.ObtenerCuentasDeCliente(dni);
        if (cuentas.Count > 0)
        {
            Console.WriteLine($"\nEste cliente tiene {cuentas.Count} cuenta(s) asociada(s):");
            foreach (var cuenta in cuentas)
            {
                Console.WriteLine($"- Cuenta: {cuenta.NumeroCuenta}, Saldo: ${cuenta.Saldo:N2}");
            }
            Console.WriteLine("No se puede eliminar un cliente con cuentas asociadas.");
            return;
        }

        Console.Write("\n¿Está seguro que desea eliminar este cliente? (s/N): ");
        string confirmacion = Console.ReadLine()!;

        if (confirmacion.ToLower() == "s")
        {
            banco.EliminarCliente(dni);
            Console.WriteLine("Cliente eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }
    }

    static void ListarTodosLosClientes()
    {
        Console.WriteLine("\n=== LISTADO DE TODOS LOS CLIENTES ===");
        var clientes = banco.ObtenerTodosLosClientes();

        if (clientes.Count == 0)
        {
            Console.WriteLine("No hay clientes registrados.");
            return;
        }

        Console.WriteLine($"Total de clientes: {clientes.Count}");
        Console.WriteLine(new string('=', 50));

        foreach (var cliente in clientes)
        {
            MostrarDetalleCliente(cliente);
            Console.WriteLine(new string('-', 30));
        }
    }

    static void MostrarDetalleCliente(Cliente cliente)
    {
        Console.WriteLine($"DNI: {cliente.DNI}");
        Console.WriteLine($"Nombre: {cliente.NombreCompleto}");
        Console.WriteLine($"Teléfono: {cliente.Telefono}");
        Console.WriteLine($"Email: {cliente.Email}");
        Console.WriteLine($"Fecha de Nacimiento: {cliente.FechaNacimiento:dd/MM/yyyy}");
        Console.WriteLine($"Edad: {cliente.CalcularEdad()} años");

        // Mostrar cuentas del cliente
        var cuentas = banco.ObtenerCuentasDeCliente(cliente.DNI);
        if (cuentas.Count > 0)
        {
            Console.WriteLine($"Cuentas ({cuentas.Count}):");
            foreach (var cuenta in cuentas)
            {
                Console.WriteLine($"  - {cuenta.NumeroCuenta} ({cuenta.ObtenerTipoCuenta()}): ${cuenta.Saldo:N2}");
            }
        }
    }

    // ========== FUNCIONES DE GESTIÓN DE CUENTAS ==========

    static void CrearCuentaAhorros()
    {
        Console.WriteLine("\n=== CREAR CUENTA DE AHORROS ===");

        Console.Write("Número de cuenta: ");
        string numeroCuenta = Console.ReadLine()!;

        Console.Write("DNI del titular: ");
        string dniTitular = Console.ReadLine()!;

        // Verificar que el cliente existe
        var cliente = banco.BuscarClientePorDNI(dniTitular);
        Console.WriteLine($"Titular: {cliente.NombreCompleto}");

        banco.CrearCuentaAhorros(numeroCuenta, dniTitular);
        Console.WriteLine("Cuenta de ahorros creada exitosamente.");
    }

    static void CrearCuentaCorriente()
    {
        Console.WriteLine("\n=== CREAR CUENTA CORRIENTE ===");

        Console.Write("Número de cuenta: ");
        string numeroCuenta = Console.ReadLine()!;

        Console.Write("DNI del titular: ");
        string dniTitular = Console.ReadLine()!;

        // Verificar que el cliente existe
        var cliente = banco.BuscarClientePorDNI(dniTitular);
        Console.WriteLine($"Titular: {cliente.NombreCompleto}");

        Console.Write("Límite de descubierto (Enter para $5,000): $");
        string limiteStr = Console.ReadLine()!
    ;
        decimal limite = string.IsNullOrWhiteSpace(limiteStr) ? 5000m : decimal.Parse(limiteStr, CultureInfo.InvariantCulture);

        banco.CrearCuentaCorriente(numeroCuenta, dniTitular, limite);
        Console.WriteLine($"Cuenta corriente creada exitosamente con límite de descubierto de ${limite:N2}.");
    }

    static void CambiarTitularCuenta()
    {
        Console.WriteLine("\n=== CAMBIAR TITULAR DE CUENTA ===");

        Console.Write("Número de cuenta: ");
        string numeroCuenta = Console.ReadLine()!;

        var cuenta = banco.ObtenerCuenta(numeroCuenta);
        Console.WriteLine($"Titular actual: {cuenta.Titular.NombreCompleto} (DNI: {cuenta.Titular.DNI})");

        Console.Write("DNI del nuevo titular: ");
        string nuevoDniTitular = Console.ReadLine()!;

        var nuevoTitular = banco.BuscarClientePorDNI(nuevoDniTitular);
        Console.WriteLine($"Nuevo titular: {nuevoTitular.NombreCompleto}");

        Console.Write("¿Confirma el cambio de titular? (s/N): ");
        string confirmacion = Console.ReadLine()!;

        if (confirmacion.ToLower() == "s")
        {
            banco.CambiarTitular(numeroCuenta, nuevoDniTitular);
            Console.WriteLine("Titular cambiado exitosamente.");
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }
    }

    static void EliminarCuenta()
    {
        Console.WriteLine("\n=== ELIMINAR CUENTA ===");

        Console.Write("Número de cuenta: ");
        string numeroCuenta = Console.ReadLine()!;

        var cuenta = banco.ObtenerCuenta(numeroCuenta);
        Console.WriteLine($"Cuenta: {cuenta.NumeroCuenta}");
        Console.WriteLine($"Titular: {cuenta.Titular.NombreCompleto}");
        Console.WriteLine($"Tipo: {cuenta.ObtenerTipoCuenta()}");
        Console.WriteLine($"Saldo: ${cuenta.Saldo:N2}");

        if (cuenta.Saldo != 0)
        {
            Console.WriteLine("No se puede eliminar una cuenta con saldo diferente de cero.");
            return;
        }

        Console.Write("¿Está seguro que desea eliminar esta cuenta? (s/N): ");
        string confirmacion = Console.ReadLine()!;

        if (confirmacion.ToLower() == "s")
        {
            banco.EliminarCuenta(numeroCuenta);
            Console.WriteLine("Cuenta eliminada exitosamente.");
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }
    }

    static void ListarTodasLasCuentas()
    {
        Console.WriteLine("\n=== LISTADO DE TODAS LAS CUENTAS ===");
        var cuentas = banco.ObtenerTodasLasCuentas();

        if (cuentas.Count == 0)
        {
            Console.WriteLine("No hay cuentas registradas.");
            return;
        }

        Console.WriteLine($"Total de cuentas: {cuentas.Count}");
        Console.WriteLine(new string('=', 60));

        foreach (var cuenta in cuentas)
        {
            MostrarDetalleCuenta(cuenta);
            Console.WriteLine(new string('-', 40));
        }
    }

    static void ListarCuentasDeCliente()
    {
        Console.WriteLine("\n=== LISTAR CUENTAS DE UN CLIENTE ===");

        Console.Write("DNI del cliente: ");
        string dni = Console.ReadLine()!;

        var cliente = banco.BuscarClientePorDNI(dni);
        var cuentas = banco.ObtenerCuentasDeCliente(dni);

        Console.WriteLine($"\nCliente: {cliente.NombreCompleto}");
        Console.WriteLine($"DNI: {cliente.DNI}");

        if (cuentas.Count == 0)
        {
            Console.WriteLine("Este cliente no tiene cuentas asociadas.");
            return;
        }

        Console.WriteLine($"Cuentas ({cuentas.Count}):");
        Console.WriteLine(new string('=', 50));

        foreach (var cuenta in cuentas)
        {
            MostrarDetalleCuenta(cuenta, false); // false para no mostrar datos del titular
            Console.WriteLine(new string('-', 30));
        }
    }

    static void MostrarDetalleCuenta(Cuenta cuenta, bool mostrarTitular = true)
    {
        Console.WriteLine($"Cuenta: {cuenta.NumeroCuenta}");
        if (mostrarTitular)
        {
            Console.WriteLine($"Titular: {cuenta.Titular.NombreCompleto} (DNI: {cuenta.Titular.DNI})");
        }
        Console.WriteLine($"Tipo: {cuenta.ObtenerTipoCuenta()}");
        Console.WriteLine($"Saldo: ${cuenta.Saldo:N2}");

        if (cuenta is CuentaAhorros ca)
        {
            Console.WriteLine($"Retiros realizados este mes: {ca.RetirosRealizados}/3");
            Console.WriteLine($"Monto máximo por retiro: ${ca.MontoMaximoPorRetiro:N2}");
        }
        else if (cuenta is CuentaCorriente cc)
        {
            Console.WriteLine($"Límite de descubierto: ${cc.LimiteDescubierto:N2}");
            Console.WriteLine($"Saldo disponible: ${cc.ObtenerSaldoDisponible():N2}");
        }
    }

    // ========== FUNCIONES DE OPERACIONES BANCARIAS ==========

    static void RealizarDeposito()
    {
        Console.WriteLine("\n=== REALIZAR DEPÓSITO ===");
        Console.Write("Número de cuenta: ");
        string numeroCuenta = Console.ReadLine()!;

        var cuenta = banco.ObtenerCuenta(numeroCuenta);
        Console.WriteLine($"Cuenta: {cuenta.NumeroCuenta} - {cuenta.Titular.NombreCompleto}");
        Console.WriteLine($"Saldo actual: ${cuenta.Saldo:N2}");

        Console.Write("Monto a depositar: $");
        decimal monto = decimal.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);

        banco.RealizarDeposito(numeroCuenta, monto);
        Console.WriteLine($"Depósito de ${monto:N2} realizado exitosamente.");
        Console.WriteLine($"Nuevo saldo: ${banco.ConsultarSaldo(numeroCuenta):N2}");
    }

    static void RealizarRetiro()
    {
        Console.WriteLine("\n=== REALIZAR RETIRO ===");
        Console.Write("Número de cuenta: ");
        string numeroCuenta = Console.ReadLine()!;

        var cuenta = banco.ObtenerCuenta(numeroCuenta);
        Console.WriteLine($"Cuenta: {cuenta.NumeroCuenta} - {cuenta.Titular.NombreCompleto}");
        Console.WriteLine($"Saldo actual: ${cuenta.Saldo:N2}");

        if (cuenta is CuentaCorriente cc)
        {
            Console.WriteLine($"Saldo disponible (con descubierto): ${cc.ObtenerSaldoDisponible():N2}");
        }
        else if (cuenta is CuentaAhorros ca)
        {
            Console.WriteLine($"Retiros realizados este mes: {ca.RetirosRealizados}/3");
            Console.WriteLine($"Monto máximo por retiro: ${ca.MontoMaximoPorRetiro:N2}");
        }

        Console.Write("Monto a retirar: $");
        decimal monto = decimal.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);

        banco.RealizarRetiro(numeroCuenta, monto);
        Console.WriteLine($"Retiro de ${monto:N2} realizado exitosamente.");
        Console.WriteLine($"Nuevo saldo: ${banco.ConsultarSaldo(numeroCuenta):N2}");
    }

    static void ConsultarSaldo()
    {
        Console.WriteLine("\n=== CONSULTAR SALDO ===");
        Console.Write("Número de cuenta: ");
        string numeroCuenta = Console.ReadLine()!;

        var cuenta = banco.ObtenerCuenta(numeroCuenta);

        Console.WriteLine("\n" + new string('=', 40));
        MostrarDetalleCuenta(cuenta);
        Console.WriteLine(new string('=', 40));
    }
}