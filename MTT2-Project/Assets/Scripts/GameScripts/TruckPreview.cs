using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MTT2
{
    public class TruckPreview : MonoBehaviour
    {
        public Transform spawnPoint;
        private PlayerPreferenceData prefData;
        private TruckController truckController;
        private GameObject truck;
        public void OnEnable()
        {
            prefData = MTT2Application.Instance.playerPreferenceData;
            SpawnTruck();
        }

        private void SpawnTruck()
        {
            truck = Instantiate(prefData.truckDef.truckPrefab, spawnPoint.position, Quaternion.Euler(spawnPoint.eulerAngles));
            truckController = truck.GetComponent<TruckController>();
            truckController.TruckDef = prefData.truckDef;
            truckController.WheelDef = prefData.wheelDef;
        }

        public void ChangeWheels()
        {
            truckController.WheelDef = MTT2Application.Instance.GetNextWheelDef();
        }

        public void ChangeTruck()
        {
            MTT2Application.Instance.GetNextTruckDef();
            Destroy(truck);
            SpawnTruck();
        }

        public void OnDisable()
        {
            Destroy(truck);
        }
    }
}
