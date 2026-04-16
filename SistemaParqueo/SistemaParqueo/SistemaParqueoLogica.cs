using System;
using System.Collections.Generic;

namespace SistemaParqueo
{
    // Esta clase maneja la lógica principal del sistema de parqueo
    public class SistemaParqueoLogica
    {
        // Matriz para representar los espacios del parqueo
        private string[,] espacios;

        // Diccionario para guardar los vehículos activos usando la placa como clave
        private Dictionary<string, Vehiculo> vehiculosActivos;

        // Cola para vehículos en espera cuando el parqueo está lleno
        private Queue<Vehiculo> colaEspera;

        // Precio por hora de parqueo
        private double tarifaPorHora;

        public SistemaParqueoLogica(int filas, int columnas, double tarifa)
        {
            espacios = new string[filas, columnas];
            vehiculosActivos = new Dictionary<string, Vehiculo>();
            colaEspera = new Queue<Vehiculo>();
            tarifaPorHora = tarifa;

            InicializarEspacios();
        }

        // Llena la matriz con "Libre" al iniciar el sistema
        private void InicializarEspacios()
        {
            for (int i = 0; i < espacios.GetLength(0); i++)
            {
                for (int j = 0; j < espacios.GetLength(1); j++)
                {
                    espacios[i, j] = "Libre";
                }
            }
        }

        // Busca el primer espacio libre en la matriz
        public (int fila, int columna) BuscarEspacioLibre()
        {
            for (int i = 0; i < espacios.GetLength(0); i++)
            {
                for (int j = 0; j < espacios.GetLength(1); j++)
                {
                    if (espacios[i, j] == "Libre")
                    {
                        return (i, j);
                    }
                }
            }

            // Si no encuentra espacio disponible
            return (-1, -1);
        }

        // Registra un vehículo al entrar al parqueo
        public string IngresarVehiculo(string placa, string propietario)
        {
            // Verifica si el vehículo ya está dentro del parqueo
            if (vehiculosActivos.ContainsKey(placa))
            {
                return "El vehículo ya está registrado en el parqueo.";
            }

            var espacioLibre = BuscarEspacioLibre();

            // Si no hay espacio libre, se agrega a la cola de espera
            if (espacioLibre.fila == -1 || espacioLibre.columna == -1)
            {
                Vehiculo vehiculoEnEspera = new Vehiculo(placa, propietario, DateTime.Now, -1, -1);
                colaEspera.Enqueue(vehiculoEnEspera);
                return "Parqueo lleno. Vehículo agregado a la cola de espera.";
            }

            // Crear el vehículo y asignarlo al espacio encontrado
            Vehiculo nuevoVehiculo = new Vehiculo(placa, propietario, DateTime.Now, espacioLibre.fila, espacioLibre.columna);

            espacios[espacioLibre.fila, espacioLibre.columna] = "Ocupado";
            vehiculosActivos.Add(placa, nuevoVehiculo);

            return $"Vehículo ingresado correctamente en el espacio [{espacioLibre.fila}, {espacioLibre.columna}]";
        }

        // Calcula el cobro según el tiempo que estuvo el vehículo en el parqueo
        public double CalcularCobro(DateTime horaEntrada)
        {
            TimeSpan tiempoTranscurrido = DateTime.Now - horaEntrada;

            // Se calcula el total de horas, redondeando hacia arriba
            double horas = Math.Ceiling(tiempoTranscurrido.TotalHours);

            // Si estuvo menos de una hora, igual se cobra una hora mínima
            if (horas < 1)
            {
                horas = 1;
            }

            return horas * tarifaPorHora;
        }

        // Retira un vehículo del parqueo y calcula el monto a pagar
        public string RetirarVehiculo(string placa)
        {
            // Verifica si el vehículo existe en el parqueo
            if (!vehiculosActivos.ContainsKey(placa))
            {
                return "El vehículo no se encuentra registrado en el parqueo.";
            }

            Vehiculo vehiculo = vehiculosActivos[placa];

            // Calcular el monto a pagar
            double totalPagar = CalcularCobro(vehiculo.HoraEntrada);

            // Liberar el espacio en la matriz
            espacios[vehiculo.Fila, vehiculo.Columna] = "Libre";

            // Eliminar el vehículo del diccionario
            vehiculosActivos.Remove(placa);

            return $"Vehículo retirado correctamente. Total a pagar: ${totalPagar}";
        }
    }
}