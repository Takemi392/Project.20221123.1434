using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WoL.ViewModels
{
  [INotifyPropertyChanged]
  internal partial class ConfigurationPageViewModel
  {
    public const string DEFAULT_BROADCAST_ADDRESS = "255.255.255.255";
    public const int DEFAULT_PORT = 9;
    public const string DEFAULT_MAC_ADDRESS = "FF:FF:FF:FF:FF:FF";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string broadcastAddress;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private int port;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string macAddress;

    public ConfigurationPageViewModel()
    {
    }

    [RelayCommand]
    private void Loaded()
    {
    }

    [RelayCommand]
    private void Appearing()
    {
      this.BroadcastAddress = Preferences.Get(nameof(this.BroadcastAddress), DEFAULT_BROADCAST_ADDRESS);
      this.Port = Preferences.Get(nameof(this.Port), DEFAULT_PORT);
      this.MacAddress = Preferences.Get(nameof(this.MacAddress), DEFAULT_MAC_ADDRESS);
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save()
    {
      Preferences.Set(nameof(this.BroadcastAddress), this.BroadcastAddress);
      Preferences.Set(nameof(this.Port), this.Port);
      Preferences.Set(nameof(this.MacAddress), this.MacAddress);
    }

    private bool CanSave()
    {
      if (String.IsNullOrEmpty(this.BroadcastAddress) || !System.Text.RegularExpressions.Regex.IsMatch(this.BroadcastAddress, "\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}"))
        return false;

      if (!(0 <= this.Port && this.Port <= 65535))
        return false;

      if (String.IsNullOrEmpty(this.MacAddress) || !System.Text.RegularExpressions.Regex.IsMatch(this.MacAddress, "(?:[0-9a-fA-F]{2}\\:){5}[0-9a-fA-F]{2}"))
        return false;

      return true;
    }
  }
}
