using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Board : MonoBehaviour
{
    public GameObject card;
    void Start()
    {
        Debug.Log("����");
        //Time.timeScale = 0f;
        StartCoroutine(SpawnCardsWithDelay());
    }

    IEnumerator SpawnCardsWithDelay()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray(); // Linq�� ����� ��

        for (int i = 0; i < 16; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);

            yield return new WaitForSeconds(0.5f); // 2���� ������
        }

        Gamemanager.instance.CardCount = arr.Length;
        //Time.timeScale = 1f;
        
    }
}
