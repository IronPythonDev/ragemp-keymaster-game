namespace Keymaster.Rage.Shared.Server
{
    public static class KeyMasterGameEvents
    {
        public static void SendScore(int score) =>
            RAGE.Events.CallRemote(Enums.ServerEvents.ProcessScoreFromKeyMasterGame, score);
    }
}
