using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace MTT2.HUD{
public class HUD : MonoBehaviour{

    private PlayerController player;
    private TruckController truck;
    private Rigidbody2D playerRB;
    public Label speedMeter;
    public VisualElement fuelMeter;
        private void Awake()
        {
            PlayerController.OnPlayerStart += OnPlayerStart;
            // player = PlayerController.Instance;
            // playerRB = player.TruckController.GetComponent<Rigidbody2D>();
        }
        void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            speedMeter = root.Q<Label>("SpeedCurrent");
            fuelMeter = root.Q("Fuel");
        }
        void Update()
        {
            speedMeter.text = Mathf.Abs(playerRB.velocity.x).ToString("00");
            fuelMeter.style.width = new StyleLength(Length.Percent(Percentage(truck.fuel, truck.TruckDef.maxFuel)));
        }
        private void OnPlayerStart(PlayerController playerController){
            playerRB = playerController.TruckController.GetComponent<Rigidbody2D>();
            player = playerController;
            truck = playerController.TruckController;
        }
        private static float Percentage(float current, float max)
        {
            float percentage = (current/max) * 100;
            return percentage;
            //alo
        }
    }
}