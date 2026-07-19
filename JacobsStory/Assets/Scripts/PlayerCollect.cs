using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollect : MonoBehaviour
{
   AudioSource myAudio;
    //public int score;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
           // ScoreScript variableDisplay = FindFirstObjectByType<ScoreScript>();
            //if (variableDisplay != null)
           // {
           //     variableDisplay.ScoreUpdate(score);
           // }

            // HealthFunc hpDisplay = FindFirstObjectByType<HealthFunc>(); // Find the heath script.
            // if (hpDisplay != null)
            // {
            //     hpDisplay.HealthUpdate(10);
            //     //  hurtTrigger = true;


            // }

            Renderer[] allRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer c in allRenderers) c.enabled = false;
            Collider[] allColliders = gameObject.GetComponentsInChildren<Collider>();
            foreach (Collider c in allColliders) c.enabled = false;
            StartCoroutine(PlayAndDestroy(myAudio.clip.length));
        }

        if (gameObject.CompareTag("Goal"))
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Finish();
            }
        }
    }

    // public int getScore()
    // {
    //     return score;
    // }

    private IEnumerator PlayAndDestroy(float waitTime)
    {
        myAudio.Play();
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    void Finish()
    {
      SceneManager.LoadScene("EndGameWin");
        
    }
}


