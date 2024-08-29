using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject rightFoot, leftFoot;
    public GameObject soundFX;
    public void SetSoundFoot(int _foot)
    {
        if(_foot == 0)
        {
            GameObject newSound = Instantiate(soundFX, rightFoot.transform.position, Quaternion.identity);
            Destroy(newSound, 1f);
        }
        else if(_foot == 1)
        {
            GameObject newSound = Instantiate(soundFX, leftFoot.transform.position, Quaternion.identity);
            Destroy(newSound, 1f);
        }
    }
}
