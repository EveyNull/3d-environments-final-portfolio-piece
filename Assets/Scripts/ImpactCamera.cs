using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCamera : MonoBehaviour {

    public enum hAlignment {left, centre, right };
    public enum vAlignment { top, middle, bottom };

    public hAlignment horAlign = hAlignment.left;
    public vAlignment verAlign = vAlignment.bottom;

    public enum UnitsIn { pixels, screen_percentage };

    public UnitsIn unit = UnitsIn.screen_percentage;

    public int width = 50;
    public int height = 50;
    public int xOffset = 0;
    public int yOffset = 0;

    public bool update = true;
    public bool displaying = true;

    public float displayTime = 5f;

    private int hsize, vsize, hloc, vloc;

    private float timer = 0f;

    private GameObject target;

	// Use this for initialization
	void Start () {
        AdjustCamera();
	}
	
	// Update is called once per frame
	void Update () {
        if (displaying)
        {
            AdjustCamera();
        }
        if(timer >= displayTime)
        {
            displaying = false;
            GetComponent<Camera>().enabled = false;
        }
        else
        {
            timer += Time.deltaTime;
        }
	}

    void AdjustCamera()
    {
        int sw = Screen.width;
        int sh = Screen.height;
        float swPercent = sw * 0.01f;
        float shPercent = sh * 0.01f;
        float xOffPercent = xOffset * swPercent;
        float yOffPercent = yOffset * swPercent;
        int xOff;
        int yOff;

        if(unit == UnitsIn.screen_percentage)
        {
            hsize = width * (int)swPercent;
            vsize = height * (int)shPercent;
            xOff = (int)xOffPercent;
            yOff = (int)yOffPercent;
        }
        else
        {
            hsize = width;
            vsize = height;
            xOff = xOffset;
            yOff = yOffset;
        }

        switch(horAlign)
        {
            case hAlignment.left:
                hloc = xOff;
                break;
            case hAlignment.right:
                int justifiedRight = (sw - hsize);
                hloc = (justifiedRight - xOff);
                break;
            case hAlignment.centre:
                float justifiedCenter = (sw * 0.5f) - (hsize * 0.5f);
                hloc = (int)(justifiedCenter - xOffset);
                break;
        }

        switch(verAlign)
        {
            case vAlignment.top:
                int justifiedTop = sh - vsize;
                vloc = (justifiedTop - yOff);
                break;
            case vAlignment.bottom:
                vloc = yOff;
                break;
            case vAlignment.middle:
                float justifiedMiddle = (sh * 0.5f) - (vsize * 0.5f);
                vloc = (int)(justifiedMiddle - yOff);
                break;
        }

        GetComponent<Camera>().pixelRect = new Rect(hloc, vloc, hsize, vsize);
    }

    public void SetTarget(GameObject newTarget)
    {
        timer = 0f;
        target = newTarget;
        displaying = true;
        GetComponent<Camera>().enabled = true;
        transform.position = target.transform.position + new Vector3(0, 5, 5);
        transform.LookAt(target.transform);
    }
}
