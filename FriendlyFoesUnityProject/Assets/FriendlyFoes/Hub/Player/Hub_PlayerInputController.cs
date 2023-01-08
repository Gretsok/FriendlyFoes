using UnityEngine;

namespace FriendlyFoes.Hub.Player
{
    public class Hub_PlayerInputController : NetworkManager.Controls.ANetworkInputController
    {
        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();
            if (GetInput(out NetworkManager.NetworkInputData inputData))
            {
                var playerInputData = inputData.playerData[localPlayerIndex];



                if (playerInputData.directionInput.sqrMagnitude > 1)
                {
                    playerInputData.directionInput.Normalize();
                }

                Debug.Log($"Local player {localPlayerIndex} | movement : {playerInputData.directionInput}");

                var currentCharacter = CurrentCharacter;
                if(currentCharacter)
                {
                    if (currentCharacter is Hub_PlayerCharacter)
                    {
                        (currentCharacter as Hub_PlayerCharacter).SetDirection(playerInputData.directionInput);
                    }
                }
            }
        }
    }
}