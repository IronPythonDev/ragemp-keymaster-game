namespace Keymaster.Rage.Shared.Enums
{
    public class ClientEvents
    {
        // Client 
        public const string OpenKeyMasterGame = "Client:Game:KeyMaster:Open";
        public const string PushIntoChat = "Client:Chat:Push";

        // Browser
        public const string SendScoreFromKeyMasterGame = "Client:Game:KeyMaster:SendScoreToServer";
    }
}
