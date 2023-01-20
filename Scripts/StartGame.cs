using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private enum Difficult { Easy, Normal, Hard }
    private Difficult selectedDifficult;
    [SerializeField] private Text selectedDifficultText;

    private void Start()
    {
        selectedDifficult = (Difficult)PlayerPrefs.GetInt("Difficult", 1);
        selectedDifficultText.text = selectedDifficult.ToString();
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
        PlayerPrefs.SetInt("Difficult", (int)selectedDifficult);
        SceneManager.LoadScene("Game");
    }

    private void ChangeSelectedDifficult(Difficult difficult)
    {
        selectedDifficult = difficult;
        selectedDifficultText.text = difficult.ToString();
    }
}
