using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ // public or private reference 
    // data type (int,float,bool,string)
    public float speed =10f;
    private float _speedincreaser = 2f;
    [SerializeField]
    private GameObject laserprefab;
    [SerializeField]
    private GameObject tripleshotprefab;
    [SerializeField]
    private float firerate = 0.25f;
    [SerializeField]
    private float _canfire = -0.5f;
    private SpawnManager _SpawnManager;    
    [SerializeField]
    private int life = 3;
    [SerializeField]
    private bool _istripleshotactive;
    [SerializeField]
    private bool _isspeedactive;
    [SerializeField]
    private bool _isshieldactive;
    [SerializeField]
    private int _score;
    [SerializeField]
    private AudioClip _laserSound;
    [SerializeField]
    private AudioSource _audioSource;
   


    private ui_manager _ui_Manager;
    // is tripleshot enabled

    // Start is called before the first frame update
    void Start()
    {
        //take the current position=new position(0,0,0)
        transform.position = new Vector3(2, -1, 0);
        _SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_SpawnManager==null)
        {
            Debug.Log("did not grab properly");
        }
        _ui_Manager = GameObject.Find("Canvas").GetComponent<ui_manager>();
        _audioSource = GetComponent<AudioSource>();
        if (_ui_Manager == null)
        {
            Debug.LogError("the ui manager is null");
        }
        if (_audioSource == null)
        {
            Debug.LogError("the audio source is null");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }

    }

    // Update is called once per frame
    void Update()
    {
         movement();
        fire();       
    }

    void movement()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");
        // new vector3(1,0,0)* real time ie time .delta time *speed *input(horizontal input)
        //time.delta time converts 60 frames per sec to real time
        if (_isspeedactive == false)
        {
            transform.Translate(Vector3.right * speed * horizontalinput * Time.deltaTime);
            transform.Translate(new Vector3(0, 1, 0) * speed * verticalinput * Time.deltaTime);
        }
        else
        {


            transform.Translate(Vector3.right * speed * _speedincreaser* horizontalinput * Time.deltaTime);
            transform.Translate(new Vector3(0, 1, 0) * speed * _speedincreaser* verticalinput * Time.deltaTime);
        }
        
        //or one line code for movement is transform.translate(new vector3(horizontalinput,verticalinput,0)*speed*time.deltatime))
        //if player position is out of bounds translate them to to boundary 
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        else if (transform.position.y <= -1.8f)

        {
            transform.position = new Vector3(transform.position.x, -1.8f, 0);
        }
        if (transform.position.x >= 14f)

        {
            transform.position = new Vector3(-14f, transform.position.y, 0);
        }
        else if (transform.position.x <= -14f)

        {
            transform.position = new Vector3(14f, transform.position.y, 0);
        }


    }
    void fire()

    {
        //if i hit space then shoot 
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        { //quaternion.identy is used for giving  default rotation 
            _canfire = Time.time + firerate;

            if (_istripleshotactive==true)
            {
                Instantiate(tripleshotprefab, transform.position, Quaternion.identity);

            }
            else
            {
                Instantiate(laserprefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
        }
        //if triple shot active then
        //instantiate triple shot
        //else fire one laser
       // _audioSource.Play();
    }
   public void damage()
    {
        life --;
        Debug.Log("ek life gyi");

        _ui_Manager.UpdateLives(life);
        //_ui_Manager.UpdateLives(life);
        if (life<1)
        { // communicate with  spawn manager tell it player died
          // find the gameobject
         if(_isshieldactive==false)
            {
                
                _SpawnManager.death();
                Destroy(this.gameObject);
  
            }

          else
            {
                life = life + 1;
                Debug.Log("bach gaya tu");
            }

        }

    }

    public void tripleshotactive()
    {
        _istripleshotactive = true;
        StartCoroutine(tripleshotroutine());
    }
    // delay 5 sec for tripleshot
    public IEnumerator tripleshotroutine()
    {
        yield return new WaitForSeconds(5.0f);
        _istripleshotactive = false;

    }
    public void speedactive()
    {
        _isspeedactive = true;
        StartCoroutine(speedroutine());
    }
    // delay 5 sec for speed
    public IEnumerator speedroutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isspeedactive = false;

    }
    public void shieldactive()
    {
        _isshieldactive = true;
        StartCoroutine(shieldroutine());
    }
    // delay 5 sec for speed
    public IEnumerator shieldroutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isshieldactive = false;

    }
    // method to create score
    public void Addscore()
    {
        _score += 10;
        _ui_Manager.UpdateScore(_score);
    }
    
}

