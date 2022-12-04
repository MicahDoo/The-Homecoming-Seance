using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoOrderer : MonoBehaviour {

    GridLayoutGroup gridLayoutGroup;
	// Use this for initialization
	void Start () {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize = new Vector2((GetComponent<RectTransform>().rect.width - 50f) / 4f, (GetComponent<RectTransform>().rect.height - 30f) / 2f);

    }
}
