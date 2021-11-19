using System;
using WanaDo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WanaDo
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new TopPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}