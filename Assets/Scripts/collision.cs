using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{

    public Material glowMat;
    public Renderer rend;

    public Transform lerpTarget = null;
    public float collectSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpTarget != null)
            transform.position = Vector3.Lerp(transform.position, lerpTarget.position, collectSpeed * Time.deltaTime * 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject, 1);
            GameManager.fuel += 1;

            rend.material = glowMat;
            lerpTarget = GameManager.rocket.transform;
        }
    }
}
