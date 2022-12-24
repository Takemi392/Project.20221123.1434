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
  internal partial class MainPageViewModel
  {
    public MainPageViewModel()
    {
    }

    [RelayCommand]
    private void Loaded()
    {
      var startupMagicPacket = Preferences.Get(nameof(ConfigurationPageViewModel.StartupMagicPacket), false);
      if (startupMagicPacket)
      {
        this.SendMagicPacket();
      }
    }

    [RelayCommand]
    private void Appearing()
    {
    }

    [RelayCommand]
    private void SendMagicPacket()
    {
      var host = Preferences.Get(nameof(ConfigurationPageViewModel.BroadcastAddress), (string)null);
      var port = Preferences.Get(nameof(ConfigurationPageViewModel.Port), -1);
      var addr = Preferences.Get(nameof(ConfigurationPageViewModel.MacAddress), (string)null);

      if (!String.IsNullOrEmpty(host) && port != -1 && !String.IsNullOrEmpty(addr))
        Models.MagicPacket.Send(host, port, addr);
    }
  }
}
