using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;


    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button InteractAlternatButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button GamepadInteractButton;
    [SerializeField] private Button GamepadInteractAlternateButton;
    [SerializeField] private Button GamepadPauseButton;
    
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI InteractText;
    [SerializeField] private TextMeshProUGUI InteractAlternateText;
    [SerializeField] private TextMeshProUGUI PauseText;
    [SerializeField] private TextMeshProUGUI GamepadInteractButtonText;
    [SerializeField] private TextMeshProUGUI GamepadInteractAlternateButtonText;
    [SerializeField] private TextMeshProUGUI GamepadPausebuttonText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();

        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        mainButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
            
        });

        moveUpButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Move_Up);});
        moveDownButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Move_Down);});
        moveLeftButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Move_Left);});
        moveRightButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Move_Right);});
        InteractButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Interact);});
        InteractAlternatButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Interact_Alternate);});
        PauseButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Pause);});
        GamepadInteractButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Gamepad_Interact);});
        GamepadInteractAlternateButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);});
        GamepadPauseButton.onClick.AddListener(() =>{ RebindBinding(GameInput.Binding.Gamepad_Pause);});

    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);


        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        InteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        InteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        PauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        GamepadInteractButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        GamepadInteractAlternateButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        GamepadPausebuttonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);

    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);

        soundEffectsButton.Select();

    }
    public void Hide()
    {
        gameObject.SetActive(false);

    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        }
        );



    }
}
