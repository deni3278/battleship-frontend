using System.Diagnostics;
using BattleshipFrontend.Models;
using Prism.Commands;

namespace BattleshipFrontend.ViewModels
{
    public class MenuPageViewModel
    {
        private Room _selectedRoom;

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                JoinCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand JoinCommand { get; }
        public DelegateCommand CreateCommand { get; }

        public MenuPageViewModel()
        {
            JoinCommand = new DelegateCommand(OnJoinAsync, () => SelectedRoom != null);
            CreateCommand = new DelegateCommand(OnCreateAsync);
        }

        public void OnJoinAsync()
        {
            Debug.WriteLine("Joining game.");
        }
        
        public void OnCreateAsync()
        {
            Debug.WriteLine("Creating game.");
        }
    }
}