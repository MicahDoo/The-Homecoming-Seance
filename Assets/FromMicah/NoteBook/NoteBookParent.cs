using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookParent : MonoBehaviour {

    static List<GameObject> spreadsActive;
    static Transform noteBook;
    static int currentSpread = 0;


	// Use this for initialization
	void Start () {
        noteBook = transform.GetChild(1);
        Debug.Log(noteBook.name + "SetAsNoteBook");

        spreadsActive = new List<GameObject>();

        gameObject.SetActive(false);

        //Debug
        //AddNote("BabySketchLiu");
        //AddNote("BabySketchChang");
        //AddNote("ModelSketchChang");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public void AddNote(string noteName)
    {
        foreach(Transform spread in noteBook)
        {
            bool spreadToBeAdded = false;
            foreach (Transform page in spread)
            {
                foreach(Transform note in page)
                {
                    if (note.name.Contains(noteName))
                    {
                        note.gameObject.SetActive(true);
                        spreadToBeAdded = true;
                        foreach(GameObject go in spreadsActive)
                        {
                            if(go.name == spread.name)
                            {
                                spreadToBeAdded = false; //alreadyAdded
                            }
                        }
                        continue;
                    }
                }
            }
            if (spreadToBeAdded)
            {
                spreadsActive.Add(spread.gameObject);
                spreadsActive[currentSpread].SetActive(true);
                break;
            }
        }
    }

    public void NextPage()
    {
        if(currentSpread < spreadsActive.Count - 1)
        {
            spreadsActive[currentSpread++].SetActive(false);
            spreadsActive[currentSpread].SetActive(true);
        }
    }

    public void PreviousPage()
    {
        if(currentSpread > 0)
        {
            spreadsActive[currentSpread--].SetActive(false);
            spreadsActive[currentSpread].SetActive(true);
        }
    }
}
