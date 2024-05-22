using System.Collections;
using UnityEngine;

public class BackgroundScrollHandler : MonoBehaviour
{
    [SerializeField] private Transform[] backgroundImages;
    [SerializeField] private float scrollSpeed;
    private Coroutine scrollCoroutine;

    private void Start()
    {
        StartBackgroundScroll();
    }

    private void StartBackgroundScroll()
    {
        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
        }

        scrollCoroutine = StartCoroutine(BackgroundScrollCoroutine());
    }

    private IEnumerator BackgroundScrollCoroutine()
    {
        Vector3 scrollVec = new Vector3(0, scrollSpeed,0);
        while (true)
        {
            for (int i = 0; i < backgroundImages.Length; i++) // 배경화면 아래로 스크롤
            {
                backgroundImages[i].position -= scrollVec * Time.deltaTime;
            }
            yield return null;
        }
    }
}