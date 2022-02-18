using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    void Victory()
    {
        GetComponent<Animator>().SetBool("Victory", false);
    }
}
