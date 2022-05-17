using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace MTT2.HUD{
public class HUD : MonoBehaviour{

    private SceneController map;
    private PlayerController player;
    private TruckController truck;
    private Rigidbody2D playerRB;
    public Label speedMeter;
    public VisualElement fuelMeter;
    public VisualElement speedSymbol;
        private void Awake()
        {
            PlayerController.OnPlayerStart += OnPlayerStart;
        }
        void Start()
        {
            map = SceneController.Instance;
            HUDVariables();
        }
        void HUDVariables()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            speedMeter = root.Q<Label>("SpeedCurrent");
            fuelMeter = root.Q("FuelLevel");
            speedSymbol = root.Q("Lord");
        }
        private void OnPlayerStart(PlayerController playerController)
        {
            playerRB = playerController.TruckController.GetComponent<Rigidbody2D>();
            player = playerController;
            truck = playerController.TruckController;
        }
        void Update()
        {
            Header();
        }
        void Header()
        {
            speedMeter.text = Mathf.Abs(playerRB.velocity.x).ToString("00");
            speedSymbol.style.width = new StyleLength(Length.Percent(InversePercentage(playerRB.velocity.x, 10)));
            
            fuelMeter.style.height = new StyleLength(Length.Percent(Percentage(truck.fuel, truck.TruckDef.maxFuel)));
        }
        void Footer()
        {
            
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
        private void OnDestroy()
        {
            PlayerController.OnPlayerStart -= OnPlayerStart;
        }
    }
}