﻿namespace Contacts.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Threading.Tasks;
    using System.Linq;
    using Models;
    using Views;
    using Windows.UI.Xaml.Navigation;
    using Base;

    public class ShellViewModel : ViewModelBase
    {
        // Variables
        private bool _isPaneOpen;
        private ObservableCollection<MenuItem> _menuItems;
        private MenuItem _selectedMenuItem;

        // Commands
        private ICommand _hamburgerCommand;
        private ICommand _navigateCommand;

        public bool IsPaneOpen
        {
            get
            {
                return _isPaneOpen;
            }
            set
            {
                _isPaneOpen = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                RaisePropertyChanged();
            }
        }

        public MenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;
                RaisePropertyChanged();
            }
        }

        public ICommand HamburgerCommand
        {
            get { return _hamburgerCommand = _hamburgerCommand ?? new DelegateCommand(HamburgerCommandExecute); }
        }

        public ICommand NavigateCommand
        {
            get { return _navigateCommand = _navigateCommand ?? new DelegateCommand<MenuItem>(NavigateCommandExecute); }
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            LoadMenu();

            return null;
        }

        private void LoadMenu()
        {
            MenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Title = "Contacts",
                    View = typeof(ContactsView)
                },
                new MenuItem
                {
                    Title = "Pick",
                    View = typeof(PickView)
                }
            };

            SelectedMenuItem = MenuItems.First();
            Navigate(SelectedMenuItem.View);
        }

        private void HamburgerCommandExecute()
        {
            IsPaneOpen = (IsPaneOpen != true);
        }

        private void NavigateCommandExecute(MenuItem menuItem)
        {
            Navigate(menuItem.View);
        }

        private void Navigate(Type view)
        {
            if (view == null)
                return;

            SplitViewFrame.Navigate(view);                  
        }
    }
}