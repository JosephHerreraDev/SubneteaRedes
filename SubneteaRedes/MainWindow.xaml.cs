using System;
using System.Windows;

namespace SubneteaRedes
{ 
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			#region Inicio automatico de componentes y ocultar Ver redes
			InitializeComponent();
			btnVerRedes.Visibility = Visibility.Collapsed;
			#endregion
		}
		#region Eventos Click
		/// <summary>
		/// Hace las acciones de los botones
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSubnetear_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ClasePrincipal.PrimerOcteto = Convert.ToInt32(txbPrimerOcteto.Text);
				ClasePrincipal.SegundoOcteto = Convert.ToInt32(txbSegundoOcteto.Text);
				ClasePrincipal.TercerOcteto = Convert.ToInt32(txbTercerOcteto.Text);
				ClasePrincipal.CuartoOcteto = Convert.ToInt32(txbCuartoOcteto.Text);
				ClasePrincipal.Sufijo = Convert.ToInt32(txbSufijo.Text);

				
			}
			catch(Exception E)
			{
				MessageBox.Show(E.Message);
				this.Close();
			}
			RevisarClase(ClasePrincipal.PrimerOcteto);
			btnVerRedes.Visibility = Visibility.Visible;
			
		}

		private void BtnVerRedes_Click(object sender, RoutedEventArgs e)
		{
			Redes win2 = new Redes();
			win2.Show();
			this.Close();
		}
		#endregion
		
		#region Revisar octetos

		/// <summary>
		/// Revisa que solo hayan numeros
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxbPrimerOcteto_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(txbPrimerOcteto.Text, "[^0-9]"))
			{
				MessageBox.Show("Por favor ingrese solo numeros.");
				txbPrimerOcteto.Text = txbPrimerOcteto.Text.Remove(txbPrimerOcteto.Text.Length - 1);
			}
		}

		private void TxbSegundoOcteto_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(txbSegundoOcteto.Text, "[^0-9]"))
			{
				MessageBox.Show("Por favor ingrese solo numeros.");
				txbSegundoOcteto.Text = txbSegundoOcteto.Text.Remove(txbSegundoOcteto.Text.Length - 1);
			}
		}
		private void TxbTercerOcteto_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(txbTercerOcteto.Text, "[^0-9]"))
			{
				MessageBox.Show("Por favor ingrese solo numeros.");
				txbTercerOcteto.Text = txbTercerOcteto.Text.Remove(txbPrimerOcteto.Text.Length - 1);
			}
		}
		private void TxbCuartoOcteto_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(txbCuartoOcteto.Text, "[^0-9]"))
			{
				MessageBox.Show("Por favor ingrese solo numeros.");
				txbCuartoOcteto.Text = txbCuartoOcteto.Text.Remove(txbCuartoOcteto.Text.Length - 1);
			}
		}
		private void TxbSufijo_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(txbSufijo.Text, "[^0-9]"))
			{
				MessageBox.Show("Por favor ingrese solo numeros.");
				txbSufijo.Text = txbSufijo.Text.Remove(txbSufijo.Text.Length - 1);
			}
		}
		#endregion

		#region Revisa el tipo de clase

		/// <summary>
		/// Hace una revision de la clase que se usa
		/// </summary>
		/// <param name="Octeto"></param>
		private void RevisarClase(int Octeto)
		{
			String Clase = "";
			int Sufijo = ClasePrincipal.Sufijo;
			if (Octeto <= 127)
			{
				Clase = "A";
			}
			else if(Octeto <= 191)
			{
				Clase = "B";
			}
			else if(Octeto <= 223)
			{
				Clase = "C";
			}
			else
			{
				MessageBox.Show("El valor del primer octeto no es valido, solo se realizan clases A, B y C");
			}
			
			AsignarMascara(Clase,Sufijo);
		}
		#endregion

		#region Asigna la mascara y Wildcard
		private void AsignarMascara(String Clase, int Sufijo)
		{
			switch (Clase)
			{
				case "A":
					var Donde = "2";
					txbMascara1.Text = "255";
					txbMascara3.Text = "0";
					txbMascara4.Text = "0";
					Sufijo = Sufijo - 8;
					ClasePrincipal.Sufijo = Sufijo;
					ClasePrincipal.Clase = "A";
					txbWildcard1.Text = "0";
					txbWildcard3.Text = "255";
					txbWildcard4.Text = "255";
					CalcularSiguiente(Sufijo,Donde);
					break;
				case "B":
					var Donde2 = "3";
					txbMascara1.Text = "255";
					txbMascara2.Text = "255";
					txbMascara4.Text = "0";
					Sufijo = Sufijo - 16;
					ClasePrincipal.Sufijo = Sufijo;
					ClasePrincipal.Clase = "B";
					txbWildcard1.Text = "0";
					txbWildcard2.Text = "0";
					txbWildcard4.Text = "255";
					CalcularSiguiente(Sufijo,Donde2);
					break;
				case "C":
					var Donde3 = "4";
					txbMascara1.Text = "255";
					txbMascara2.Text = "255";
					txbMascara3.Text = "255";
					Sufijo = Sufijo - 24;
					ClasePrincipal.Sufijo = Sufijo;
					ClasePrincipal.Clase = "C";
					txbWildcard1.Text = "0";
					txbWildcard2.Text = "0";
					txbWildcard3.Text = "0";
					CalcularSiguiente(Sufijo,Donde3);
					break;
				default:
					MessageBox.Show("No reconozco ese tipo de red.");
					break;
			}
			
		}
		#endregion

		#region Calcula el siguiente octeto basado en la clase
		private void CalcularSiguiente(int Sufijo, string Donde)
		{
			double OctetoMascara = 2;
			double OctetoWildcard;

			if (Sufijo == 1)
			{
				OctetoMascara = Math.Pow(OctetoMascara, 7.0);
				OctetoWildcard = 255 - OctetoMascara;
			}
			else if (Sufijo == 2)
			{
				OctetoMascara = Math.Pow(OctetoMascara, 7.0) + Math.Pow(OctetoMascara, 6.0);
			}
			else if (Sufijo == 3)
			{
				OctetoMascara = Math.Pow(OctetoMascara, 7.0) + Math.Pow(OctetoMascara, 6.0) + Math.Pow(OctetoMascara, 5.0);
			}
			else if (Sufijo == 4)
			{
				OctetoMascara = Math.Pow(OctetoMascara, 7.0) + Math.Pow(OctetoMascara, 6.0) + Math.Pow(OctetoMascara, 5.0) + Math.Pow(OctetoMascara, 4.0);
			}
			else if (Sufijo == 5)
			{
				OctetoMascara = Math.Pow(OctetoMascara, 7.0) + Math.Pow(OctetoMascara, 6.0) + Math.Pow(OctetoMascara, 5.0) + Math.Pow(OctetoMascara, 4.0) + Math.Pow(OctetoMascara, 3.0);
			}
			else if (Sufijo == 6)
			{
				OctetoMascara = Math.Pow(OctetoMascara, 7.0) + Math.Pow(OctetoMascara, 6.0) + Math.Pow(OctetoMascara, 5.0) + Math.Pow(OctetoMascara, 4.0) + Math.Pow(OctetoMascara, 3.0) + Math.Pow(OctetoMascara, 2.0);
			}
			else if (Sufijo == 7)
			{
				OctetoMascara = Math.Pow(OctetoMascara, 7.0) + Math.Pow(OctetoMascara, 6.0) + Math.Pow(OctetoMascara, 5.0) + Math.Pow(OctetoMascara, 4.0) + Math.Pow(OctetoMascara, 3.0) + Math.Pow(OctetoMascara, 2.0) + Math.Pow(OctetoMascara, 1.0);
			}
			else if (Sufijo == 8)
			{
				OctetoMascara = Math.Pow(OctetoMascara, 7.0) + Math.Pow(OctetoMascara, 6.0) + Math.Pow(OctetoMascara, 5.0) + Math.Pow(OctetoMascara, 4.0) + Math.Pow(OctetoMascara, 3.0) + Math.Pow(OctetoMascara, 2.0) + Math.Pow(OctetoMascara, 1.0) + Math.Pow(OctetoMascara, 0);
			}
			switch (Donde)
			{
				case "2":
					txbMascara2.Text = Convert.ToString(OctetoMascara);
					txbWildcard2.Text = Convert.ToString(255 - OctetoMascara);
					break;
				case "3":
					txbMascara3.Text = Convert.ToString(OctetoMascara);
					txbWildcard3.Text = Convert.ToString(255 - OctetoMascara);
					break;
				case "4":
					txbMascara4.Text = Convert.ToString(OctetoMascara);
					txbWildcard4.Text = Convert.ToString(255 - OctetoMascara);
					break;
				default:
					MessageBox.Show("Algo salio mal :T");
					break;
			}
			
			
		}
		#endregion
	}
}
