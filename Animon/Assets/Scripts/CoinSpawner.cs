using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinfPrefab;
    public float spawnRateMin = 10f; // 최소 생성 주기
    public float spawnRateMax = 30f; // 최대 생성 주기
    public float spawnMinPos = -9;
    public float spawnMaxPos = 9;

    private float spawnRate; // 생성 주기
    private float timeAfterSpawn; // 최근 생성 시점에서 지난 시간

    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        // 최근 생성 이후의 누적 시간을 0으로 초기화
        timeAfterSpawn = 0f;
        // 총알 생성 간격을 spawnRateMin과 spawnRateMax 사이에서 랜덤 지정 
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        // PlayerController 컴포넌트를 가진 게임 오브젝트를 찾아 조준 대상으로 설정

        time = 0;
        transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        // timeAfterSpawn을 갱신
        timeAfterSpawn += Time.deltaTime;

        // 최근 생성 시점에서부터 누적된 시간이, 생성 주기보다 크거나 같다면
        if (timeAfterSpawn >= spawnRate)
        {
            // 누적된 시간을 리셋
            timeAfterSpawn = 0f;
                        
            GameObject bullet = Instantiate(coinfPrefab,
                randPosition(), transform.rotation);            
            

            // 다음번 생성 간격을 spawnRateMin, spawnRateMax 사이에서 랜덤 지정
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);

            transform.localScale = Vector3.one * (1 - time);
            if (time > 1f)
            {
                time = 0;
                gameObject.SetActive(false);
            }
            time += Time.deltaTime;
        }
    }

    Vector3 randPosition()
    {
        float xPos = Random.Range(spawnMinPos, spawnMaxPos);
        float zPos = Random.Range(spawnMinPos, spawnMaxPos);

        Vector3 rndPos = new Vector3(xPos, 1, zPos);
        return rndPos;
    }
}
