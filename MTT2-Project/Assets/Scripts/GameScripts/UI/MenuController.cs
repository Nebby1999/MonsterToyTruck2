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
        RichColor();
        m_manager = root.Q<MenuManager>("Manager");
        m_manager.EnableHome();
    }
    void RichColor(){
        root.Q<Label>("title").text = "<color=#0d0869>M</color><color=#9c0610>a</color><color=#92069c>i</color><color=#069c18>N</color> m<color=#069c8a>E</color><color=#e09d31>N</color><color=#6c49bf>u</color>";
        root.Q<Label>("level-title").text = "<color=#6c49bf>S</color>E<color=#4972bf>L</color><color=#bf498c>E</color><color=#22a128>C</color>T <color=#ab1120>A</color> <color=#77a647>L</color><color=#d49933>E</color>V<color=#0e998d>E</color><color=#e334cc>L</color>";
        root.Q<Label>("car-title").text = "<color=#d41c3a>M</color><color=#deb72a>O</color><color=#2b1cd4>D</color><color=#e531e8>I</color><color=#26a6ab>F</color>Y <color=#2ebf3f>U</color><color=#8a361d>R</color> <color=#2db581>C</color><color=#ad8817>A</color>R";
    }
}