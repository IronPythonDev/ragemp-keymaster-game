using GTANetworkAPI;
using System;

namespace Keymaster.Rage.Server.Systems
{
    public partial class KeyMasterGame
    {
        ColShape ColShape { get; set; }

        public KeyMasterGame()
        {
            NAPI.Marker.CreateMarker(
                1, 
                new Vector3(-418.60727, 1148.3651, 324.85483), 
                new Vector3(), 
                new Vector3(), 
                1f, 
                new Color(255, 0, 0, 155), 
                dimension: NAPI.GlobalDimension);

            ColShape = NAPI.ColShape.CreatCircleColShape(
                -418.60727f, 
                1148.3651f, 
                1f, 
                NAPI.GlobalDimension);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void PlayerEnterCheckpoint(ColShape colShape, Player player)
        {
            try
            {
                if (colShape != ColShape) return;

                player.TriggerEvent(Rage.Shared.Enums.ClientEvents.OpenKeyMasterGame);
            }
            catch (Exception ex)
            {
                NAPI.Log.Exception(ex.Message);
            }
        }
    }
}
