using Nebby.UnityUtils;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MTT2
{
    public class PlayerController : SingletonBehaviour<PlayerController>
    {
        public static Action<PlayerController> OnPlayerStart;
        public TruckDef chosenTruck;
        public WheelDef chosenWheels;
        public GameObject truckPrefab;

        public float drive;
        public float steer;
        public bool breaking;

        public TruckController TruckController { get; private set; }

        public void Start()
        {
            SpawnPlayerTruck();
            OnPlayerStart?.Invoke(this);
        }
        public void SpawnPlayerTruck()
        {
            if (SceneController.Instance)
                SpawnPlayerTruck(SceneController.Instance.spawnPoint);
            else
                throw new InvalidOperationException($"Cannot spawn the player as the current scene does not have a SceneController! Use SpawnPlayerTruck(Transform spawnPosition) instead.");
        }

        public void SpawnPlayerTruck(Transform spawnPosition)
        {
            var obj = Instantiate(truckPrefab, spawnPosition.position, Quaternion.Euler(Vector3.zero));
            TruckController = obj.GetComponent<TruckController>();
            TruckController.TruckDef = chosenTruck;
            TruckController.SetWheelDef(chosenWheels);

            CameraController.Instance.SetFollow(obj.transform);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            drive = input.y;
            steer = input.x;
        }

        public void OnBreak(InputAction.CallbackContext context)
        {
            breaking = context.ReadValueAsButton();
        }

        private void FixedUpdate()
        {
            TruckControllerUpdate();
        }

        private void TruckControllerUpdate()
        {
            TruckController.driveValue = drive;
            TruckController.steerValue = steer;
            TruckController.breaking = breaking;
        }
    }
}