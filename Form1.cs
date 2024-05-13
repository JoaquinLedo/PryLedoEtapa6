using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PryLedoEtapa6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += Form1_KeyDown;

            // Configurar el temporizador
            TmrChoque.Interval = 100; // Intervalo de tiempo en milisegundos
            TmrChoque.Tick += TmrChoque_Tick;
        }

        List<clsVehiculos> listaVehiculos = new List<clsVehiculos>();
        private int contadorChoques = 0;
        private void TmrChoque_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            List<clsVehiculos> vehiculosParaEliminar = new List<clsVehiculos>(); // Lista para almacenar vehículos que se eliminarán

            foreach (clsVehiculos vehiculo in listaVehiculos)
            {
                int desplazamientoX = random.Next(-40, 41);
                int desplazamientoY = random.Next(-40, 41);

                int nuevaPosX = vehiculo.pctAvion.Location.X + desplazamientoX;
                int nuevaPosY = vehiculo.pctAvion.Location.Y + desplazamientoY;

                if (nuevaPosX < 0)
                    nuevaPosX = 0;
                else if (nuevaPosX > this.ClientSize.Width - vehiculo.pctAvion.Width)
                    nuevaPosX = this.ClientSize.Width - vehiculo.pctAvion.Width;

                if (nuevaPosY < 0)
                    nuevaPosY = 0;
                else if (nuevaPosY > this.ClientSize.Height - vehiculo.pctAvion.Height)
                    nuevaPosY = this.ClientSize.Height - vehiculo.pctAvion.Height;

                vehiculo.pctAvion.Location = new Point(nuevaPosX, nuevaPosY);

                foreach (clsVehiculos otroVehiculo in listaVehiculos)
                {
                    if (otroVehiculo != vehiculo && vehiculo.pctAvion.Bounds.IntersectsWith(otroVehiculo.pctAvion.Bounds))
                    {
                        vehiculosParaEliminar.Add(vehiculo); // Agregar vehículo a la lista de vehículos para eliminar
                        vehiculosParaEliminar.Add(otroVehiculo); // Agregar vehículo a la lista de vehículos para eliminar
                        break;
                    }
                }
            }

            // Eliminar los vehículos de la lista de vehículos después de terminar de iterar
            foreach (var vehiculoParaEliminar in vehiculosParaEliminar)
            {
                listaVehiculos.Remove(vehiculoParaEliminar);
                Controls.Remove(vehiculoParaEliminar.pctAvion);
            }

            // Incrementar el contador de choques y actualizar el Label
            contadorChoques += vehiculosParaEliminar.Count / 2; // Dividido por 2 porque cada colisión implica eliminar 2 vehículos
            lblChoques.Text = "Choques: " + contadorChoques;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                // Lógica para crear vehículos cuando se presiona la tecla 'A'
                Random random = new Random();

                for (int i = 0; i < 5; i++)
                {
                    clsVehiculos nuevoVehiculo = new clsVehiculos();
                    nuevoVehiculo.CrearAvion();

                    int posicionX;
                    int posicionY;
                    bool superpuesto;

                    do
                    {
                        posicionX = random.Next(0, this.ClientSize.Width - nuevoVehiculo.pctAvion.Width);
                        posicionY = random.Next(0, this.ClientSize.Height - nuevoVehiculo.pctAvion.Height);

                        superpuesto = false;

                        foreach (clsVehiculos vehiculoExistente in listaVehiculos)
                        {
                            if (Math.Abs(posicionX - vehiculoExistente.pctAvion.Location.X) < nuevoVehiculo.pctAvion.Width && Math.Abs(posicionY - vehiculoExistente.pctAvion.Location.Y) < nuevoVehiculo.pctAvion.Height)
                            {
                                superpuesto = true;
                                break;
                            }
                        }
                    }
                    while (superpuesto);

                    nuevoVehiculo.pctAvion.Location = new Point(posicionX, posicionY);
                    listaVehiculos.Add(nuevoVehiculo);

                    Controls.Add(nuevoVehiculo.pctAvion);
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                // Iniciar el temporizador cuando se presiona la tecla espacio
                TmrChoque.Start();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
          
        }
    }
}
