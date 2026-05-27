#nullable disable
using System;
using System.Windows.Forms;

namespace Sistema
{
    public partial class FormConfiguracion : Form
    {
        public FormConfiguracion()
        {
            InitializeComponent();
        }

        private void FormConfiguracion_Load(object sender, EventArgs e)
        {
            txtTarifaCarro.Text = GestionConfig.ObtenerConfig("tarifa_carro", "2.00");
            txtTarifaMoto.Text = GestionConfig.ObtenerConfig("tarifa_moto", "1.00");

            textBox1.Text = GestionConfig.ObtenerConfig("total_espacios_seccion1", "10");
            textBox2.Text = GestionConfig.ObtenerConfig("total_espacios_seccion2", "10");
        }

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

        private void btnResetParqueo_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¡ATVERTENCIA CRÍTICA!\n\nEsta acción eliminará todos los vehículos que se encuentren actualmente parqueados y creará los nuevos espacios desde cero.\n\n¿Estás seguro de continuar?",
                "Confirmación de Reseteo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (respuesta != DialogResult.Yes) return;

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

            if (GestionConfig.ResetearParqueoCompleto(cant1, cant2))
            {
                MessageBox.Show("El parqueo ha sido regenerado por completo.", "Reseteo Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}