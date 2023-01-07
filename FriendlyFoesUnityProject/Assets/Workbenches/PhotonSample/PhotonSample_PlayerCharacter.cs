using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Workbenches.PhotonSample
{
    public class PhotonSample_PlayerCharacter : NetworkBehaviour
    {
        private NetworkCharacterControllerPrototype _cc;

        private void Awake()
        {
            _cc = GetComponent<NetworkCharacterControllerPrototype>();
        }


        public override void FixedUpdateNetwork() 
        {
            if (GetInput(out PhotonSample_NetworkInputData data))
            {
                data.direction.Normalize();
                _cc.Move(5 * data.direction * Runner.DeltaTime);
            }
        }
    }
}