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
    public const bool DEFAULT_STARTUP_MAGIC_PACKET = false;

    #region BroadcastAddress
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BroadcastAddress))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private int broadcastAddress_I0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BroadcastAddress))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private int broadcastAddress_I1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BroadcastAddress))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private int broadcastAddress_I2;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BroadcastAddress))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private int broadcastAddress_I3;

    public string BroadcastAddress => $"{this.BroadcastAddress_I0}.{this.BroadcastAddress_I1}.{this.BroadcastAddress_I2}.{this.BroadcastAddress_I3}";
    #endregion

    #region Port
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private int port;
    #endregion

    #region
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MacAddress))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string macAddress_I0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MacAddress))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string macAddress_I1;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MacAddress))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string macAddress_I2;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MacAddress))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string macAddress_I3;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MacAddress))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string macAddress_I4;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MacAddress))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string macAddress_I5;

    public string MacAddress => $"{this.MacAddress_I0}:{this.MacAddress_I1}:{this.MacAddress_I2}:{this.MacAddress_I3}:{this.MacAddress_I4}:{this.MacAddress_I5}";
    #endregion

    #region StartupMagicPacket
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private bool startupMagicPacket;
    #endregion

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
      try
      {
        var tmpBroadcastAddress = Preferences.Get(nameof(this.BroadcastAddress), DEFAULT_BROADCAST_ADDRESS).Split(".");

        this.BroadcastAddress_I0 = Convert.ToInt32(tmpBroadcastAddress[0]);
        this.BroadcastAddress_I1 = Convert.ToInt32(tmpBroadcastAddress[1]);
        this.BroadcastAddress_I2 = Convert.ToInt32(tmpBroadcastAddress[2]);
        this.BroadcastAddress_I3 = Convert.ToInt32(tmpBroadcastAddress[3]);
      }
      catch (Exception)
      {
        var tmpBroadcastAddress = DEFAULT_BROADCAST_ADDRESS.Split(".");

        this.BroadcastAddress_I0 = Convert.ToInt32(tmpBroadcastAddress[0]);
        this.BroadcastAddress_I1 = Convert.ToInt32(tmpBroadcastAddress[1]);
        this.BroadcastAddress_I2 = Convert.ToInt32(tmpBroadcastAddress[2]);
        this.BroadcastAddress_I3 = Convert.ToInt32(tmpBroadcastAddress[3]);
      }

      try
      {
        this.Port = Preferences.Get(nameof(this.Port), DEFAULT_PORT);
      }
      catch (Exception)
      {
        this.Port = DEFAULT_PORT;
      }

      try
      {
        var tmpMacAddress = Preferences.Get(nameof(this.MacAddress), DEFAULT_MAC_ADDRESS).Split(":");

        this.MacAddress_I0 = tmpMacAddress[0];
        this.MacAddress_I1 = tmpMacAddress[1];
        this.MacAddress_I2 = tmpMacAddress[2];
        this.MacAddress_I3 = tmpMacAddress[3];
        this.MacAddress_I4 = tmpMacAddress[4];
        this.MacAddress_I5 = tmpMacAddress[5];
      }
      catch (Exception)
      {
        var tmpMacAddress = DEFAULT_MAC_ADDRESS.Split(":");

        this.MacAddress_I0 = tmpMacAddress[0];
        this.MacAddress_I1 = tmpMacAddress[1];
        this.MacAddress_I2 = tmpMacAddress[2];
        this.MacAddress_I3 = tmpMacAddress[3];
        this.MacAddress_I4 = tmpMacAddress[4];
        this.MacAddress_I5 = tmpMacAddress[5];
      }

      try
      {
        this.StartupMagicPacket = Preferences.Get(nameof(this.StartupMagicPacket), DEFAULT_STARTUP_MAGIC_PACKET);
      }
      catch (Exception)
      {
        this.StartupMagicPacket = DEFAULT_STARTUP_MAGIC_PACKET;
      }
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save()
    {
      Preferences.Set(nameof(this.BroadcastAddress), this.BroadcastAddress);
      Preferences.Set(nameof(this.Port), this.Port);
      Preferences.Set(nameof(this.MacAddress), this.MacAddress);
      Preferences.Set(nameof(this.StartupMagicPacket), this.StartupMagicPacket);
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
