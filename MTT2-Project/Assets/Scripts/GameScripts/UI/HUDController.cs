using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MTT2.HUD
{
    public class HUDController : MonoBehaviour
    {
        public SceneController sceneController;
        public PlayerController playerController;

        private Label speedMeter;
        private VisualElement fuelMeter;
        private VisualElement speedSymbol;
        private Slider playerPos;

        private void Awake()
        {
            SetHUDElements();
        }

        private void SetHUDElements()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            speedMeter = root.Q<Label>("SpeedCurrent");
            fuelMeter = root.Q("FuelLevel");
            speedSymbol = root.Q("Lord");
            playerPos = root.Q<Slider>("Slider");
        }

        void Update()
        {
            Header();
            Footer();
        }
        void Header()
        {
            var rigidBody = playerController.TruckController.RigidBody2d;

            speedMeter.text = Mathf.Abs(rigidBody.velocity.x * 10).ToString("000");
            speedSymbol.style.width = new StyleLength(Length.Percent(InversePercentage(rigidBody.velocity.x, 10)));
            
            fuelMeter.style.height = new StyleLength(Length.Percent(Percentage(playerController.TruckController.fuel, playerController.TruckController.TruckDef.maxFuelBase)));
        }
        void Footer()
        {
            PlayerPositionHUD();
        }
        void PlayerPositionHUD()
        {
            float lvlSize = Vector3.Distance(sceneController.spawnPoint.position, sceneController.finishLine.position);
            float inLvlPos = Vector3.Distance(playerController.TruckController.transform.position, sceneController.finishLine.position);
            playerPos.value = InversePercentage(inLvlPos, lvlSize);
        }
        private static float Percentage(float current, float max)
        {
            float percentage = (current/max) * 100;
            return percentage;
        }
        private static float InversePercentage(float current, float max)
        {
            float inverse = (max - current);
            float result = (inverse/max) * 100;
            return result;
        }
    }
}