using System;
using UnityEngine;
using UnityEngine.UIElements;
public class MenuManager : VisualElement
{
    #region Screens
    VisualElement s_Main;
    VisualElement s_Level;
    VisualElement s_Car;
    #endregion
    #region Buttons
    Button b_play;
    Button b_exit;
    Button b_lvl1;
    Button b_lvl2;
    Button b_lvl3;
    Button b_addon1;
    Button b_addon2;
    Button b_addon3;
    Button b_backLvl;
    Button b_backCar;
    #endregion

    public new class UxmlFactory : UxmlFactory<MenuManager, UxmlTraits>{}
    public new class UxmlTraits : VisualElement.UxmlTraits{}
    public MenuManager(){
        this.RegisterCallback<AttachToPanelEvent>((x) =>
        {
            //Necesita correr en este evento para poder referenciarse desde MonoBehaviour Awake o Start
            #region ScreenSetter 
            s_Car = this.Q<VisualElement>("CarMenu");
            s_Level = this.Q<VisualElement>("LevelMenu");
            s_Main = this.Q<VisualElement>("Menu");
            #endregion
            #region ButtonSetter
            b_play = this.Q<Button>("play");
            b_exit = this.Q<Button>("exit");
            b_lvl1 = this.Q<Button>("lvl1");
            b_lvl2 = this.Q<Button>("lvl2");
            b_lvl3 = this.Q<Button>("lvl3");
            b_addon1 = this.Q<Button>("add1");
            b_addon2 = this.Q<Button>("add2");
            b_addon3 = this.Q<Button>("add3");
            b_backLvl = this.Q<Button>("backlvl");
            b_backCar = this.Q<Button>("backcar");
            #endregion

        });
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    private void OnGeometryChange(GeometryChangedEvent evt)
    {
        #region Click Events
        b_play?.RegisterCallback<ClickEvent>(ev => PlayClicked());
        b_lvl1?.RegisterCallback<ClickEvent>(ev => EnableCarMenu());
        b_lvl2?.RegisterCallback<ClickEvent>(ev => EnableCarMenu());
        b_lvl3?.RegisterCallback<ClickEvent>(ev => EnableCarMenu());
        b_backLvl?.RegisterCallback<ClickEvent>(ev => EnableHome());
        b_backCar?.RegisterCallback<ClickEvent>(ev => EnableLevelMenu());
        b_exit?.RegisterCallback<ClickEvent>(ev => UnityEditor.EditorApplication.isPlaying = false);
        #endregion
        #region Transition Events
        s_Main?.RegisterCallback<TransitionEndEvent>(ev => EnableLevelMenu());
        #endregion

        UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }
    public void PlayClicked(){
        s_Main.Q<VisualElement>("UI-Container").AddToClassList("rotateBRight");
        s_Main.Q<VisualElement>("Logo").AddToClassList("fadeOut");
    }
    public void EnableHome(){
        DisableAllScreens();

        s_Main.style.display = DisplayStyle.Flex;
    }
    public void EnableLevelMenu(){
        DisableAllScreens();

        s_Level.style.display = DisplayStyle.Flex;
    }
    public void EnableCarMenu(){
        DisableAllScreens();

        s_Car.style.display = DisplayStyle.Flex;
    }
    public void DisableAllScreens(){
        s_Main.style.display = DisplayStyle.None;
        s_Level.style.display = DisplayStyle.None;
        s_Car.style.display = DisplayStyle.None;
    }
}
