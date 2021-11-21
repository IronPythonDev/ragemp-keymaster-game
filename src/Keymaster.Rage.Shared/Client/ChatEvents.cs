using GTANetworkAPI;

namespace Keymaster.Rage.Shared.Client
{
    public static class ChatEvents
    {
        public static void PushMessage(Player player, string message) =>
            player.TriggerEvent(Keymaster.Rage.Shared.Enums.ClientEvents.PushIntoChat, message);
    }
}
