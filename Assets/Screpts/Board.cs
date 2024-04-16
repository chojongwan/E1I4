using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Board : MonoBehaviour
{
    public GameObject card;
    void Start()
    {
        Debug.Log("보드");
        //Time.timeScale = 0f;
        StartCoroutine("SpawnCardsWithDelay", PlayerPrefs.GetInt("StageCard"));
    }

    IEnumerator SpawnCardsWithDelay(int cardCount)
    {
        // int형 List arr를 선언 후 cardCount만큼 List에 저장
        List<int> arr = new List<int>();
        for (int i = 0; i < cardCount; i++)
        {
            arr.Add(i);
            arr.Add(i);
        }
        // arr의 값을 랜덤으로 섞어서 배치
        arr = arr.OrderBy(x => Random.Range(0f, cardCount - 1)).ToList(); //Linq를 사용한곳
        
        for (int i = 0; i < arr.Count; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y;

            // 난이도(카드 수)에 따라서 생성되는 카드 위치 변경
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

            yield return new WaitForSeconds(0.1f); // 2초의 딜레이
        }

        Gamemanager.instance.CardCount = arr.Count;
        //Time.timeScale = 1f;
        
    }
}
