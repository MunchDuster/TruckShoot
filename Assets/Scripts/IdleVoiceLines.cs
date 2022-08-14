using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdleVoiceLines : MonoBehaviour
{
	public AudioClip[] lines;
	public string[] linesSubs;
	public TextMeshProUGUI sub;

	public float minTime = 6;
	public float maxTime = 12;

	AudioSource source;
	Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
		coroutine = StartCoroutine(Play());
    }

	IEnumerator Play()
	{
		while(true)
		{
			for(int i = 0; i < lines.Length; i++)
			{
				source.PlayOneShot(lines[i]);
				sub.text = linesSubs[i];
				yield return new WaitForSeconds(lines[i].length * 1.5f);
				sub.text = "";

				yield return new WaitForSeconds(Random.Range(minTime, maxTime));
			}
		}
	}

	public void Stop()
	{
		StopCoroutine(coroutine);
		sub.text = "";
		source.Stop();
	}
}
