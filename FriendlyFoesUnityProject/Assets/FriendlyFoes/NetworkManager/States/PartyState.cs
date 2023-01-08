
using Fusion;

namespace FriendlyFoes.NetworkManager.States
{
    /// <summary>
    /// Represents a group of clusters that can play together.
    /// </summary>
    [System.Serializable]
    public struct PartyState : INetworkStruct
    {
        [Networked, Capacity(8)]
        public NetworkArray<ClusterState> clusters => default;
    }
}