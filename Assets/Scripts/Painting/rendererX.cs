using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rendererX : MonoBehaviour
{
    public Texture2D myTexture;
    // Start is called before the first frame update
    void Update()
    {
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = (Paintable.paintable.rgbTex1);

    }

}
