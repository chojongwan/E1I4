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
        StartCoroutine("SpawnCardsWithDelay", PlayerPrefs.GetInt("StageCard"));
    }

    IEnumerator SpawnCardsWithDelay(int cardCount)
    {
        // int�� List arr�� ���� �� cardCount��ŭ List�� ����
        List<int> arr = new List<int>();
        for (int i = 0; i < cardCount; i++)
        {
            arr.Add(i);
            arr.Add(i);
        }
        // arr�� ���� �������� ��� ��ġ
        arr = arr.OrderBy(x => Random.Range(0f, cardCount - 1)).ToList(); //Linq�� ����Ѱ�
        
        for (int i = 0; i < arr.Count; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y;

            // ���̵�(ī�� ��)�� ���� �����Ǵ� ī�� ��ġ ����
            if (cardCount == 6)
            {
                y = (i / 4) * 1.4f - 1.8f;
            }
            else if (cardCount == 8)
            {
                y = (i / 4) * 1.4f - 3.0f;
            }
            else
            {
                y = (i / 4) * 1.4f - 3.8f;
            }

            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);

            yield return new WaitForSeconds(0.1f); // 2���� ������
        }

        Gamemanager.instance.CardCount = arr.Count;
        //Time.timeScale = 1f;
        
    }
}
