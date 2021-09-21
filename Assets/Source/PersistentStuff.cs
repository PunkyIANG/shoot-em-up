using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentStuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PersistentStuff[] objs = GameObject.FindObjectsOfType<PersistentStuff>();

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
