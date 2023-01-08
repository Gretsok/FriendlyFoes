using UnityEngine;
using Fusion;

namespace FriendlyFoes.NetworkManager
{
    public struct NetworkInputData : INetworkInput
    {
        public struct PlayerData : INetworkInput
        {
            public Vector2 directionInput;
        }

        [Networked, Capacity(8)]
        public NetworkArray<PlayerData> playerData => default;
    }
}