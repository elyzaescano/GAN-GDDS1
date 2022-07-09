using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperNotes : MonoBehaviour
{
    public Text noteText;
    public string note; //ur input 
    public GameObject paperNote;
    bool viewNote = false;
    EventManager em;
    // Start is called before the first frame update
    void Start()
    {
        //noteText.text = note;
        em = FindObjectOfType<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            EventManager.InteractEvent += OpenNote;

            viewNote = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EventManager.InteractEvent -= OpenNote;
            viewNote = false;
        }
    }

    public void OpenNote()
    {
        if (viewNote)
        {
            paperNote.SetActive(true);
            noteText.text = note;
            viewNote = false;
        }
    }
    public void Close()
    {
       paperNote.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.InteractEvent -= OpenNote;
    }

}
