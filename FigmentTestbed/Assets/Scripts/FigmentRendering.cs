﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigmentRendering : MonoBehaviour {

    public Material screenRenderMaterial;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, screenRenderMaterial);
    }
}
