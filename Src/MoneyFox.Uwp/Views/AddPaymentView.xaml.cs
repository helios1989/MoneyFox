﻿using MoneyFox.Application.Common;
using MoneyFox.Domain;
using MoneyFox.Uwp.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MoneyFox.Uwp.Views
{
    public sealed partial class AddPaymentView
    {
        public override string Header => ViewModelLocator.AddPaymentVm.Title;

        private AddPaymentViewModel ViewModel => DataContext as AddPaymentViewModel;

        public AddPaymentView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter != null && e.NavigationMode != NavigationMode.Back)
            {
                ViewModel.PaymentType = (PaymentType) e.Parameter;
                ViewModel.InitializeCommand.ExecuteAsync().FireAndForgetSafeAsync();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if(e.NavigationMode == NavigationMode.Back)
                ResetPageCache();
        }

        private void ResetPageCache()
        {
            int cacheSize = ((Frame) Parent).CacheSize;
            ((Frame) Parent).CacheSize = 0;
            ((Frame) Parent).CacheSize = cacheSize;
        }
    }
}
