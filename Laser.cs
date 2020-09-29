using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    // we need speed
    [SerializeField]
    private float speed = 25f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate it up indefinately
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        //if laser gets out of screen so destroy it
        if(transform.position.y>=10f)
        {  //check if has parent
            //the destroy parent
            if(transform.parent!=null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
