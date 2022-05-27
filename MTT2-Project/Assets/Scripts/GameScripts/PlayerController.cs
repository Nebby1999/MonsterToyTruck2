﻿using MTT2.Addons;
using Nebby.UnityUtils;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MTT2
{
    public class PlayerController : SingletonBehaviour<PlayerController>
    {
        public static Action<PlayerController> OnPlayerStart;
        public PlayerPreferenceData playerPreferenceData;
        public GameObject truckPrefab;

        public TruckController TruckController { get; private set; }
        public AddonManager AddonManager { get; private set; }

        private Vector2 _driveSteer;
        private bool _breaking;
        private Vector2 _addonControl;
        public Vector2 _mouse;

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
            TruckController.TruckDef = playerPreferenceData.truckDef;
            TruckController.SetWheelDef(playerPreferenceData.wheelDef);
            AddonManager = TruckController.AddonManager;

            AddonManager.SpawnAddons(playerPreferenceData.addonSpawnData);
            CameraController.Instance.SetFollow(obj.transform);
        }

        private void FixedUpdate()
        {
            TruckControllerFixedUpdate();
            AddonManagerFixedUpdate();
        }

        private void TruckControllerFixedUpdate()
        {
            TruckController.driveValue = _driveSteer.y;
            TruckController.steerValue = _driveSteer.x;
            TruckController.breaking = _breaking;
        }

        private void AddonManagerFixedUpdate()
        {
            AddonManager.AddonControl = _addonControl;
            AddonManager.MousePos = _mouse;
        } 

        #region Input Action Callbacks
        public void OnMove(InputAction.CallbackContext context)
        {
            _driveSteer = context.ReadValue<Vector2>();
        }

        public void OnBreak(InputAction.CallbackContext context)
        {
            _breaking = context.ReadValueAsButton();
        }

        public void OnAddonTrigger(InputAction.CallbackContext context)
        {
            AddonLocator.AddonLocation location = AddonLocator.AddonLocation.Unknown;
            switch(context.action.name)
            {
                case "AddonTopTrigger":
                    location = AddonLocator.AddonLocation.Top;
                    break;
                case "AddonBottomTrigger":
                    location = AddonLocator.AddonLocation.Bottom;
                    break;
                case "AddonFrontTrigger":
                    location = AddonLocator.AddonLocation.Front;
                    break;
                case "AddonBackTrigger":
                    location = AddonLocator.AddonLocation.Back;
                    break;
            }
            if (location == AddonLocator.AddonLocation.Unknown)
                return;
            AddonManager.RelyTrigger(ref context, location);
        }

        public void OnAddonControl(InputAction.CallbackContext context)
        {
            _addonControl = context.ReadValue<Vector2>();
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            _mouse = context.ReadValue<Vector2>();
        }
        #endregion
    }
}