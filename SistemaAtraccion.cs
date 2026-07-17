#nullable disable

using System;
using System.Collections.Generic;

namespace ParqueDiversiones
{
    class Persona
    {
        public int Id;
        public string Nombre;
        public string HoraLlegada;
        public int Asiento;

        public Persona(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            HoraLlegada = DateTime.Now.ToString("HH:mm:ss");
            Asiento = 0;
        }
    }

    class Cola
    {
        private List<Persona> personas = new List<Persona>();
        private int capacidadMaxima;

        public Cola(int max)
        {
            capacidadMaxima = max;
        }

        public bool Encolar(Persona p)
        {
            if (personas.Count >= capacidadMaxima)
            {
                Console.WriteLine("No se puede agregar mas personas, la cola esta llena.");
                return false;
            }
            personas.Add(p);
            return true;
        }

        public Persona Desencolar()
        {
            if (personas.Count == 0)
            {
                Console.WriteLine("La cola esta vacia.");
                return null;
            }
            Persona primero = personas[0];
            personas.RemoveAt(0);
            return primero;
        }

        public int CantidadEnCola()
        {
            return personas.Count;
        }

        public bool EstaVacia()
        {
            return personas.Count == 0;
        }

        public bool EstaLlena()
        {
            return personas.Count >= capacidadMaxima;
        }

        public void MostrarCola()
        {
            if (personas.Count == 0)
            {
                Console.WriteLine("No hay personas en la cola.");
                return;
            }

            Console.WriteLine("\n--- Personas en la cola de espera ---");
            for (int i = 0; i < personas.Count; i++)
            {
                Console.WriteLine("Posicion " + (i + 1) + " | ID: " + personas[i].Id +
                                  " | Nombre: " + personas[i].Nombre +
                                  " | Llego a las: " + personas[i].HoraLlegada);
            }
            Console.WriteLine("Total en cola: " + personas.Count);
        }
    }

    class Program
    {
        static Cola colaEspera = new Cola(30);
        static List<Persona> historial = new List<Persona>();
        static int contadorId = 1;
        static int asientoActual = 0;
        static int totalAsientos = 30;

        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE ATENCION - ATRACCION DEL PARQUE ===");
            Console.WriteLine("Capacidad maxima: 30 personas\n");

            string opcion = "";

            while (opcion != "0")
            {
                MostrarMenu();
                opcion = Console.ReadLine();

                if (opcion == "1")
                {
                    RegistrarPersona();
                }
                else if (opcion == "2")
                {
                    AsignarAsiento();
                }
                else if (opcion == "3")
                {
                    colaEspera.MostrarCola();
                }
                else if (opcion == "4")
                {
                    MostrarHistorial();
                }
                else if (opcion == "5")
                {
                    MostrarEstado();
                }
                else if (opcion == "6")
                {
                    CargarDemostracion();
                }
                else if (opcion == "0")
                {
                    Console.WriteLine("Saliendo del sistema...");
                }
                else
                {
                    Console.WriteLine("Opcion no valida, intente de nuevo.");
                }

                Console.WriteLine("\nPresione ENTER para continuar...");
                Console.ReadLine();
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("   SISTEMA DE ATENCION - PARQUE");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Registrar nueva persona");
            Console.WriteLine("2. Asignar asiento al siguiente en cola");
            Console.WriteLine("3. Ver cola de espera");
            Console.WriteLine("4. Ver historial de abordaje");
            Console.WriteLine("5. Ver estado del sistema");
            Console.WriteLine("6. Cargar datos de prueba");
            Console.WriteLine("0. Salir");
            Console.Write("\nElige una opcion: ");
        }

        static void RegistrarPersona()
        {
            if (colaEspera.EstaLlena())
            {
                Console.WriteLine("Lo sentimos, ya no hay asientos disponibles.");
                return;
            }

            Console.Write("Ingrese el nombre de la persona: ");
            string nombre = Console.ReadLine();

            if (nombre == "" || nombre == null)
            {
                Console.WriteLine("El nombre no puede estar vacio.");
                return;
            }

            Persona nueva = new Persona(contadorId, nombre);
            contadorId++;

            bool resultado = colaEspera.Encolar(nueva);

            if (resultado)
            {
                Console.WriteLine("Persona registrada correctamente.");
                Console.WriteLine("Nombre: " + nombre);
                Console.WriteLine("Posicion en cola: " + colaEspera.CantidadEnCola());
            }
        }

        static void AsignarAsiento()
        {
            if (colaEspera.EstaVacia())
            {
                Console.WriteLine("No hay personas esperando.");
                return;
            }

            if (asientoActual >= totalAsientos)
            {
                Console.WriteLine("Todos los asientos ya fueron asignados.");
                return;
            }

            Persona persona = colaEspera.Desencolar();
            asientoActual++;
            persona.Asiento = asientoActual;

            historial.Add(persona);

            Console.WriteLine("Asiento asignado correctamente.");
            Console.WriteLine("Nombre: " + persona.Nombre);
            Console.WriteLine("Asiento numero: " + persona.Asiento);
        }

        static void MostrarHistorial()
        {
            if (historial.Count == 0)
            {
                Console.WriteLine("Todavia no ha abordado nadie.");
                return;
            }

            Console.WriteLine("\n--- Personas que ya abordaron la atraccion ---");
            foreach (Persona p in historial)
            {
                Console.WriteLine("Asiento #" + p.Asiento + " | " + p.Nombre +
                                  " | Llego a las: " + p.HoraLlegada);
            }
            Console.WriteLine("\nTotal abordados: " + historial.Count);
        }

        static void MostrarEstado()
        {
            Console.WriteLine("\n--- Estado actual del sistema ---");
            Console.WriteLine("Asientos totales:      " + totalAsientos);
            Console.WriteLine("Asientos ocupados:     " + asientoActual);
            Console.WriteLine("Asientos disponibles:  " + (totalAsientos - asientoActual));
            Console.WriteLine("Personas en cola:      " + colaEspera.CantidadEnCola());
        }

        static void CargarDemostracion()
        {
            string[] nombres = { "Ana Torres", "Luis Perez", "Maria Lopez",
                                 "Carlos Ruiz", "Sofia Castro", "Diego Mora",
                                 "Laura Vega", "Ivan Silva", "Carla Reyes", "Pedro Nunez" };

            Console.WriteLine("Cargando datos de demostracion...");

            foreach (string n in nombres)
            {
                if (!colaEspera.EstaLlena())
                {
                    Persona p = new Persona(contadorId, n);
                    contadorId++;
                    colaEspera.Encolar(p);
                }
            }

            Console.WriteLine("Se cargaron " + nombres.Length + " personas de prueba.");
        }
    }
}
