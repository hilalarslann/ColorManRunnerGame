using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI ScoreText;

    public Material[] ColorMaterials;
    public Button[] ColorButtons;

    public int maxScore;
    public bool endGame;

    public GameObject startButton;

    [HideInInspector]
    public bool start;
    private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            ScoreText.text = score.ToString();
        }
    }

    private void Awake()
    {
        Instance = this;
        start = false;
    }
    public void Play()
    {
        start = true;
        startButton.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
