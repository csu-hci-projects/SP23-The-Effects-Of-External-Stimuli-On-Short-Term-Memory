using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //Key: 0 = nothing, 1 = sound, 2 = buzz, 3 = sound & buzz
    private static int[] order1 = { 0, 1, 2, 3 };
    private static int[] order2 = { 1, 2, 3, 0 };
    private static int[] order3 = { 2, 3, 0, 1 };
    private static int[] order4 = { 3, 0, 1, 2 };

    private int totalRoundCounter = 0;
    private int roundCounter = 0;

    //Here is where you change which order the participant will be using
    private int[] roundSettings;
    private int roundNumber;

    private List<int> tasks = new List<int>();
    private List<int> sequence = new List<int>();

    private bool soundActive;
    private bool buzzActive;

    public List<AudioClip> sounds = new List<AudioClip>();
    public List<List<Color32>> colors = new List<List<Color32>>();

    public List<Button> clickableButtons;

    public AudioSource audioSource;

    public CanvasGroup buttons;

    public GameObject startButton;
    public GameObject nextRoundButton;

    public static GameManager instance;

    public void Awake()
    {
        nextRoundButton.SetActive(false);

        instance = this;

        roundSettings = order3;
        roundNumber = roundSettings[0];

        DataToFile.instance.CreateFile("order3");

        colors.Add(new List<Color32> { new Color32(255, 100, 100, 255), new Color32(255, 0, 0, 255) }); // red
        colors.Add(new List<Color32> { new Color32(255, 187, 109, 255), new Color32(255, 136, 0, 255) }); //yellow
        colors.Add(new List<Color32> { new Color32(162, 255, 124, 255), new Color32(72, 248, 0, 255) }); //green
        colors.Add(new List<Color32> { new Color32(57, 111, 255, 255), new Color32(0, 70, 255, 255) }); // blue

        for (int i = 0; i < 4; i++)
        {
            clickableButtons[i].GetComponent<Image>().color = colors[i][0];
        }
    }

    public void addToSequence(int buttonID)
    {
        sequence.Add(buttonID);
        StartCoroutine(highlightButton(buttonID));
        for (int i = 0; i < sequence.Count; i++)
        {
            if (tasks[i] != sequence[i])
            {
                StartCoroutine(lostRound());
                return;
            }
        }

        if (sequence.Count == tasks.Count)
        {
            ScoreManager.instance.addPoint();
            StartCoroutine(nextRound());
        }
        else
        {
            return;
        }
    }

    public void StartGame()
    {
        StartCoroutine(nextRound());
        startButton.SetActive(false);

    }

    public void nextRoundButtonClick()
    {
        StartCoroutine(nextRound());
        nextRoundButton.SetActive(false);
        ScoreManager.instance.clearPoints();
    }

    public void roundNumberCheck(int roundNumber)
    {
        switch (roundNumber)
        {
            case 0:
                soundActive = false;
                buzzActive = false;
                break;
            case 1:
                soundActive = true;
                buzzActive = false;
                break;
            case 2:
                soundActive = false;
                buzzActive = true;
                break;
            case 3:
                soundActive = true;
                buzzActive = true;
                break;

        }
    }

    public IEnumerator lostRound()
    {
        sequence.Clear();
        tasks.Clear();

        DataToFile.instance.AppendToFile(totalRoundCounter, roundSettings[totalRoundCounter / 3]);

        roundCounter++;
        totalRoundCounter++;

        if (roundCounter == 3)
        {
            roundCounter = 0;
            roundNumber = roundSettings[totalRoundCounter / 3];
        }


        yield return new WaitForSeconds(2f);

        nextRoundButton.SetActive(true);

        // startButton.SetActive(true);
    }


    public IEnumerator highlightButton(int buttonID)
    {
        clickableButtons[buttonID].GetComponent<Image>().color = colors[buttonID][1];
        if (soundActive)
        {
            audioSource.PlayOneShot(sounds[buttonID]);
        }
        if (buzzActive)
        {
            RumbleManager.instance.RumblePulse(0.25f, 1f, 0.25f);
        }
        yield return new WaitForSeconds(0.5f);//time button is highlighted
        clickableButtons[buttonID].GetComponent<Image>().color = colors[buttonID][0];
        yield return new WaitForSeconds(0.5f);//time button is highlighted
    }


    public IEnumerator nextRound()
    {
        sequence.Clear();
        roundNumberCheck(roundNumber);
        buttons.interactable = false;

        yield return new WaitForSeconds(1f);

        tasks.Add(Random.Range(0, 4));

        foreach (int index in tasks)
        {
            yield return StartCoroutine(highlightButton(index));
        }

        buttons.interactable = true;

        yield return null;
    }

}
