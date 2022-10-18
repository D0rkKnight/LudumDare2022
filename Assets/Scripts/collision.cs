using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public Material glowMat;
    public Renderer rend;


    private Transform lerpTarget = null;
    private bool collected = false;
    public float collectSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (lerpTarget != null)
            transform.position = Vector3.Lerp(transform.position, lerpTarget.position, collectSpeed * Time.deltaTime * 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !collected)
        {
            collected = true;
            Destroy(gameObject, 1);
            GameManager.fuel += 1;

            rend.material = glowMat;
            lerpTarget = GameManager.rocket.transform;
        }
    }
}
