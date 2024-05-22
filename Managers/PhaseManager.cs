using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhaseManager : MonoBehaviour
{
    [SerializeField] private List<EnemyPhase> defaultPhases;
    [SerializeField] private Phase MidPhase;
    [SerializeField] private BossPhase BossPhase;
    [SerializeField] private float startScale;
    [SerializeField] private float endScale;
    [SerializeField] private float scaleUpDelay;
    
    // 맨 처음 Phase는 열어두고 시작
    private int curPhaseIdx = 0;
    private void Start()
    {
        OpenPhase(curPhaseIdx);
    }

    public void CheckClear()
    {
        // defaultPhase일 때
        if(curPhaseIdx < defaultPhases.Count)
        {
            // Phase내의 모든 몬스터체크
            foreach(GameObject obj in defaultPhases[curPhaseIdx].Enemys)
            {
                // 하나라도 살아있는 obj가 있다면 pass
                if (obj.activeSelf) 
                {
                    Debug.Log("아직 살아있는 기체가 있는걸요?");    
                    return; 
                }
            }
            // Phase내의 모든 몬스터가 죽었다면
            
            // 현재 페이즈를 종료하고
            defaultPhases[curPhaseIdx].PhasePrefabs.SetActive(false);
            
            curPhaseIdx++;
            OpenPhase(curPhaseIdx);
        }
        // 보스 스테이지일 경우
        else
        {
            if (!BossPhase.Boss.activeSelf)
            {
                StageClear();
            }
        }
    }


    private void OpenPhase(int idx)
    {
        if(idx < defaultPhases.Count)
        {
            ActivePhase(defaultPhases[idx].PhasePrefabs);
        }
        else
        {
            ActiveBossPhase(MidPhase.PhasePrefabs);
        }
    }

    private void ActivePhase(GameObject phase)
    {
        StartCoroutine(ScaleUp(phase));
    }
    private void ActiveBossPhase(GameObject phase)
    {
        StartCoroutine(BossScaleUp(phase));
    }

    private IEnumerator BossScaleUp(GameObject phase)
    {
        // 매개변수로 들어온 midPhase를 먼저 수행하고
        phase.SetActive(true);
        float elapsedTime = 0.0f;
        while (elapsedTime < scaleUpDelay)
        {
            elapsedTime += Time.deltaTime;
            phase.transform.localScale = Vector3.Lerp(new Vector3(startScale, startScale, startScale), new Vector3(endScale, endScale, endScale), elapsedTime / scaleUpDelay);
            yield return null;
        }
        phase.transform.localScale = new Vector3(endScale, endScale, endScale);

        // 이후에 bossPhase를 연다
        BossPhase.PhasePrefabs.SetActive(true);
        elapsedTime = 0.0f;
        while (elapsedTime < scaleUpDelay)
        {
            elapsedTime += Time.deltaTime;
            BossPhase.PhasePrefabs.transform.localScale = Vector3.Lerp(new Vector3(startScale, startScale, startScale), new Vector3(endScale, endScale, endScale), elapsedTime / scaleUpDelay);
            yield return null;
        }

        BossPhase.PhasePrefabs.transform.localScale = new Vector3(endScale, endScale, endScale);
    }

    private IEnumerator ScaleUp(GameObject phase)
    {
        phase.SetActive(true);
        float elapsedTime = 0.0f;
        while (elapsedTime < scaleUpDelay)
        {
            elapsedTime += Time.deltaTime;
            phase.transform.localScale = Vector3.Lerp(new Vector3(startScale, startScale, startScale), new Vector3(endScale, endScale, endScale), elapsedTime / scaleUpDelay);
            yield return null;
        }

        phase.transform.localScale = new Vector3(endScale, endScale, endScale);
        yield break;
    }

    private void StageClear()
    {
        GameManager.Instance.GameClearUI.SetActive(true);
    }
}
