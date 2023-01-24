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

    public UnityEvent StartGame;
    public UnityEvent GameEnd;

    public UnityEvent ChangeScore;
    public enum Difficult { Easy, Normal, Hard }
    public Difficult SelectedDifficult;
    private int score = 0;

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
        menuTipText.text = "Choose difficult and start";
    }

    public void SelectEasy()
    {
        StartInSelectedDifficult(Difficult.Easy);
    }

    public void SelectNormal()
    {
        StartInSelectedDifficult(Difficult.Normal);
    }

    public void SelectHard()
    {
        StartInSelectedDifficult(Difficult.Hard);
    }

    private void StartInSelectedDifficult(Difficult difficult)
    {
        score = 0;
        UpdateScore();

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

        score = 0;
        UpdateScore();
    }

    public void AddPoint()
    {
        score++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        inGameScore.text = score.ToString();
    }

    private IEnumerator SessionTimer()
    {
        WaitForSeconds second = new (1);
        int secondsCounter = 0;

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
