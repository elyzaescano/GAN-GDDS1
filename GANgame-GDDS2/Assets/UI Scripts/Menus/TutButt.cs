using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutButt : MonoBehaviour
{
    public IEnumerator ButtonFade(GameObject butt, float fadeTime)
    {
        Image image =butt.GetComponent<Image>();
        Color colortoFadeTo = new Color (1f, 1f, 1f, 0f);
        image.CrossFadeColor(colortoFadeTo, fadeTime, true, true);
        yield return new WaitForSeconds(fadeTime);
        butt.SetActive(false);
    }

}
