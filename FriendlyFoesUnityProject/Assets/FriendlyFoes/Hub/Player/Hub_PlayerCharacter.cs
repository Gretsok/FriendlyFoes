using Fusion;
using UnityEngine;

namespace FriendlyFoes.Hub.Player
{
    public class Hub_PlayerCharacter : NetworkManager.Controls.ANetworkCharacter
    {
        private NetworkCharacterControllerPrototype _characterController = null;

        [Networked]
        private Vector2 direction { get; set; }

        private void Awake()
        {
            _characterController = GetComponent<NetworkCharacterControllerPrototype>();
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();

            if(direction.sqrMagnitude > 1)
            {
                direction.Normalize();
            }

            _characterController.Move(new UnityEngine.Vector3(direction.x, 0f, direction.y));
            
        }

        public void SetDirection(Vector2 direction)
        {
            this.direction = direction;
        }
    }
}