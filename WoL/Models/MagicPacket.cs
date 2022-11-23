using System.Net.Sockets;

namespace WoL.Models
{
  public static class MagicPacket
  {
    public static bool Send(string host, int port, string addr)
    {
      var bAddr = new byte[6];
      foreach (var item in addr.Split(":").Select((v, i) => new { Value = v, Index = i }))
        bAddr[item.Index] = Convert.ToByte(item.Value?.ToString(), 16);

      var packet = new byte[6 + 6 * 16];

      Array.Copy(new byte[] { 0XFF, 0XFF, 0XFF, 0XFF, 0XFF, 0XFF }, 0, packet, 0, 6);
      for (var i = 0; i < 16; i++)
        Array.Copy(bAddr, 0, packet, 6 + i * 6, bAddr.Length);

      var udp = new UdpClient()
      {
        EnableBroadcast = true,
      };

      try
      {
        udp.Send(packet, packet.Length, host, port);
      }
      catch (Exception)
      {
        return false;
      }

      return true;
    }
  }
}
