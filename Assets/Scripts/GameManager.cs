using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  private List<int> tasks = new List<int>();
  private List<int> sequence = new List<int>();

  public List<List<Color32>> colors = new List<List<Color32>>();

  public List<Button> clickableButtons;

  // public AudioClip loseSound;
  // public AudioSource audioSource;
  // public List<AudioClip> sounds = new List<AudioClip>();

  public CanvasGroup buttons;

  public GameObject startButton;
  public GameObject nextRoundButton;

  public void Awake(){
    nextRoundButton.SetActive(false);

    colors.Add(new List<Color32> {new Color32(255, 100, 100, 255), new Color32(255, 0, 0 , 255)}); // red
    colors.Add(new List<Color32> {new Color32(255, 187, 109, 255), new Color32(255, 136, 0, 255)}); //yellow
    colors.Add(new List<Color32> {new Color32(162, 255, 124, 255), new Color32(72, 248, 0, 255)}); //green
    colors.Add(new List<Color32> {new Color32(57, 111, 255, 255), new Color32(0, 70, 255, 255)}); // blue

    for(int i = 0; i < 4; i++){
      clickableButtons[i].GetComponent<Image>().color = colors[i][0];
    }
  }

  public void addToSequence(int buttonID){
    sequence.Add(buttonID);
    StartCoroutine(highlightButton(buttonID));
    for (int i = 0; i < sequence.Count; i++){
      if (tasks[i] != sequence[i]){
        StartCoroutine(lostRound());
        return;
      }
    }

    if(sequence.Count == tasks.Count){
      StartCoroutine(nextRound());
    }
    else{
      return;
    }
  }

  public void StartGame(){
    StartCoroutine(nextRound());
    startButton.SetActive(false);
  }

  public void nextRoundButtonClick(){
    StartCoroutine(nextRound());
    nextRoundButton.SetActive(false);
  }

  public IEnumerator lostRound(){
    sequence.Clear();
    tasks.Clear();
    yield return new WaitForSeconds(2f);

    nextRoundButton.SetActive(true);

    // startButton.SetActive(true);
  }

  public IEnumerator highlightButton(int buttonID){
    clickableButtons[buttonID].GetComponent<Image>().color = colors[buttonID][1];
    // audioSource.PlayOneShot(soundsList[buttonID]);
    yield return new WaitForSeconds(0.5f);//time button is highlighted
    clickableButtons[buttonID].GetComponent<Image>().color = colors[buttonID][0];
  }

  public IEnumerator nextRound(){
    sequence.Clear();
    buttons.interactable = false;

    yield return new WaitForSeconds(1f);

    tasks.Add(Random.Range(0,4));

    foreach(int index in tasks){
      yield return StartCoroutine(highlightButton(index));
    }

    buttons.interactable = true;

    yield return null;
  }

}
