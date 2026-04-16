using System;

namespace SistemaParqueo
{
    // Esta clase guarda la información de cada vehículo que entra al parqueo
    public class Vehiculo
    {
        // Placa del vehículo
        public string Placa { get; set; }

        // Nombre del propietario
        public string Propietario { get; set; }

        // Hora en la que el vehículo ingresó
        public DateTime HoraEntrada { get; set; }

        // Posición del vehículo en el parqueo
        public int Fila { get; set; }
        public int Columna { get; set; }

        // Constructor para inicializar los datos del vehículo
        public Vehiculo(string placa, string propietario, DateTime horaEntrada, int fila, int columna)
        {
            Placa = placa;
            Propietario = propietario;
            HoraEntrada = horaEntrada;
            Fila = fila;
            Columna = columna;
        }
    }
}