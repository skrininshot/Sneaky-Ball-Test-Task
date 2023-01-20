using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Text selectedDifficultText;
    [SerializeField] private Text headerText;
    [SerializeField] private Text startButtonText;
    [SerializeField] private Text durationCounterText;
    [SerializeField] private Text timesCounterText;
    [SerializeField] private Text menuTipText;

    public UnityEvent startGame;
    public UnityEvent gameOver;
    private enum Difficult { Easy, Normal, Hard }
    private Difficult selectedDifficult;

    private void Awake()
    {
        gameOver.AddListener(GameOver);
    }

    private void Start()
    {
        selectedDifficult = (Difficult)PlayerPrefs.GetInt("Difficult", 1);
        selectedDifficultText.text = selectedDifficult.ToString();

        if (PlayerPrefs.GetInt("GameOver", 0) == 0)
        {
            headerText.text = "Sneaky Ball";
            startButtonText.text = "Start";
            durationCounterText.enabled = false;
            timesCounterText.enabled = false;
            menuTipText.text = "Choose difficult and start";
        }
        else
        {
            headerText.text = "Game Over";
            startButtonText.text = "Restart";
            durationCounterText.enabled = true;
            durationCounterText.text = $"You lived: {SecondsToClockFace(PlayerPrefs.GetInt("Seconds",0))}";
            timesCounterText.enabled = true;
            timesCounterText.text = $"You died {PlayerPrefs.GetInt("DiedTimes",1)} times";
            menuTipText.text = "Choose difficult and restart";
        }
    }

    public void SelectEasy()
    {
        ChangeSelectedDifficult(Difficult.Easy);
    }

    public void SelectNormal()
    {
        ChangeSelectedDifficult(Difficult.Normal);
    }

    public void SelectHard()
    {
        ChangeSelectedDifficult(Difficult.Hard);
    }

    public void StartInSelectedDifficult()
    {
        startGame.Invoke();
        Time.timeScale = 1;
        StartCoroutine(SessionTimer());
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("GameOver", 1);
        PlayerPrefs.SetInt("DiedTimes", PlayerPrefs.GetInt("DiedTimes", 0) + 1);
        StopCoroutine(SessionTimer());
    }

    private void ChangeSelectedDifficult(Difficult difficult)
    {
        selectedDifficult = difficult;
        selectedDifficultText.text = difficult.ToString();
        PlayerPrefs.SetInt("Difficult", (int)selectedDifficult);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("GameOver", 0);
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
