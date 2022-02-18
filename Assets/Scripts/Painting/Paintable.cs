using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
public class Paintable : MonoBehaviour {
    public static Paintable paintable;
const int TEXTURE_SIZE = 10;

    public float extendsIslandOffset = 1;
    public GameObject paintText;
    RenderTexture extendIslandsRenderTexture;
    RenderTexture uvIslandsRenderTexture;
    RenderTexture maskRenderTexture;
    RenderTexture supportTexture;
    int[,] x;
    Renderer rend;
    bool waitforit;
    int maskTextureID = Shader.PropertyToID("_MaskTexture");

    int count;
    public RenderTexture getMask() => maskRenderTexture;
    public RenderTexture getUVIslands() => uvIslandsRenderTexture;
    public RenderTexture getExtend() => extendIslandsRenderTexture;
    public RenderTexture getSupport() => supportTexture;
    public Renderer getRenderer() => rend;
    
    public Texture2D rgbTex1;
    bool OnlyOne;
    private void Awake()
    {
           paintable = this;
    }
    void Start() {
        maskRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        maskRenderTexture.filterMode = FilterMode.Bilinear;

        extendIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        extendIslandsRenderTexture.filterMode = FilterMode.Bilinear;

        uvIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        uvIslandsRenderTexture.filterMode = FilterMode.Bilinear;

        supportTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        supportTexture.filterMode =  FilterMode.Bilinear;

        rend = GetComponent<Renderer>();
        rend.material.SetTexture(maskTextureID, extendIslandsRenderTexture);
       
        PaintManager.instance.initTextures(this);
        x = new int[extendIslandsRenderTexture.width, extendIslandsRenderTexture.height];
        for (int i = 0; i < extendIslandsRenderTexture.width; i++)
        {
            for (int j = 0; j < extendIslandsRenderTexture.height; j++)
            {
                x[i, j] = 0;
            }
        }

    }
    private void Update()
    {
        if (!waitforit)
        {
            waitforit = true;
            StartCoroutine(waitForIT());
        }
        else
        {

            CheckWhite();
        }
        rgbTex1 = toTexture2D(extendIslandsRenderTexture);

        IsTransparent(rgbTex1);

     
        

        
    }

    public void OnDisable(){
        maskRenderTexture.Release();
        uvIslandsRenderTexture.Release();
        extendIslandsRenderTexture.Release();
        supportTexture.Release();
    }
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
    
        void IsTransparent(Texture2D tex)
        {
        for (int i = 0; i < tex.width; i++)
            for (int j = 0; j < tex.height; j++)
                if (tex.GetPixel(i, j).r != 0)
                    x[i, j] = 1;
        }

    void CheckWhite()
    {
        count = 0;
        for (int i = 0; i < rgbTex1.width; i++)
            for (int j = 0; j < rgbTex1.height; j++)
                if(x[i,j] == 1)
                {
                    count++;
                }
        paintText.GetComponent<TextMeshProUGUI>().text = count + "/100";
        if (count == 100)
        {
            LevelManagement.levelManagement.LevelCompleted.SetActive(true);
            GameManager.gameManager.NextLevelPrefab.SetActive(true);

        }
    }
    IEnumerator waitForIT()
    {
        yield return new WaitForSeconds(1);
        waitforit = true;
    }


}