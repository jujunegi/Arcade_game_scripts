using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tripleshot : MonoBehaviour
{
    [SerializeField]
    private GameObject _tripleshot;
    [SerializeField]
    private float speed = 3f;
    //id for powerup
    //0 triple shot
    //1 speed
    //2 shield
    [SerializeField]
    private int powerid;
    // Start is called before the first frame update
    void Start()
    {
        //move down at the speed of 3
        // when leave the screen destroy it 
        // do not interact with enemy
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
       
        if(transform.position.y<=-3.8)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="player")
        {//communicate with player script
            //istripleshotactive = true;
            //handle the component i want
            //assign the handle to components
            Player player = other.transform.GetComponent<Player>();
            if(player!=null)
            {
                if (powerid == 0)
                {
                    player.tripleshotactive();
                    Debug.Log("triple shot");
                }
                else if (powerid == 1)
                {
                    player.speedactive();
                    Debug.Log("speed");
                }
                else if (powerid == 2)
                {
                    player.shieldactive();
                    Debug.Log("sheild");
                }
            }

            Destroy(this.gameObject);
        }
    }
}
