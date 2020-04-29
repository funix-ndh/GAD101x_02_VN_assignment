using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    static MenuController instance;

    [SerializeField]
    GameObject[] birds;

    [SerializeField]
    GameObject chooseBirdBtn;

    [SerializeField]
    GameObject playGameBtn;

    // 0 - green
    // 1 - red
    // 2 - blue
    int selectedBird;

    void Awake()
    {
        MakeInstance();
        chooseBirdBtn.GetComponent<Button>().onClick.AddListener(ChooseBird);
        playGameBtn.GetComponent<Button>().onClick.AddListener(PlayGame);
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void ChooseBird()
    {
        selectedBird++;
        if (selectedBird > 2)
        {
            selectedBird = 0;
        }
        switch (selectedBird)
        {
            case 0:
                birds[0].SetActive(true);
                birds[1].SetActive(false);
                birds[2].SetActive(false);
                break;
            case 1:
                birds[0].SetActive(false);
                birds[1].SetActive(true);
                birds[2].SetActive(false);
                break;
            case 2:
                birds[0].SetActive(false);
                birds[1].SetActive(false);
                birds[2].SetActive(true);
                break;
        }
    }

    void PlayGame()
    {
        SceneFader.instance.FadeIn("GamePlay");
        GameController.instance.SetSelectedBird(selectedBird);
    }
}
