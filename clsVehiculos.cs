using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PryLedoEtapa6
{
    internal class clsVehiculos
    {
        public PictureBox pctAvion;
        public string tipoVehiculo;

        public void CrearAvion()
        {
            pctAvion = new PictureBox();
            pctAvion.SizeMode = PictureBoxSizeMode.StretchImage;
            pctAvion.Width = 100;
            pctAvion.Height = 100;
            pctAvion.BackColor = Color.Transparent;
            string rutaImagen = Path.Combine(Application.StartupPath, "..", "..", "Resources", "avion-removebg-preview.png");
            pctAvion.ImageLocation = rutaImagen;
            tipoVehiculo = "Avión";
        }
    }
}
