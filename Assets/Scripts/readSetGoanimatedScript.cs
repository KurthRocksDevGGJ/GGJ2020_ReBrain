using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class readSetGoanimatedScript : MonoBehaviour
{
    [SerializeField]
    private Text textarea;
    [SerializeField]
    private string[] strs;
    private int counter = 0;
    private int fontsize = 60;
    [SerializeField]
    private AudioClip clipReady;
    [SerializeField]
    private AudioClip clipSet;
    [SerializeField]
    private AudioClip clipGo;
    private AudioSource _playerAudioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        _playerAudioSource = GetComponent<AudioSource>();
        StartCoroutine(tmr());             
                
    }

    private IEnumerator tmr()
    {        
        while (counter < 3) {
            yield return new WaitForSeconds(0.8f);

            if (counter == 0)
            {
                _playerAudioSource.clip = clipReady;
            }
            if (counter == 1)
            {
                _playerAudioSource.clip = clipSet;
            }
            if (counter == 2)
            {
                _playerAudioSource.clip = clipGo;
            }


            _playerAudioSource.Play(); 
            textarea.fontSize = fontsize;
            textarea.text = strs[counter];
            StartCoroutine(zoom());
            counter++;            

        }
    }

    private IEnumerator zoom()
    {
        int z = 0;
        while (z < 20)
        {
            yield return new WaitForSeconds(0.01f);
            textarea.fontSize += 3;
            z++;
        }
        textarea.text = "";
    }

    
}
