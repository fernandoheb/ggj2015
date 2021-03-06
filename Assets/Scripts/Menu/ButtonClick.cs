﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[AddComponentMenu("Scripts/Menu/ButtonClick")]
public class ButtonClick : MonoBehaviour 
{
    /// <summary>
    /// Referência para o botão utilizado na UI
    /// </summary>
    public Button button;

    /// <summary>
    /// Nome do botão que vai acionar o evento de click
    /// </summary>
    public string inputName = "Attack";

    /// <summary>
    /// Cena que o botão chamara ao ser clicado
    /// </summary>
    public string nextScene;

    /// <summary>
    /// Música que será tocada ao clicar no botão
    /// </summary>
    public string nextMusic;

    /// <summary>
    /// Efeito sonoro de click no botão
    /// </summary>
    public AudioClip buttonClip;

    /// <summary>
    /// Referência pra o gerenciador de cenas
    /// </summary>
    private ScreenManager screenManager;

    private SoundFXManager soundFX;

    /// <summary>
    /// Flag que indica se o botão foi pressionado
    /// </summary>
    private bool clicked = false;

    /// <summary>
    /// Pega as referências
    /// </summary>
    void Start()
    {
        screenManager = ScreenManager.Instance;
        soundFX = SoundFXManager.Instance;
    }

    /// <summary>
    /// Verifica se a tecla foi pressionada
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown(inputName + "1") || Input.GetButtonDown(inputName + "2"))
        {
            if (!clicked)
            {
                clicked = true;
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerClickHandler);
            }
        }
    }

    /// <summary>
    /// Evento de click que deve ser usado no botão
    /// </summary>
    public void Click()
    {
        if (buttonClip)
            soundFX.PlayOneShot(buttonClip);

        screenManager.Load(nextScene, nextMusic);   
    }
}
