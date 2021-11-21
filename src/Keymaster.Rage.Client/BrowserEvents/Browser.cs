using System;
using System.Collections.Generic;
using System.Text;

namespace Keymaster.Rage.Client.BrowserEvents
{
    public class Browser : RAGE.Events.Script
    {
        public Browser()
        {
            RAGE.Events.Add("Browser:Close", CloseBrowser);
        }

        private void CloseBrowser(object[] args)
        {
            try
            {
                if (!ushort.TryParse($"{args[0]}", out ushort id)) return;

                BrowserContainer.RemoveById(id);
            }
            catch (Exception ex)
            {
                RAGE.Chat.Output(ex.Message);
            }
        }
    }
}
