using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    [SerializeField]
    GameObject[] birds;

    [SerializeField]
    Button flapBtn;

    [SerializeField]
    GameObject endGameScoreBoard, endGameTextObject;

    [SerializeField]
    GameObject whiteMedal, bronzeMedal, goldMedal;

    [SerializeField]
    Text realtimeScoreTxt, scoreTxt, bestScoreTxt;

    [SerializeField]
    Button menuBtn, replayBtn, pauseBtn, guideBtn;

    public static BirdScripts activeBird;

    int score = 0;

    void Awake()
    {
        int selectedBird = GameController.instance.GetSelectedBird();
        GameObject activeBirdGameObject = birds[selectedBird];
        activeBirdGameObject.SetActive(true);

        activeBird = activeBirdGameObject.GetComponent<BirdScripts>();
        activeBird.RegisterOnEndGameCallBack(DisplayEndGameScoreBoard);
        activeBird.RegisterScoreCallBack(WinAScore);

        flapBtn.onClick.AddListener(Flap);
        menuBtn.onClick.AddListener(GoToMenu);
        replayBtn.onClick.AddListener(Replay);
        pauseBtn.onClick.AddListener(DisplayPauseGameScoreBoard);
        guideBtn.onClick.AddListener(() =>
        {
            guideBtn.gameObject.SetActive(false);
            GameController.instance.ResetGameSpeed();
            activeBird.Resume();
        });
    }

    void DisplayPauseGameScoreBoard()
    {
        activeBird.Stop();
        endGameTextObject.SetActive(false);
        DisplayEndGameScoreBoard();
        UpdateMedal(GameController.instance.GetHightScore());
        replayBtn.onClick.RemoveAllListeners();
        replayBtn.onClick.AddListener(() =>
        {
            activeBird.Resume();
            GameController.instance.ResetGameSpeed();
            endGameTextObject.SetActive(true);
            endGameScoreBoard.SetActive(false);
            replayBtn.onClick.RemoveAllListeners();
            replayBtn.onClick.AddListener(Replay);
        });
    }

    void DisplayEndGameScoreBoard()
    {
        GameController.instance.StopGame();
        endGameScoreBoard.SetActive(true);
        UpdateMedal(score);
        scoreTxt.text = score.ToString();
        GameController.instance.SetHighScore(Mathf.Max(GameController.instance.GetHightScore(), score));
        bestScoreTxt.text = GameController.instance.GetHightScore().ToString();
    }

    void WinAScore()
    {
        score++;
        realtimeScoreTxt.text = score.ToString();
    }

    void Flap()
    {
        activeBird.Jump();
    }

    void GoToMenu()
    {
        SceneFader.instance.FadeIn("MainMenu");
    }

    void Replay()
    {
        SceneFader.instance.FadeIn(Application.loadedLevelName);
    }

    void UpdateMedal(int score)
    {
        if (score <= 20)
        {
            whiteMedal.SetActive(true);
            bronzeMedal.SetActive(false);
            goldMedal.SetActive(false);
        }
        else if (score < 40)
        {
            whiteMedal.SetActive(false);
            bronzeMedal.SetActive(true);
            goldMedal.SetActive(false);
        }
        else
        {
            whiteMedal.SetActive(false);
            bronzeMedal.SetActive(false);
            goldMedal.SetActive(true);
        }
    }
}
