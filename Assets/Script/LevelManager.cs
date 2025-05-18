using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelView view;

    private LevelData levelData;
    private string[] pages;
    private float cooldown = 60f;
    private float timer = 0f;
    private bool canUnlock = true;
    private int currentPage = 0;

    private void Start()
    {
        levelData = new LevelData();
        LoadPages();
        UpdatePage();
        view.ToggleBook(false);
        view.ToggleHint(false);
        view.ToggleEnd(false);
        LoadHints();
    }

    public void OnHints()
    {
        view.ToggleHint(true);
    }

    public void CloseHint()
    {
        view.ToggleHint(false);
    }

    public void CloseInfo()
    {
        view.ToggleBook(false);
    }
    private void LoadPages()
    {
        pages = new string[4];
        for (int i = 0; i < 4; i++)
        {
            pages[i] = Resources.Load<TextAsset>($"Info{i}")?.text ?? $"Missing Info{i}";
        }
    }

    private void LoadHints()
    {
        int level = levelData.CurrentLevel;
        int part = levelData.Progress;

        for (int i = 0; i < view.hints.Length; i++)
        {
            string filename = $"Hint.{level}.{part}.#{i}";
            var textAsset = Resources.Load<TextAsset>(filename);
            if (textAsset != null)
            {
                view.SetHint(i, textAsset.text);
                view.SetLock(i, true);
            }
        }
        view.SetTimerText("Ви можете використати підказку");
        canUnlock = true;
    }

    public void OnReadInformation()
    {
        UpdatePage();
        view.ToggleBook(true);
    }

    public void OnPrev()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    public void OnNext()
    {
        if (currentPage < pages.Length - 2)
        {
            currentPage++;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        string left = pages[currentPage];
        string right = (currentPage + 1 < pages.Length) ? pages[currentPage + 1] : "";
        view.ShowPage(left, right, currentPage > 0, currentPage < pages.Length - 2);
    }

    public void OnUnlockHint(int index)
    {
        if (canUnlock)
        {
            view.SetLock(index, false);
            canUnlock = false;
            timer = 0f;
        }
    }

    public void OnClickToPass()
    {
        levelData.PassLevel();
        LoadHints();
        if (levelData.Progress > 3)
            view.ToggleEnd(true);
    }

    private void Update()
    {
        if (!canUnlock)
        {
            timer += Time.deltaTime;
            float remain = Mathf.Clamp(cooldown - timer, 0f, cooldown);
            view.SetTimerText($"Наступна підказка через: {Mathf.CeilToInt(remain)} сек");

            if (timer >= cooldown)
            {
                canUnlock = true;
                timer = 0f;
                view.SetTimerText("Підказка доступна!");
            }
        }
    }
}

