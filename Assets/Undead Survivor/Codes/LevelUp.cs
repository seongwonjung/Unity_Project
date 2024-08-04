using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.Instance.Stop();
        AudioManager.instance.Playsfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();
        AudioManager.instance.Playsfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }
    public void Select(int index)
    {
        items[index].OnClick();
    }
    void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }
        int[] ren = new int[3];
        while (true)
        {
            ren[0] = Random.Range(0, items.Length);
            ren[1] = Random.Range(0, items.Length);
            ren[2] = Random.Range(0, items.Length);

            if (ren[0]!=ren[1] && ren[1]!=ren[2] && ren[0] != ren[2])
                break;
        }

        for (int index = 0; index < ren.Length; index++){
            Item renItem = items[ren[index]];
            if(renItem.level == renItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
            {
                renItem.gameObject.SetActive(true);
            }
        }
    }
}
