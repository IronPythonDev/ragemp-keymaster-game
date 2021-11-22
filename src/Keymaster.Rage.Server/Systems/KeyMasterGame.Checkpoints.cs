using GTANetworkAPI;
using System;

namespace Keymaster.Rage.Server.Systems
{
    public partial class KeyMasterGame
    {
        ColShape ColShape { get; set; }

        public KeyMasterGame()
        {
            // Создаём маркер, подробнее про маркер вы можете узнать в документации GTANetwork
            NAPI.Marker.CreateMarker(
                1, 
                new Vector3(-418.60727, 1148.3651, 324.85483), 
                new Vector3(), 
                new Vector3(), 
                1f, 
                new Color(255, 0, 0, 155), 
                dimension: NAPI.GlobalDimension);

            // Создаём круглую 2D сферу, подробнее про маркер вы можете узнать в документации GTANetwork
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
                // Если сферу в которую вошел игрок не соответствует ранее созданной сфере выходим из метода
                if (colShape != ColShape) return;

                // Тригерим событие Client:Game:KeyMaster:Open которое открывает саму игру
                player.TriggerEvent(Rage.Shared.Enums.ClientEvents.OpenKeyMasterGame);
            }
            catch (Exception ex)
            {
                NAPI.Log.Exception(ex.Message);
            }
        }
    }
}
