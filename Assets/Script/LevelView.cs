using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using NUnit;

public class LevelView : MonoBehaviour
{
    public Text pageTextLeft;
    public Text pageTextRight;
    public Button prevButton;
    public Button nextButton;
    public GameObject book;
    public GameObject hint;
    public Text[] hints;
    public GameObject[] locks;
    public GameObject end;
    public Text timerText;

    public void ShowPage(string leftText, string rightText, bool canGoPrev, bool canGoNext)
    {
        pageTextLeft.text = leftText;
        pageTextRight.text = rightText;
        prevButton.interactable = canGoPrev;
        nextButton.interactable = canGoNext;
    }

    public void ToggleBook(bool active) => book.SetActive(active);
    public void ToggleHint(bool active) => hint.SetActive(active);
    public void ToggleEnd(bool active) => end.SetActive(active);
    public void SetHint(int index, string text) => hints[index].text = text;
    public void SetLock(int index, bool state) => locks[index].SetActive(state);
    public void SetTimerText(string text) => timerText.text = text;
}
