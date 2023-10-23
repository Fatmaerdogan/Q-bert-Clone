using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text GameFinishText;
    [SerializeField] private GameObject FinishPanel;

    private void Start()
    {
        Events.GameFinish += GameFinishTextWrite;
    }
    private void OnDestroy()
    {
        Events.GameFinish -= GameFinishTextWrite;
    }
    public void GameFinishTextWrite(bool Temp)
    {
        FinishPanel.SetActive(true);
        if (Temp) GameFinishText.text = "Game Win";
        else GameFinishText.text = "Game Lose";
    }
    public void SceneLoadButton() => SceneManager.LoadScene(0);
}
