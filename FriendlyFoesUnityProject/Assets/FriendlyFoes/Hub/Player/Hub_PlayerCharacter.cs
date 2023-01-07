using Fusion;

namespace FriendlyFoes.Hub.Player
{
    public class Hub_PlayerCharacter : NetworkBehaviour
    {
        private NetworkCharacterControllerPrototype _characterController = null;

        private void Awake()
        {
            _characterController = GetComponent<NetworkCharacterControllerPrototype>();
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();
            if(GetInput(out NetworkManager.NetworkInputData inputData))
            {
                if(inputData.directionInput.sqrMagnitude > 1)
                {
                    inputData.directionInput.Normalize();
                }

                _characterController.Move(new UnityEngine.Vector3(inputData.directionInput.x, 0f, inputData.directionInput.y));
            }
        }
    }
}