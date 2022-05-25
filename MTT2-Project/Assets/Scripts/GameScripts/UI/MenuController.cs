using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MenuController : MonoBehaviour
{
    VisualElement root;
    MenuManager m_manager;
    private void Awake()
    {

        root = GetComponent<UIDocument>().rootVisualElement;
        m_manager = root.Q<MenuManager>("Manager");
    }
    private void Start()
    {
        m_manager.EnableHome();
        // root.Q("Menu").style.display = DisplayStyle.None;
        // root.Q("CarMenu").style.display = DisplayStyle.None;
        // root.Q("LevelMenu").style.display = DisplayStyle.None;

        // root.Q("Menu").style.display = DisplayStyle.Flex;
    }
}