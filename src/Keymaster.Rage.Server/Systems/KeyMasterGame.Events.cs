using GTANetworkAPI;
using Keymaster.Rage.Shared.Client;
using System;

namespace Keymaster.Rage.Server.Systems
{
    public partial class KeyMasterGame : Script
    {
        [RemoteEvent("Server:Game:KeyMaster:Score:Process")]
        public void ProcessScoreFromKeyMasterGame(Player player, int score)
        {
            try
            {

                // Здесь вы можете сохранять все в БД

                ChatEvents.PushMessage(player, $"You have been credited with {score} PGD");//Тригерим событие Client:Chat:Push
            }
            catch (Exception ex)
            {
                NAPI.Log.Exception(ex.Message);
            }
        }
    }
}
