#nullable disable
using System;
using System.Windows.Forms;

namespace Sistema
{
    // REGLA DE ORO: Debe tener ": Form" para heredar de Windows Forms
    public partial class FormConfiguracion : Form
    {
        public FormConfiguracion()
        {
            InitializeComponent();
        }

        // Al abrir la ventana, cargamos lo que está actualmente en la BD
        private void FormConfiguracion_Load(object sender, EventArgs e)
        {
            txtTarifaCarro.Text = GestionConfig.ObtenerConfig("tarifa_carro", "2.00");
            txtTarifaMoto.Text = GestionConfig.ObtenerConfig("tarifa_moto", "1.00");

            // Usamos los nombres por defecto de tus TextBox de cantidad
            textBox1.Text = GestionConfig.ObtenerConfig("total_espacios_seccion1", "10");
            textBox2.Text = GestionConfig.ObtenerConfig("total_espacios_seccion2", "10");
        }

        // BOTÓN: APLICAR CAMBIOS DE TARIFA (Sin resetear el mapa)
        private void btnAplicarTarifas_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTarifaCarro.Text) || string.IsNullOrWhiteSpace(txtTarifaMoto.Text))
            {
                MessageBox.Show("Por favor, ingresa valores válidos para las tarifas.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GestionConfig.GuardarConfig("tarifa_carro", txtTarifaCarro.Text.Trim());
            GestionConfig.GuardarConfig("tarifa_moto", txtTarifaMoto.Text.Trim());

            MessageBox.Show("Tarifas actualizadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // BOTÓN: RESETEAR TODO EL PARQUEO (Cambia capacidades y vacía todo)
        private void btnResetParqueo_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¡ATVERTENCIA CRÍTICA!\n\nEsta acción eliminará todos los vehículos que se encuentren actualmente parqueados y creará los nuevos espacios desde cero.\n\n¿Estás seguro de continuar?",
                "Confirmación de Reseteo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (respuesta != DialogResult.Yes) return;

            // Validamos que los campos de texto tengan números enteros válidos
            if (!int.TryParse(textBox1.Text, out int cant1) || !int.TryParse(textBox2.Text, out int cant2))
            {
                MessageBox.Show("Por favor, ingresa números válidos para las cantidades.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cant1 <= 0 || cant2 <= 0)
            {
                MessageBox.Show("La cantidad de espacios debe ser mayor a 0.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ejecutamos el reseteo masivo en la BD
            if (GestionConfig.ResetearParqueoCompleto(cant1, cant2))
            {
                MessageBox.Show("El parqueo ha sido regenerado por completo.", "Reseteo Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Indica a la ventana principal que debe redibujar el mapa
                this.Close();
            }
        }
    }
}