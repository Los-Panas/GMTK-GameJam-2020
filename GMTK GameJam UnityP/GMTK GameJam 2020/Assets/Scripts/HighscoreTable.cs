using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    public string prefabName;

    private void Awake()
    {
        //RemoveHighscoreEntry();

        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        AddHighscoreEntry(PlayerPrefs.GetInt("FinalScore"), "AGC");

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        int scoreLimit = 1;

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            if (scoreLimit <= 10)
            {
                scoreLimit++;
                CreateHighScoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            }
        }

        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        entryTemplate.gameObject.SetActive(false);
    }

    private void CreateHighScoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTemplate.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        entryTransform.gameObject.SetActive(true);
        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);
        
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private void RemoveHighscoreEntry()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores removeHighscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < removeHighscores.highscoreEntryList.Count; i++)
        {
            removeHighscores.highscoreEntryList.Remove(removeHighscores.highscoreEntryList[i]);
        }

        string json = JsonUtility.ToJson(removeHighscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    [Serializable]
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList = new List<HighscoreEntry>();
    }

    [Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
//highscoreTable