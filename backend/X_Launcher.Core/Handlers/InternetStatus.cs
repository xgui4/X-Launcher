using System.Net.NetworkInformation;

namespace X_Launcher_Core.Handlers;

public class InternetStatus
{
    private const string Reference = "www.minecraft.net";

    public static bool IsConnected()
    {
        try
        {
            using var ping = new Ping();
            var reply = ping.Send(Reference);
            return reply.Status == IPStatus.Success;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}