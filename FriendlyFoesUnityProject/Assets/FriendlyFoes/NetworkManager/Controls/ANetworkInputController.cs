using Fusion;

namespace FriendlyFoes.NetworkManager.Controls
{
    public abstract class ANetworkInputController :  NetworkBehaviour
    {
        [Networked]
        private NetworkId _possessedCharacterID { get; set; }
        [Networked]
        public int localPlayerIndex { get; set; }

        /// <summary>
        /// Cache this value to use it
        /// </summary>
        public ANetworkCharacter CurrentCharacter
        {
            get
            {
                if (Runner.TryFindObject(_possessedCharacterID, out NetworkObject networkCharacterObject))
                {
                    if (networkCharacterObject.TryGetComponent(out ANetworkCharacter currentNetworkCharacter))
                    {
                        return currentNetworkCharacter;
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// Can only be ran by server
        /// </summary>
        /// <param name="characterToPossess"></param>
        public void Possess(ANetworkCharacter characterToPossess)
        {
            if (!Runner.IsServer) return;

            var currentCharacter = CurrentCharacter;

            if (currentCharacter == characterToPossess) return;

            if (currentCharacter)
                currentCharacter.OnUnpossessed();
            currentCharacter = characterToPossess;
            _possessedCharacterID = currentCharacter.Object.Id;
            if (currentCharacter)
                currentCharacter.OnPossessed(this);

        }
    }
}