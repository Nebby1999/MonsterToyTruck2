using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace MTT2
{
    public class MenuController : MonoBehaviour
    {
        public GameObject playerContainer;
        public TruckPreview TruckPreview => playerContainer.GetComponent<TruckPreview>();
        VisualElement root;
        MenuManager m_manager;
        private void Awake()
        {
            playerContainer.SetActive(false);
            root = GetComponent<UIDocument>().rootVisualElement;
            m_manager = root.Q<MenuManager>("Manager");
            m_manager.menuController = this;
            m_manager.EnableHome();
        }

        public void EnableContainer(bool val) => playerContainer.SetActive(val);
    }
}