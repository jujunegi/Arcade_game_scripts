using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private bool gamespeed;
    private Player _player;
    private Animator _anim;
    // handle to animator component

    // Start is called before the first frame update
    void Start()
    {
         
        _player = GameObject.Find("Player").GetComponent<Player>();
        // null check player
        //assign components
        if (_player == null)
        {
            Debug.LogError("player is null");
        }
        _anim = GetComponent<Animator>();
        if (_anim==null)
        {
            Debug.LogError("anim is null");
        }
    }

    // Update is called once per frame
    void Update()

    {
        StartCoroutine(gameflow());
        if (gamespeed == false)
        {
            Debug.Log("speed is  not working"+ speed);
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
       if(gamespeed==true)
        {
            
            transform.Translate(Vector3.down * (speed)* Time.deltaTime);
            Debug.Log("speed is working" + speed);
        }
        //move it down 4 m/s 
        // if bottom the respawn at top at new random position
        if (transform.position.y <= -3.5f)
        {

            transform.position = new Vector3(Random.Range(-13f, 13f), 10 , 0);
           

        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("hit" + other.transform.name);
        if (other.tag == "player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.damage();
            }

            // trigger anim
            _anim.SetTrigger("Enemy_death");
            speed = 0;

            Debug.Log("trigger animation");
            Destroy(this.gameObject);



        }
        if (other.tag == "laser")
        {
            Destroy(this.gameObject,1f);
            //add 10 to score
            if(_player !=null)
            {
                _player.Addscore();

            }
            //trigger anim
            _anim.SetTrigger("Enemy_death");
            speed = 0;
            Debug.Log("trigger animation");
            Destroy(other.gameObject);
        }
    }
   IEnumerator gameflow()
    {
        yield return new WaitForSeconds(10f);
        gamespeed = true;
    }
    
   
}



