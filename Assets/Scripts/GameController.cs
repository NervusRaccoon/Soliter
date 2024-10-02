using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;

    private void Start()
    {
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
        _winPanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        _winPanel.SetActive(true);
    }

    public void HideWinPanel()
    {
        _winPanel.SetActive(false);
    }
}
