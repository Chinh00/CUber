
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignaIR;

public abstract class HubBase : Hub
{
    public static string HubName = "signaIR";
}