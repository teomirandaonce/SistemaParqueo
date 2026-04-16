using System;
using System.Windows.Forms;

namespace SistemaParqueo
{
    /// <summary>
    /// Punto de entrada de la aplicación
    /// INTEGRANTE: EVANEYH - Desarrollador UI (Frontend)
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
