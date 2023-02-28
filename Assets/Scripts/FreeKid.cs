using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeKid : MonoBehaviour
{
    [SerializeField] private Animator Kid = null;
    [SerializeField] private bool free = false;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (free)
            {
                Kid.Play("Gangnam Style", 0, 0f);
                gameObject.SetActive(false);
            }
        }
    }

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void FixedUpdate()
    {

    }
}
