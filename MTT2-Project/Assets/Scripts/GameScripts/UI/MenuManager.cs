using System;
using UnityEngine;
using UnityEngine.UIElements;
using MTT2;
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
    Button b_addon4;
    Button b_backLvl;
    Button b_backCar;
    #endregion
    enum CurrentMenu {Main, Level, Car};
    CurrentMenu m_Menu;
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
            Transitions();
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
            b_addon4 = this.Q<Button>("add4");
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
        #region Mouse Events
        b_lvl1?.RegisterCallback<MouseEnterEvent>(ev => DisplayLevel(0));
        b_lvl1?.RegisterCallback<MouseLeaveEvent>(evt => DisplayLevel(-1));
        b_lvl2?.RegisterCallback<MouseEnterEvent>(evt => DisplayLevel(1));
        b_lvl2?.RegisterCallback<MouseLeaveEvent>(evt => DisplayLevel(-1));
        b_lvl3?.RegisterCallback<MouseEnterEvent>(evt => DisplayLevel(2));
        b_lvl3?.RegisterCallback<MouseLeaveEvent>(evt => DisplayLevel(-1));
        #endregion

        s_Main.Q<VisualElement>("Logo")?.RegisterCallback<TransitionEndEvent>(TransitionEndEvent => EnableLevelMenu());
        UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }
    public void PlayClicked(){
        s_Main.Q<VisualElement>("UI-Container").AddToClassList("offsetRight");
        s_Main.Q<VisualElement>("Logo").AddToClassList("fadeOut");
    }

    public void DisplayLevel(int level)
    {
        var levelContainer = this.Q<VisualElement>("Level-Container");
        var levelLabel = levelContainer.Q<Label>("Level-Label");
        var levelIcon = levelContainer.Q<VisualElement>("Level-Texture");

        if (level == -1)
        {
            levelLabel.text = String.Empty;
            levelIcon.style.backgroundImage = null;
            return;
        }

        if (level > MTT2Application.Instance.levels.Length - 1)
            return;

        LevelDef levelDef = MTT2Application.Instance.levels[level];
        levelLabel.text = levelDef.levelName;
        levelIcon.style.backgroundImage = levelDef.levelIcon;
    }
    public void EnableHome(){
        DisableAllScreens();

        s_Main.style.display = DisplayStyle.Flex;
        s_Main.Q<VisualElement>("UI-Container").RemoveFromClassList("offsetRight");
        s_Main.Q<VisualElement>("Logo").RemoveFromClassList("fadeOut");

    }
    public void EnableLevelMenu(){
        DisableAllScreens();

        s_Level.style.display = DisplayStyle.Flex;
        s_Level.Q<VisualElement>("UI-Container").RemoveFromClassList("offsetUp");
        s_Level.Q<VisualElement>("Back-Container").RemoveFromClassList("offsetBottomLeft");
    }
    public void EnableCarMenu(){
        DisableAllScreens();

        s_Car.style.display = DisplayStyle.Flex;
        s_Car.Q<VisualElement>("UI-Container").RemoveFromClassList("offsetUp");
        s_Car.Q<VisualElement>("Back-Container").RemoveFromClassList("offsetBottomLeft");
    }
    public void DisableAllScreens(){
        Transitions();
        s_Main.style.display = DisplayStyle.None;
        s_Level.style.display = DisplayStyle.None;
        s_Car.style.display = DisplayStyle.None;
    }
    void Transitions(){
        s_Level.Q<VisualElement>("UI-Container").AddToClassList("offsetUp");
        s_Level.Q<VisualElement>("Back-Container").AddToClassList("offsetBottomLeft");

        s_Car.Q<VisualElement>("UI-Container").AddToClassList("offsetUp");
        s_Car.Q<VisualElement>("Back-Container").AddToClassList("offsetBottomLeft");
    }
    // public void LvlClicked(){
    //     s_Level.Q<VisualElement>("UI-Container").AddToClassList("offsetUp");
    //     s_Level.Q<VisualElement>("Back-Container").AddToClassList("offsetBottomLeft");
    // }
    // public void BackCarClicked(){
    //     s_Car.Q<VisualElement>("UI-Container").AddToClassList("rotateTop");
    //     s_Level.Q<VisualElement>("UI-Container").RemoveFromClassList("rotateTop");
    // }
}
