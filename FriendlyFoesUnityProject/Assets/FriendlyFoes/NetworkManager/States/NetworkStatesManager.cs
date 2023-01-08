using Fusion;
using System.Collections.Generic;
using UnityEngine;

namespace FriendlyFoes.NetworkManager.States
{
    public class NetworkStatesManager : NetworkBehaviour
    {
        private Dictionary<PlayerRef, ClusterState> clustersAssociation = new Dictionary<PlayerRef, ClusterState>();
        private List<ClusterState> clusters = new List<ClusterState>();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        #region Adding new cluster
        public void HandleNewPlayerConnected(PlayerRef player)
        {
            if(Runner.IsServer)
            {
                Rpc_AskClusterFromNewPlayer(player);
            }
        }

        [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
        public void Rpc_AskClusterFromNewPlayer([RpcTarget] PlayerRef player)
        {
            ClusterState newCluster = new ClusterState();
            newCluster.isConnected = true;
            newCluster.players.Set(0, new PlayerState { name = $"Player{Random.Range(0,5000)}", color = Color.red});
            Rpc_ShareLocalClusterWithAuthority(player, newCluster);
        }
        
        [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
        public void Rpc_ShareLocalClusterWithAuthority(PlayerRef sourcePlayerRef, ClusterState cluster)
        {
            if(clustersAssociation.ContainsKey(sourcePlayerRef))
            {
                clustersAssociation[sourcePlayerRef] = cluster;
            }
            else
            {
                clustersAssociation.Add(sourcePlayerRef, cluster);
                clusters.Add(cluster);
            }
        }
        #endregion

        #region Removing cluster
        public void HandlePlayerDisconnected(PlayerRef player)
        {
            if(Runner.IsServer)
            {
                clusters.Remove(clustersAssociation[player]);
                clustersAssociation.Remove(player);
            }
        }
        #endregion
    }
}