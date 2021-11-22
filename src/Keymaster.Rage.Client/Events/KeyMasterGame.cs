using System;

namespace Keymaster.Rage.Client.Events
{
    public class KeyMasterGame : RAGE.Events.Script
    {
        public KeyMasterGame()
        {
            RAGE.Events.Add(Shared.Enums.ClientEvents.OpenKeyMasterGame, OpenKeyMasterGame);
            RAGE.Events.Add(Shared.Enums.ClientEvents.SendScoreFromKeyMasterGame, SendScoreFromKeyMasterGame);
        }

        private void SendScoreFromKeyMasterGame(object[] args)
        {
            try
            {
                if (!int.TryParse($"{args[0]}", out int score)) return;

                // Тригерим серверный ивент Server:Game:KeyMaster:Score:Process
                Keymaster.Rage.Shared.Server.KeyMasterGameEvents.SendScore(score);

                // Выводим в чат счет
                RAGE.Chat.Output($"Your score: {score}");
            }
            catch (Exception ex)
            {
                RAGE.Chat.Output(ex.Message);
            }
        }

        // Создаём и открываем игру
        private void OpenKeyMasterGame(object[] args)
        {
            try
            {
                Browser browser = new Browser("keymaster-game");

                browser.Open();
            }
            catch (Exception ex)
            {
                RAGE.Chat.Output(ex.Message);
            }
        }


    }
}
