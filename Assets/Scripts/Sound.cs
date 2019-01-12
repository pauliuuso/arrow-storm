using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

    public AudioClip audioToPlay;
    private float timeToLive;
    private IEnumerator coroutine;
    [System.NonSerialized]
    public float timeAlive;

    void Start()
    {
        timeToLive = audioToPlay.length;
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = audioToPlay;
        audio.Play();

        coroutine = Kill(timeToLive);
        StartCoroutine(coroutine);
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
    }

    IEnumerator Kill(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(gameObject);
    }
}
