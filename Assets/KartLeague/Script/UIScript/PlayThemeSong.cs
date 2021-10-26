using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayThemeSong : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] themeSongs = GameObject.FindGameObjectsWithTag("ThemeSong");
        if(themeSongs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
