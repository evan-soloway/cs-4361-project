using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{


    public AudioSource endsound;

    // Start is called before the first frame update
    void Start()
    {

        // Start the coroutine to close the game
        StartCoroutine(CloseGame());

    }

    IEnumerator CloseGame()
    {
        // Wait for 5 seconds
        endsound.Play();
        yield return new WaitForSeconds(5);

        // Close the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
