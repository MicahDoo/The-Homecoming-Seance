using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class begin : MonoBehaviour {

    public void newGame()
    {
        SceneManager.LoadScene(1);
    }
}
