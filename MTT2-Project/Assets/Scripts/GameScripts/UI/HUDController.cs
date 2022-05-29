using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MTT2.HUD
{
    public class HUDController : MonoBehaviour
    {
        #region Classes
        public SceneController sceneController;
        public PlayerController playerController;
        #endregion
        #region UI
        HUDManager m_manager;
        private Label speedMeter;
        private VisualElement speedBar;
        private VisualElement fuelMeter;
        private VisualElement speedSymbol;
        private Slider playerPos;
        #endregion

        private void Awake()
        {
            SetHUDElements();
            m_manager.EnableHUD();
        }

        private void SetHUDElements()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            
            m_manager = root.Q<HUDManager>("Manager");

            speedMeter = root.Q<Label>("SpeedCurrent");
            fuelMeter = root.Q<VisualElement>("FuelLevel");
            speedBar = root.Q<VisualElement>("Meter");
            speedSymbol = root.Q<VisualElement>("Lord");
            playerPos = root.Q<Slider>("Slider");
        }

        void Update()
        {
            Header();
            Footer();
        }
        void Header()
        {
            SpeedMeterStyle();
            FuelMeterStyle();
        }
        void SpeedMeterStyle(){
            var rigidBody = playerController.TruckController.RigidBody2d;

            speedMeter.text = Mathf.Abs(rigidBody.velocity.x * 10).ToString("000");
            speedSymbol.style.width = new StyleLength(Length.Percent(InversePercentage(rigidBody.velocity.x, 15)));
            if(rigidBody.velocity.x >= 15){
                speedBar.style.unityBackgroundImageTintColor = Color.red;
                speedMeter.style.color = Color.red;
                }
            else if (rigidBody.velocity.x <= 15 && rigidBody.velocity.x >= 0 ){
                speedBar.style.unityBackgroundImageTintColor = Color.cyan;
                speedMeter.style.color = Color.white;
                }
            else if (rigidBody.velocity.x < -0.1){
                speedMeter.style.color = Color.yellow;
                }
        }
        void FuelMeterStyle(){
            fuelMeter.style.height = new StyleLength(Length.Percent(Percentage(playerController.TruckController.fuel, playerController.TruckController.TruckDef.maxFuelBase)));
            if(Percentage(playerController.TruckController.fuel, playerController.TruckController.TruckDef.maxFuelBase) >= 50)
                fuelMeter.style.backgroundColor = Color.white;
            else if(Percentage(playerController.TruckController.fuel, playerController.TruckController.TruckDef.maxFuelBase) <= 50 && Percentage(playerController.TruckController.fuel, playerController.TruckController.TruckDef.maxFuelBase) >= 25)
                fuelMeter.style.backgroundColor = Color.yellow;
            else if(Percentage(playerController.TruckController.fuel, playerController.TruckController.TruckDef.maxFuelBase) <= 25)
                fuelMeter.style.backgroundColor = Color.red;
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