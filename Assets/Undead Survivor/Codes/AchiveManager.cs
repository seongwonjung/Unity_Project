using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;

    enum Achive { unlockPotato, unlockBean}
    Achive[] achives;
    WaitForSecondsRealtime wait;
    void Awake()
    {
        achives = (Achive[]) Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5);
        if (!PlayerPrefs.HasKey("myData"))
            Init();
    }

    void Init()
    {
        PlayerPrefs.SetInt("myData", 1);
        foreach(Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }
    void Start()
    {
        UnlockCharacter();
    }
    void UnlockCharacter()
    {
        for(int index = 0; index < lockCharacter.Length; index++)
        {
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach(Achive ahive in achives)
        {
            CheckAchive(ahive); 
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.unlockPotato:
                isAchive = GameManager.Instance.kill >= 10;
                break;

            case Achive.unlockBean:
                isAchive = GameManager.Instance.gameTime == GameManager.Instance.maxGameTime;
                break;

        }
        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for(int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }
            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        AudioManager.instance.Playsfx(AudioManager.Sfx.LevelUp);
        yield return wait;
        uiNotice.SetActive(false);
    }
}
