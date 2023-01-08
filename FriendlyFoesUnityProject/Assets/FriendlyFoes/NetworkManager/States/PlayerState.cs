using Fusion;
using UnityEngine;

namespace FriendlyFoes.NetworkManager.States
{
    /// <summary>
    /// Represents a unique player, wether it is alone on a machine or not
    /// </summary>
    [System.Serializable]
    public struct PlayerState : INetworkStruct
    {
        public NetworkString<_16> name;
        public Color color;
    }
}