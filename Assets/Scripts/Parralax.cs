using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;//attached to the camera so that when it moves, the dist variable changes to make the effect work
    public float parallaxEffect; //controls how fast we want the background to move. If closer to the player, set to low value like zero to make it move faster. If far from player, set to high value like .9 to move slow.

    void Start()
    {
        startpos = transform.position.x;//current position
        length = GetComponent<SpriteRenderer>().bounds.size.x;//how long the image is.
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));//temp value to make effect infinite.
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);//where the magic happens

        if (temp > startpos + length) startpos += length;// if player is clode to the end of the image, the image will move back to the center of the player to keep the effect going infinitey.
        else if (temp < startpos - length) startpos -= length;//^^in the opposite direction
    }
}
