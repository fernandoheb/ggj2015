﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[AddComponentMenu("Scripts/Game/PlayerSelect")]
public class PlayerSelect : MonoBehaviour
{
    public Transform[] players;
    public string nameBackScene = "Screen_Menu";
    public string nameNextScene = "Level_01";
    public string nameMusicToNextLevel = "Level1";
    public Button backButton;
    public Button nextButton;
    public AudioClip moveClip;
    public AudioClip confirmClip;

    private Vector3[] positions;
    private int[] playerIndex = { 0, 1 };
    private int index = 0;
    private bool selected = false;

    private float move;
    private float lastMove;
    private SoundFXManager soundFX;
    private ScreenManager screenManager;
    
    void Start()
    {
        screenManager = ScreenManager.Instance;
        soundFX = SoundFXManager.Instance;

        positions = new Vector3[players.Length];
        for (int i = 0; i < players.Length; i++)
            positions[i] = players[i].position;
    }

    void Update()
    {
        if (!selected)
        {
            move = Input.GetAxisRaw("Horizontal1") + Input.GetAxisRaw("Horizontal2");

            if (lastMove == 0.0f && move < 0)
                SwapIndex();

            if (lastMove == 0.0f && move > 0)
                SwapIndex();

            lastMove = move;

            players[0].position = positions[playerIndex[0]];
            players[1].position = positions[playerIndex[1]];

            var pointer = new PointerEventData(EventSystem.current);
                
            if (Input.GetButtonDown("Attack1") || Input.GetButtonDown("Attack2"))
                    ExecuteEvents.Execute(nextButton.gameObject, pointer, ExecuteEvents.pointerClickHandler);

            if (Input.GetButtonDown("Cancel1") || Input.GetButtonDown("Cancel2"))
                ExecuteEvents.Execute(backButton.gameObject, pointer, ExecuteEvents.pointerClickHandler);
 
        }
    }

    void SwapIndex()
    {
        playerIndex[index] = 1;
        index = (++index) % players.Length;
        playerIndex[index] = 0;

        soundFX.PlayOneShot(moveClip);
    }

    public void BackScene()
    {
        selected = true;

        soundFX.PlayOneShot(confirmClip);

        screenManager.Load(nameBackScene);
    }

    public void NextScene()
    {
        selected = true;

        soundFX.PlayOneShot(confirmClip);

        PlayerPrefs.SetInt(PlayerType.Kimaro.ToString(), playerIndex[0] + 1);
        PlayerPrefs.SetInt(PlayerType.Yeti.ToString(), playerIndex[1] + 1);
        PlayerPrefs.Save();

        screenManager.Load(nameNextScene, nameMusicToNextLevel);
    }
}