using System;

namespace Keymaster.Rage.Client.Events
{
    public class Chat : RAGE.Events.Script
    {
        public Chat()
        {
            RAGE.Events.Add(Rage.Shared.Enums.ClientEvents.PushIntoChat, PushIntoChat);
        }

        private void PushIntoChat(object[] args)
        {
            try
            {
                var msg = args[0];

                RAGE.Chat.Output($"{msg}");
            }
            catch (Exception ex)
            {
                RAGE.Chat.Output(ex.Message);
            }
        }
    }
}
