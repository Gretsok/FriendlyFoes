using Fusion;

namespace FriendlyFoes.NetworkManager.States
{
    /// <summary>
    /// Represents a group of players on the same machine. It can be made of only one player.
    /// </summary>
    [System.Serializable]
    public struct ClusterState : INetworkStruct
    {
        [Networked, Capacity(8)]
        public NetworkArray<PlayerState> players => default;
        public bool isConnected;
    }
}