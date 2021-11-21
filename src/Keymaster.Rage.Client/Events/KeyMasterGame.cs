﻿using System;

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

                Keymaster.Rage.Shared.Server.KeyMasterGameEvents.SendScore(score);

                RAGE.Chat.Output($"Your score: {score}");

                RAGE.Elements.Player.LocalPlayer.SetMoney(RAGE.Elements.Player.LocalPlayer.GetMoney() + score);
            }
            catch (Exception ex)
            {
                RAGE.Chat.Output(ex.Message);
            }
        }

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