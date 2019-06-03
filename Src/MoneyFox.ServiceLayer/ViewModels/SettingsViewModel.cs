﻿using System.Threading.Tasks;
using MoneyFox.Foundation;
using MoneyFox.Foundation.Models;
using MoneyFox.Foundation.Resources;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MoneyFox.ServiceLayer.ViewModels
{
    public interface ISettingsViewModel : IBaseViewModel
    {
        /// <summary>
        ///     Contains all available Settings items.
        /// </summary>
        MvxObservableCollection<SettingsSelectorType> SettingsList { get; }

        /// <summary>
        ///     Navigate to a concrete settings page.
        ///     Used in Xamarin Forms.
        /// </summary>
        MvxAsyncCommand<SettingsSelectorType> GoToSettingCommand { get; }

        /// <summary>
        ///     View Model for the Background job part.
        /// </summary>
        ISettingsBackgroundJobViewModel BackgroundJobViewModel { get; }

        ISettingsPersonalizationViewModel PersonalizationViewModel { get; }
        ISettingsSecurityViewModel SettingsSecurityViewModel { get; }
    }

    /// <summary>
    ///     ViewModel for the settings view.
    /// </summary>
    public class SettingsViewModel : BaseNavigationViewModel, ISettingsViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public SettingsViewModel(IMvxNavigationService navigationService,
                                 IAboutViewModel aboutViewModel, 
                                 ISettingsBackgroundJobViewModel settingsBackgroundJobViewModel,
                                 ISettingsPersonalizationViewModel settingsPersonalizationViewModel,
                                 IMvxLogProvider logProvider) : base(logProvider, navigationService)
        {
            this.navigationService = navigationService;

            AboutViewModel = aboutViewModel;
            BackgroundJobViewModel = settingsBackgroundJobViewModel;
            PersonalizationViewModel = settingsPersonalizationViewModel;
        }

        public IAboutViewModel AboutViewModel { get; }

        /// <inheritdoc />
        public MvxObservableCollection<SettingsSelectorType> SettingsList => new MvxObservableCollection<SettingsSelectorType>
        {
            //new SettingsSelectorType
            //{
            //    Name = Strings.SettingsPersonalizationLabel,
            //    Description = Strings.SettingsPersonalizationDescription,
            //    Type = SettingsType.Personalization
            //},
            new SettingsSelectorType
            {
                Name = Strings.CategoriesLabel,
                Icon = "\uf316",
                Description = Strings.CategoriesSettingsDescription,
                Type = SettingsType.Categories
            },
            new SettingsSelectorType
            {
                Name = Strings.BackgroundJobLabel,
                Icon = "\uf4e6",
                Description = Strings.BackgroundJobSettingDescription,
                Type = SettingsType.BackgroundJob
            },
            new SettingsSelectorType
            {
                Name = Strings.BackupLabel,
                Icon = "\uf167",
                Description = Strings.BackupSettingsDescription,
                Type = SettingsType.Backup
            },
            new SettingsSelectorType
            {
                Name = Strings.AboutLabel,
                Icon = "\uf2fd",
                Description = Strings.AboutSettingsDescription,
                Type = SettingsType.About
            }
        };

        /// <inheritdoc />
        public MvxAsyncCommand<SettingsSelectorType> GoToSettingCommand => new MvxAsyncCommand<SettingsSelectorType>(GoToSettings);

        public ISettingsBackgroundJobViewModel BackgroundJobViewModel { get; set; }
        public ISettingsPersonalizationViewModel PersonalizationViewModel { get; set; }
        public ISettingsSecurityViewModel SettingsSecurityViewModel { get; set; }

        private async Task GoToSettings(SettingsSelectorType item)
        {
            switch (item.Type)
            {
                case SettingsType.Personalization:
                    await navigationService.Navigate<SettingsPersonalizationViewModel>();
                    break;

                case SettingsType.Categories:
                    await navigationService.Navigate<CategoryListViewModel>();
                    break;

                case SettingsType.BackgroundJob:
                    await navigationService.Navigate<SettingsBackgroundJobViewModel>();
                    break;

                case SettingsType.Backup:
                    await navigationService.Navigate<BackupViewModel>();
                    break;

                case SettingsType.About:
                    await navigationService.Navigate<AboutViewModel>();
                    break;
            }
        }
    }
}