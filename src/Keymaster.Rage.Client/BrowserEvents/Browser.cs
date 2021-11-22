using System;

namespace Keymaster.Rage.Client.BrowserEvents
{
    public class Browser : RAGE.Events.Script
    {
        public Browser()
        {
            RAGE.Events.Add("Browser:Close", CloseBrowser);
        }

        // Просто удаляем браузер, в аргументы нужно передать ИД браузера который можно получить через window.browserId
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
