using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GamePanel : MonoBehaviour
{

    [SerializeField] private int _Score, _Healty;
    [SerializeField] private TMP_Text HealtyText;
    [SerializeField] private TMP_Text ScoreText;
    void Start()
    {


        Events.Score += Score;
        Events.Healty += Healty;
    }
    private void OnDestroy()
    {
        Events.Score -= Score;
        Events.Healty -= Healty;
    }
    public void Score(int value)
    {
        _Score += value;
        ScoreText.text = "Score : " + _Score;
    }
    public void Healty()
    {
        _Healty--;
        HealtyText.text = _Healty.ToString();
        if (_Healty <1) Events.GameFinish?.Invoke(false);
    }
  
}
