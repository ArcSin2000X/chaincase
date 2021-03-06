﻿using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Chaincase.ViewModels;
using Chaincase.Views;
using Xamarin.Forms;

namespace Chaincase.Navigation
{
	public class AppBootstrapper : ReactiveObject, IScreen
	{
		public AppBootstrapper(
			bool walletExists,
			IMutableDependencyResolver dependencyResolver = null,
			RoutingState router = null)
		{
			Router = router ?? new RoutingState();

			RegisterParts(dependencyResolver ?? Locator.CurrentMutable);

			if (walletExists)
			{
				Router.Navigate.Execute(new MainViewModel(this));
			}
			else
			{
				Router.Navigate.Execute(new PassphraseViewModel(this));
			}
		}

		public RoutingState Router { get; private set; }

		private void RegisterParts(IMutableDependencyResolver dependencyResolver)
		{
			dependencyResolver.RegisterConstant(this, typeof(IScreen));

			dependencyResolver.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>));
			dependencyResolver.Register(() => new ReceivePage(), typeof(IViewFor<ReceiveViewModel>));
			dependencyResolver.Register(() => new SendPage(), typeof(IViewFor<SendViewModel>));
			dependencyResolver.Register(() => new PassphrasePage(), typeof(IViewFor<PassphraseViewModel>));
			dependencyResolver.Register(() => new MnemonicPage(), typeof(IViewFor<MnemonicViewModel>));
			dependencyResolver.Register(() => new VerifyMnemonicPage(), typeof(IViewFor<VerifyMnemonicViewModel>));
		}

		public Page CreateMainPage()
		{
			return new RoutedViewHost();
		}
	}
}
