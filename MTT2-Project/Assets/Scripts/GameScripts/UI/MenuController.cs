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
        m_manager.EnableHome();
    }
}