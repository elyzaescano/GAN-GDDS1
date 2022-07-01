using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualDpad : MonoBehaviour
{
    public static VirtualDpad instance;

    public Button[] controls;

    Vector2 axis;

    public static int GetAxis(string axe)
    {
        switch (axe.ToLower())
        {
            case "horizontal":
            case "h":
            case "x":
                return Mathf.RoundToInt(instance.axis.x);
            case "vertical":
            case "v":
            case "y":
                return Mathf.RoundToInt(instance.axis.y);
        }
        return 0;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            }
}
