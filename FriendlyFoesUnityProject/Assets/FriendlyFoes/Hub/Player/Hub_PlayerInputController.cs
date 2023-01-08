namespace FriendlyFoes.Hub.Player
{
    public class Hub_PlayerInputController : NetworkManager.Controls.ANetworkInputController
    {
        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();
            if (GetInput(out NetworkManager.NetworkInputData inputData))
            {
                if (inputData.directionInput.sqrMagnitude > 1)
                {
                    inputData.directionInput.Normalize();
                }
                
                var currentCharacter = CurrentCharacter;
                if(currentCharacter)
                {
                    if (currentCharacter is Hub_PlayerCharacter)
                    {
                        (currentCharacter as Hub_PlayerCharacter).SetDirection(inputData.directionInput);
                    }
                } 
            }
        }
    }
}