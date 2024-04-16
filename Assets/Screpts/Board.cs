using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    public GameObject card;

    void Start()
    {
        int cardCount = PlayerPrefs.GetInt("StageCard");
        StartCoroutine(SpawnCardsWithDelay(cardCount));
    }

    IEnumerator SpawnCardsWithDelay(int cardCount)
    {
        List<int> arr = RandomArray(cardCount);

        for (int i = 0; i < arr.Count; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = CalculateYPosition(i, cardCount);

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);

            yield return new WaitForSeconds(0.1f);
        }

        Gamemanager.instance.CardCount = arr.Count;
    }

    List<int> RandomArray(int count)
    {
        List<int> arr = new List<int>();
        for (int i = 0; i < count; i++)
        {
            arr.Add(i);
            arr.Add(i);
        }

        for (int i = arr.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        return arr;
    }

    float CalculateYPosition(int index, int cardCount)
    {
        float y;
        switch (cardCount)
        {
            case 6:
                y = (index / 4) * 1.4f - 1.8f;
                break;
            case 8:
                y = (index / 4) * 1.4f - 3.0f;
                break;
            default:
                y = (index / 4) * 1.4f - 3.8f;
                break;
        }
        return y;
    }
}
