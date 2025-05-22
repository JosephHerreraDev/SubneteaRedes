using System.Windows;
using System;

namespace SubneteaRedes
{
	/// <summary>
	/// Interaction logic for Redes.xaml
	/// </summary>
	public partial class Redes : Window
	{
		DSRedes dataset = new DSRedes();
		public Redes()
		{
			#region Inicia los componentes y llama a la funcion Subnetear
			InitializeComponent();
			Subnetear(ClasePrincipal.Sufijo, ClasePrincipal.Clase);
			#endregion
		}

		#region Evento Click
		/// <summary>
		/// Nos regresa a la pagina MainWindow
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MainWindow win1 = new MainWindow();
			win1.Show();
			this.Close();
		}
		#endregion

		#region Funcion subneteo
		/// <summary>
		/// Se encarga de subnetar usando el sufijo segun la clase y mostrarlo
		/// </summary>
		/// <param name="Sufijo"></param>
		/// <param name="Clase"></param>
		public void Subnetear(int Sufijo, string Clase)
		{
			#region Inicializacion de las variables
			int NumeroMagico = 0;
			int IPrimerOcteto = ClasePrincipal.PrimerOcteto;
			int ISegundoOcteto = ClasePrincipal.SegundoOcteto;
			int ITercerOcteto = ClasePrincipal.TercerOcteto;
			int ICuartoOcteto = ClasePrincipal.CuartoOcteto;

			string PrimerOcteto = Convert.ToString(IPrimerOcteto);
			string SegundoOcteto = Convert.ToString(ISegundoOcteto);
			string TercerOcteto = Convert.ToString(ITercerOcteto);
			string CuartoOcteto = Convert.ToString(ICuartoOcteto);

			string Subred = ClasePrincipal.Subred;
			string PrimerHost = ClasePrincipal.PrimerHost;
			string UltimoHost = ClasePrincipal.UltimoHost;
			string Broadcast = ClasePrincipal.Broadcast;
            #endregion

            #region Switch que asigna el numero magico segun el sufijo
            switch (Sufijo)
            {
                case 1:
                    NumeroMagico = 128;
                    break;
                case 2:
                    NumeroMagico = 64;
                    break;
                case 3:
                    NumeroMagico = 32;
                    break;
                case 4:
                    NumeroMagico = 16;
                    break;
                case 5:
                    NumeroMagico = 8;
                    break;
                case 6:
                    NumeroMagico = 4;
                    break;
                case 7:
                    NumeroMagico = 2;
                    break;
                case 8:
                    NumeroMagico = 1;
                    break;

                default:
                    MessageBox.Show("Error al Subnetear, revisa tu red y sufijo.");
                    TerminarAplicacion();
                    break;
            }

            #endregion

            #region Switch que subnetea segun la clase usando el numero magico
            switch (Clase)
			{
				#region Clase A
				case "A":
					Subred = PrimerOcteto + "." +
						     SegundoOcteto + "." +
							 TercerOcteto + "." +
							 CuartoOcteto;

					PrimerHost = PrimerOcteto + "." +
								 SegundoOcteto + "." +
								 TercerOcteto + "." +
								 Convert.ToString(ICuartoOcteto + 1);

					UltimoHost = PrimerOcteto + "."
							   + Convert.ToString(NumeroMagico - 1) + "."
							   + "255.254";

					Broadcast = PrimerOcteto + "."
					          + Convert.ToString(NumeroMagico - 1) + "."
							  + "255.255";

					ISegundoOcteto = NumeroMagico;
					dataset.DTRedes.AddDTRedesRow(Subred, PrimerHost,UltimoHost, Broadcast);
					int NumeroMagicoOGA = NumeroMagico;
					for (int x = ISegundoOcteto; x < 255; )
					{
						
						Subred = PrimerOcteto + "." +
								 Convert.ToString(NumeroMagico) + "." +
								 TercerOcteto + "." +
								 CuartoOcteto;

						PrimerHost = PrimerOcteto + "." +
								     Convert.ToString(NumeroMagico) + "." +
									 TercerOcteto + "." +
									 Convert.ToString(ICuartoOcteto + 1);

						UltimoHost = PrimerOcteto + "."
								   + Convert.ToString((NumeroMagico + NumeroMagicoOGA) - 1) + "."
						           + Convert.ToString(255) + "."
								   + Convert.ToString(254);

						Broadcast = PrimerOcteto + "."
					              + Convert.ToString((NumeroMagico + NumeroMagicoOGA) - 1) + "."
								  + Convert.ToString(255) + "."
								  + Convert.ToString(255);

						NumeroMagico = NumeroMagico + NumeroMagicoOGA;
						ISegundoOcteto = ISegundoOcteto + NumeroMagicoOGA;
						dataset.DTRedes.AddDTRedesRow(Subred, PrimerHost, UltimoHost, Broadcast);
						
						x = NumeroMagico;
					}
					break;
				#endregion

				#region Clase B
				case "B":
					Subred = PrimerOcteto + "." +
							 SegundoOcteto + "." +
							 TercerOcteto + "." +
							 CuartoOcteto;

					PrimerHost = PrimerOcteto + "." +
								 SegundoOcteto + "." +
								 TercerOcteto + "." +
								 Convert.ToString(ICuartoOcteto + 1);

					UltimoHost = PrimerOcteto + "."
							   + SegundoOcteto + "."
							   + Convert.ToString(NumeroMagico - 1) + "254";

					Broadcast = PrimerOcteto + "."
							   + SegundoOcteto + "."
							   + Convert.ToString(NumeroMagico - 1) + "255";

					ITercerOcteto = NumeroMagico;
					dataset.DTRedes.AddDTRedesRow(Subred, PrimerHost, UltimoHost, Broadcast);
					int NumeroMagicoOGB = NumeroMagico;
					for (int x = ISegundoOcteto; x < 255;)
					{

						Subred = PrimerOcteto + "." +
								 SegundoOcteto + "." +
								 Convert.ToString(NumeroMagico) + "." +
								 CuartoOcteto;

						PrimerHost = PrimerOcteto + "." +
									 SegundoOcteto + "." +
									 Convert.ToString(NumeroMagico) + "." +
									 Convert.ToString(ICuartoOcteto + 1);

						UltimoHost = PrimerOcteto + "."
								   + SegundoOcteto + "."
								   + Convert.ToString((NumeroMagico + NumeroMagicoOGB) - 1) + "."
								   + Convert.ToString(254);

						Broadcast = PrimerOcteto + "."
								  +  SegundoOcteto + "."
								  + Convert.ToString((NumeroMagico + NumeroMagicoOGB) - 1) + "."
								  + Convert.ToString(255);

						NumeroMagico = NumeroMagico + NumeroMagicoOGB;
						ISegundoOcteto = ISegundoOcteto + NumeroMagicoOGB;
						dataset.DTRedes.AddDTRedesRow(Subred, PrimerHost, UltimoHost, Broadcast);

						x = NumeroMagico;
					}
					break;
				#endregion

				#region Clase C
				case "C":
					Subred = PrimerOcteto + "." +
							 SegundoOcteto + "." +
							 TercerOcteto + "." +
							 CuartoOcteto;

					PrimerHost = PrimerOcteto + "." +
								 SegundoOcteto + "." +
								 TercerOcteto + "." +
								 Convert.ToString(ICuartoOcteto + 1);

					UltimoHost = PrimerOcteto + "."
							   + SegundoOcteto + "."
							   + TercerOcteto + "." 
							   + Convert.ToString(NumeroMagico - 2);

					Broadcast = PrimerOcteto + "."
							   + SegundoOcteto + "."
							   + TercerOcteto + "."
							   + Convert.ToString(NumeroMagico - 1);

					ISegundoOcteto = NumeroMagico;
					dataset.DTRedes.AddDTRedesRow(Subred, PrimerHost, UltimoHost, Broadcast);
					int NumeroMagicoOGC = NumeroMagico;
					for (int x = ISegundoOcteto; x < 255;)
					{

						Subred = PrimerOcteto + "." +
								 SegundoOcteto + "." +
								 TercerOcteto + "." +
								 Convert.ToString(NumeroMagico);

						PrimerHost = PrimerOcteto + "." +
									 SegundoOcteto + "." +
									 TercerOcteto + "." +
									 Convert.ToString(NumeroMagico + 1);

						UltimoHost = PrimerOcteto + "."
								   + SegundoOcteto + "."
								   + TercerOcteto + "."
								   + Convert.ToString(NumeroMagico + NumeroMagicoOGC - 2);

						Broadcast = PrimerOcteto + "."
								  + SegundoOcteto + "."
								  + TercerOcteto + "."
								  + Convert.ToString((NumeroMagico + NumeroMagicoOGC) - 1);

						NumeroMagico = NumeroMagico + NumeroMagicoOGC;
						ISegundoOcteto = ISegundoOcteto + NumeroMagicoOGC;
						dataset.DTRedes.AddDTRedesRow(Subred, PrimerHost, UltimoHost, Broadcast);

						x = NumeroMagico;
					}
					break;
				#endregion

				default:
					TerminarAplicacion();
					break;	
			}
			#endregion

			#region carga la datatable a el datagrid
			dataGrid.ItemsSource = dataset.DTRedes;
			#endregion

			#endregion
		}
		#region Funcion para cerrar la aplicacion y evitar que se repita el ciclo
		public void TerminarAplicacion()
		{
	       System.Windows.Application.Current.Shutdown();
			Environment.Exit(0); 
		}
		#endregion

	}
}
