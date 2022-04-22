using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private GameObject _player;
    private Camera _camera;

    public Text bananaCount;
    public Text finishMenuBananaCount;
    public Text finishMenuTimer;
    public GameObject finishMenu;
    

    private void Awake()
    {
        instance = this;
        _player = GameObject.FindGameObjectWithTag("Player");
        _camera = Camera.main;
    }

    public void GameOver()
    {
        _camera.GetComponent<FollowHero>().enabled = false;
    }
    
    public void Restart()
    {
        _camera.GetComponent<FollowHero>().enabled = true;
        if (PlayerController.instance.checkpointTest)
        {
            _player.transform.position = PlayerController.instance.checkpoint.transform.position;
        }
        else
        {
            _player.transform.position = PlayerController.instance.spawnPoint.transform.position;
        }
        _player.gameObject.SetActive(true);
    }
    
    public void AddBanana()
    {
        bananaCount.text = (int.Parse(bananaCount.text) + 1).ToString();
    }
    
    public void ResetBananas()
    {
        bananaCount.text = "0";
    }

    public void FinishGame()
    {
        Timer.instance.gameObject.SetActive(false);
        finishMenu.SetActive(true);
        finishMenuBananaCount.text = bananaCount.text;
        finishMenuTimer.text = Timer.instance.timerText.text;
        PlayerController.instance.GetComponent<PlayerController>().enabled = false;
    }

    public void RestartGame()
    {
        finishMenu.SetActive(false);
        PlayerController.instance.GetComponent<PlayerController>().enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
