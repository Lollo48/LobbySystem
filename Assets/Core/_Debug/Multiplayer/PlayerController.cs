using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
   // [SerializeField] private PlayerInput playerInput;
   // [SerializeField] private StarterAssetsInputs starterAssetsInputs;
   // [SerializeField] private ThirdPersonController thirdPersonController;
   //
   // private void Awake()
   // {
   //    playerInput.enabled = false;
   //    starterAssetsInputs.enabled = false;
   //    thirdPersonController.enabled = false;
   // }
   //
   // public override void OnNetworkSpawn()
   // {
   //    base.OnNetworkSpawn();
   //
   //    if (IsOwner)
   //    {
   //       playerInput.enabled = true;
   //       starterAssetsInputs.enabled = true;
   //       thirdPersonController.enabled = true;
   //    }
   // }
}
