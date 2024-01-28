using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {
        public string name;
        public string description;
        public float reward;
        public string questCompleted;
        public string questInProgress;
        public string promptBotIsCompleted;
    }

    public Quest[] quests;
    private int idQuest = 0;
    private int stateQuest = 1;
    public HeroKnight player;
    public GameObject windowsHints;

    public TMP_Text botText;
    public TMP_Text questText;
    public TMP_Text questName;

    public void SetQuest()
    {
        Quest quest = quests[idQuest];

        if (stateQuest == 3)
        {
            player.AddMoney(quest.reward);
            stateQuest = 1;
            AddIDQuest();
        }

        if (idQuest == 1)
        {
            botText.text = "Продолжение следует. Бывай молодой...";
            windowsHints.SetActive(false);
            return;
        }

        stateQuest = 2;
        windowsHints.SetActive(true);
        questName.text = quests[idQuest].name;
        questText.text = quests[idQuest].description;
        botText.text = quests[idQuest].questInProgress;
    }

    public void AddIDQuest()
    {
        idQuest++;
    }

    public void QuestCompeted()
    {
        questText.text = quests[idQuest].questCompleted;
        botText.text = quests[idQuest].promptBotIsCompleted;
        stateQuest = 3;
    }

    public string GetPrompt()
    {
        return quests[idQuest].promptBotIsCompleted;
    }

    public int GetStateQuest()
    {
        return stateQuest;
    }
}

