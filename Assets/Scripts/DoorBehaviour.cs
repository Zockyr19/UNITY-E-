using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private Animator Door = null;
    [SerializeField] private bool Open = false;

    void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {
            if (Open)
            {
                Door.Play("OpenDoor", 0, 0f);
                gameObject.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
