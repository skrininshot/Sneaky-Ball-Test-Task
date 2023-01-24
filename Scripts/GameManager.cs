using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Text headerText;
    [SerializeField] private Text durationCounterText;
    [SerializeField] private Text timesCounterText;
    [SerializeField] private Text menuTipText;
    [SerializeField] private Text inGameScore;
    [SerializeField] private Text menuScore;
    [SerializeField] private Text menuMaxScore;

    public UnityEvent StartGame;
    public UnityEvent GameEnd;
    public UnityEvent ChangeScore;

    public enum Difficult { Easy, Normal, Hard }
    public Difficult SelectedDifficult = Difficult.Normal;
    private int score = 0;
    private int secondsCounter = 0;

    private void Awake()
    {
        Instance = this;
        GameEnd.AddListener(GameOver);
        ChangeScore.AddListener(AddPoint);
    }

    private void Start()
    {
        headerText.text = "Sneaky Ball";
        durationCounterText.enabled = false;
        timesCounterText.enabled = false;
        menuTipText.text = "tap button to start";
        menuScore.enabled = false;
        UpdateMenuMaxScore();
    }

    public void Play()
    {
        score = 0;
        UpdateInGameScore();

        StartGame.Invoke();
        StartCoroutine(SessionTimer());
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("DiedTimes", PlayerPrefs.GetInt("DiedTimes", 0) + 1);
        StopCoroutine(SessionTimer());

        headerText.text = "Game Over";
        durationCounterText.enabled = true;
        durationCounterText.text = $"You lived: {SecondsToClockFace(PlayerPrefs.GetInt("Seconds", 0))}";
        timesCounterText.enabled = true;
        timesCounterText.text = $"You died {PlayerPrefs.GetInt("DiedTimes", 1)} times";
        menuTipText.text = "Choose difficult and restart";
        menuScore.enabled = true;
        UpdateMenuScore();
        UpdateMenuMaxScore();
        ClearData();
    }

    public void AddPoint()
    {
        score++;
        UpdateInGameScore();
        PlayerPrefs.SetInt("LastScore", score);

        if (score > PlayerPrefs.GetInt("MaxScore", 0))
        {
            PlayerPrefs.SetInt("MaxScore", score);
        }
    }

    private void UpdateInGameScore()
    {
        inGameScore.text = score.ToString();  
    }

    private void UpdateMenuMaxScore()
    {
        menuMaxScore.text = "Max Score: " + PlayerPrefs.GetInt("MaxScore", 0).ToString();
    }

    private void UpdateMenuScore()
    {
        menuScore.text = "Score: " + PlayerPrefs.GetInt("LastScore", 0).ToString();
    }

    private void ClearData()
    {
        secondsCounter = 0;
        score = 0;
    }

    private IEnumerator SessionTimer()
    {
        WaitForSeconds second = new (1);

        while (true)
        {
            PlayerPrefs.SetInt("Seconds", secondsCounter);
            secondsCounter++;
            yield return second;
        }
    }

    private string SecondsToClockFace(int seconds)
    {
        int minutesTimer = seconds / 60;
        int secondsTimer = seconds - (minutesTimer * 60);

        return AddZero(minutesTimer) + ":" + AddZero(secondsTimer);
    }

    private string AddZero(int digit)
    {
        return digit.ToString().Length == 1 ? "0" + digit : digit.ToString();
    }
}
