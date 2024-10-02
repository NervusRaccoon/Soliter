using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _playPanel;

    private void Start()
    {
        ShowPlayPanel();
        HideWinPanel();
        Game.Card.OnLevelFinished += ShowWinPanel;
    }

    private void OnDestroy()
    {
        if (Game.Card != null)
        {
            Game.Card.OnLevelFinished -= ShowWinPanel;
        }
    }

    public void RestartGame()
    {
        Game.Card.CreateCardPacks();
    }

    public void StartNewLevel()
    {
        Game.Card.GenerateLevel();
        HideWinPanel();
    }

    public void StartGame()
    {
        Game.Card.GenerateLevel();
        HidePlayPanel();
    }

    public void ShowWinPanel()
    {
        _winPanel.SetActive(true);
    }

    public void HideWinPanel()
    {
        _winPanel.SetActive(false);
    }

    public void ShowPlayPanel()
    {
        _playPanel.SetActive(true);
    }

    public void HidePlayPanel()
    {
        _playPanel.SetActive(false);
    }
}
