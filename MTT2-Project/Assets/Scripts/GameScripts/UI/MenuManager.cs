using System;
using UnityEngine;
using UnityEngine.UIElements;
using MTT2;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using MTT2.Addons;
using System.Globalization;

public class MenuManager : VisualElement
{
    public MenuController menuController;
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
    Button b_truck;
    Button b_wheel;
    Button b_play_game;
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
            b_truck = this.Q<Button>("truck");
            b_wheel = this.Q<Button>("wheel");
            b_play_game = this.Q<Button>("play-game");
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
        b_lvl1?.RegisterCallback<ClickEvent>(EnableCarMenu);
        b_lvl2?.RegisterCallback<ClickEvent>(EnableCarMenu);
        b_backLvl?.RegisterCallback<ClickEvent>(ev => EnableHome());
        b_backCar?.RegisterCallback<ClickEvent>(ev => EnableLevelMenu());
        b_exit?.RegisterCallback<ClickEvent>(ev => UnityEditor.EditorApplication.isPlaying = false);
        List<VisualElement> elmnts = new List<VisualElement> { b_truck, b_wheel};
        elmnts.ForEach(ve => ve.RegisterCallback<ClickEvent>(ChangeTruckElement));
        b_play_game?.RegisterCallback<ClickEvent>(ev => {SceneManager.LoadScene(MTT2Application.Instance.GetNextLevelName());});
        #endregion
        #region Mouse Events
        b_lvl1?.RegisterCallback<MouseEnterEvent>(ev => DisplayLevel(0));
        b_lvl1?.RegisterCallback<MouseLeaveEvent>(evt => DisplayLevel(-1));
        b_lvl2?.RegisterCallback<MouseEnterEvent>(evt => DisplayLevel(1));
        b_lvl2?.RegisterCallback<MouseLeaveEvent>(evt => DisplayLevel(-1));
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
    public void ChangeTruckElement(ClickEvent clickEvent)
    {
        VisualElement target = (VisualElement)clickEvent.target;
        switch(target.name)
        {
            case "truck":
                {
                    menuController.TruckPreview.ChangeTruck();
                    break;
                }
            case "wheel":
                {
                    menuController.TruckPreview.ChangeWheels();
                    break;
                }
            case "add1":
                {
                    menuController.TruckPreview.ChangeAddon(AddonLocator.AddonLocation.Top);
                    break;
                }
            case "add2":
                {
                    menuController.TruckPreview.ChangeAddon(AddonLocator.AddonLocation.Back);
                    break;
                }
            case "add3":
                {

                    break;
                }
            case "add4":
                {

                    break;
                }
        }
    }
    public void EnableCarMenu(ClickEvent clickEvent){

        string veName = (clickEvent.target as VisualElement).name;
        int lvlIndex = int.Parse(veName.Substring("lvl".Length), CultureInfo.InvariantCulture) - 1;
        MTT2Application.Instance.SetNextLevel(lvlIndex);

        DisableAllScreens();

        s_Car.style.display = DisplayStyle.Flex;
        s_Car.Q<VisualElement>("UI-Container").RemoveFromClassList("offsetUp");
        s_Car.Q<VisualElement>("Back-Container").RemoveFromClassList("offsetBottomLeft");
        s_Car.Q<VisualElement>("Play-Container").RemoveFromClassList("offsetBottomRight");
        menuController.EnableContainer(true);
        
    }
    public void DisableAllScreens(){
        Transitions();
        s_Main.style.display = DisplayStyle.None;
        s_Level.style.display = DisplayStyle.None;
        s_Car.style.display = DisplayStyle.None;
        menuController.EnableContainer(false);
    }
    void Transitions(){
        s_Level.Q<VisualElement>("UI-Container").AddToClassList("offsetUp");
        s_Level.Q<VisualElement>("Back-Container").AddToClassList("offsetBottomLeft");

        s_Car.Q<VisualElement>("UI-Container").AddToClassList("offsetUp");
        s_Car.Q<VisualElement>("Back-Container").AddToClassList("offsetBottomLeft");
        s_Car.Q<VisualElement>("Play-Container").AddToClassList("offsetBottomRight");
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
