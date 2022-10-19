using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    //private Vector3 surfVelo;
    public float playerSpeed = 100f;

    private Player playerRef;


    public void attachPlayer(Player playerRef)
    {
        this.playerRef = playerRef;

        float sz=gameObject.GetComponent<Planet>().size;
        playerRef.transform.localPosition = new Vector3(0.0f, sz -0.0f, 0.0f);
    }
    public void detachPlayer()
    {
        playerRef = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float sz = gameObject.GetComponent<Planet>().size;
        float actualSpeed = playerSpeed * 1.63f / sz;

        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //surfVelo = move * actualSpeed;
        GameObject core = gameObject;
        Vector3 d1 = Vector3.right, d2 = Vector3.forward;
        if (playerRef)
        {
            d1 = playerRef.transform.right;
            d2 = playerRef.transform.forward;
        }
        transform.RotateAround(core.transform.position, d1, -move.y * Time.deltaTime * actualSpeed);
        transform.RotateAround(core.transform.position, d2, move.x * Time.deltaTime * actualSpeed);
    }

}
