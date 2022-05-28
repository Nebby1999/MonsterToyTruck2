using System;
using UnityEngine;
using UnityEngine.UIElements;
public class HUDManager : VisualElement
{
    #region Screens
    VisualElement s_Menu;
    VisualElement s_HUD;
    #endregion
    #region Buttons
    Button b_restart;
    Button b_exit;
    Button b_pause;
    Button b_resume;
    #endregion
    public bool isPlaying;
    public new class UxmlFactory : UxmlFactory<HUDManager, UxmlTraits>{}
    public new class UxmlTraits : VisualElement.UxmlTraits{}
    public HUDManager()
    {
        this.RegisterCallback<AttachToPanelEvent>((x) =>
        {
            //Necesita correr en este evento para poder referenciarse desde MonoBehaviour Awake o Start
            #region ScreenSetter 
            s_Menu = this.Q<VisualElement>("GameMenu");
            s_HUD = this.Q<VisualElement>("HUD");
            #endregion
            #region ButtonSetter
            b_restart = this.Q<Button>("restart");
            b_exit = this.Q<Button>("exit");
            b_pause = this.Q<Button>("pause");
            b_resume = this.Q<Button>("resume");
            #endregion
        });
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }
    private void OnGeometryChange(GeometryChangedEvent evt)
    {
        
        
        //Click Events
        b_pause?.RegisterCallback<ClickEvent>(ev => EnableMenu());
        b_resume?.RegisterCallback<ClickEvent>(ev => EnableHUD());
        // b_lvl2?.RegisterCallback<ClickEvent>(ev => EnableCarMenu());
        // b_lvl3?.RegisterCallback<ClickEvent>(ev => EnableCarMenu());
        // b_exit?.RegisterCallback<ClickEvent>(ev => UnityEditor.EditorApplication.isPlaying = false);

        UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }
    public void DisableAllScreens(){
        s_HUD.style.display = DisplayStyle.None;
        s_Menu.style.display = DisplayStyle.None;
    }
    public void EnableHUD(){
        DisableAllScreens();
        b_pause.style.display = DisplayStyle.Flex;

        isPlaying = true;
        s_HUD.style.display = DisplayStyle.Flex;
    }
    public void EnableMenu(){
        // DisableAllScreens();
        b_pause.style.display = DisplayStyle.None;

        isPlaying = false;
        s_Menu.style.display = DisplayStyle.Flex;
    }
}
