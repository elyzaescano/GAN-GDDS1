using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperNotes : MonoBehaviour
{
    public Text noteText;
    public string note;
    public GameObject paperNote;
    bool viewNote = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //noteText.text = note;
    }

    // Update is called once per frame
    void Update()
    {
        noteText.text = note;   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            viewNote = true;
        }
    }

    public void OpenNote()
    {
        if (viewNote)
        {
            paperNote.SetActive(true);
            viewNote = false;
        }
    }
    public void Close()
    {
       paperNote.SetActive(false);
    }
}
