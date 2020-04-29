using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    const string HIGH_SCORE = "HighScore";
    const string SELECTED_BIRD = "selectedBird";
    const string IS_FIRST_START = "IsFirstStart";

    private static float gameSpeed = 0f;

    public void ResetGameSpeed()
    {
        gameSpeed = 0.05f;
    }

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public void StopGame()
    {
        gameSpeed = 0;
    }

    void Awake()
    {
        MakeSingeton();
        IsFirstStart();
    }

    void MakeSingeton()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void IsFirstStart()
    {
        if (!PlayerPrefs.HasKey(IS_FIRST_START))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt(SELECTED_BIRD, 0);
            PlayerPrefs.SetInt(IS_FIRST_START, 0);
        }
    }

    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int GetHightScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }

    public void SetSelectedBird(int selectedBird)
    {
        PlayerPrefs.SetInt(SELECTED_BIRD, selectedBird);
    }

    public int GetSelectedBird()
    {
        return PlayerPrefs.GetInt(SELECTED_BIRD);
    }
}
