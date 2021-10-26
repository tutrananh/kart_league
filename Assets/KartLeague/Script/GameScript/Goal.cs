using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            bool side;
            if (this.gameObject.name.Equals("BlueGoal"))
            {
                side = true;
            }
            else 
            {
                side = false; 
            }
            gameManager.UpdateGoal(side);
        }
    }
}
