using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBlock : MonoBehaviour
{
    bool movingRight = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x > -6)
        {
            movingRight = false;
        }
        if (transform.position.x < -24)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        } else
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
    }
}
