using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RingMoveControl : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 destination;
    static int i = 0;
    static int sheetNumber = 0;
    [SerializeField] Transform OuijaPiece;

    private RectTransform rectTransform;
    static private RectTransform dots;
    [SerializeField] Image _dotPrefab;
    static Image dotPrefab;
    static Image[] allDots;
    private Button nextButton;
    static private List<string> answers;

    void Awake()
    {
        //** Original Point???
        OuijaCoords.createCoords();
        foreach (RectTransform rectTransform in transform.parent.transform)
        {
            dots = rectTransform;
            break;
        }
    }

    void Start()
    {
        //Don't initialize dots in awake because stretch hasn't been done
        answers = new List<string>();
        answers.Add("Yes");
        answers.Add("Depends");
        answers.Add("Yes");
        answers.Add("Bobby, midsummer baby Bobby");
        answers.Add("One of you");
        answers.Add("truth in healthroom, yang go find my diagonosis there");

        dotPrefab = _dotPrefab;
        plotDots();

        nextButton = transform.parent.GetComponentsInChildren<Button>()[0];

        nextButton.GetComponentInChildren<Text>().text = "Next";

        nextButton.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if(destination != rectTransform.position)
        {
            rectTransform.position = Vector3.Lerp(rectTransform.position, destination, 0.5f);
            OuijaPiece.localPosition = new Vector3(-transform.localPosition.x/ transform.parent.GetComponent<RectTransform>().rect.width, OuijaPiece.localPosition.y, -transform.localPosition.y / transform.parent.GetComponent<RectTransform>().rect.height);

            //OuijaPiece.localPosition = new Vector3(0.5f, OuijaPiece.localPosition.y, 0.5f);
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        Debug.Log("PointerDown");
    }

    public void OnBeginDrag(PointerEventData e)
    {
        Debug.Log("DragBegins");
    }

    public void OnDrag(PointerEventData e)
    {
        //No going back
        destination = NearestPointOnFiniteLine(destination, endPoint, e.position);
        
        //Can go back
        //destination = NearestPointOnFiniteLine(startPoint, endPoint, e.position);

        //Next route / edge
        if ((rectTransform.position - endPoint).magnitude < 1f)
        {
            rectTransform.position = endPoint;
            if (i+1 < allDots.Length - 1)
            {
                startPoint = endPoint;
                ++i;
                Debug.Log("i = " + i.ToString());
                endPoint = allDots[i + 1].transform.position;
            }
            /*else if (i+1 == allDots.Length - 1)
            {
                startPoint = endPoint;
                ++i;
                endPoint = allDots[0].transform.position;
            }*/
            else
            {
                nextButton.gameObject.SetActive(true);
                if(sheetNumber + 1 >= answers.Count)
                {
                    nextButton.GetComponentInChildren<Text>().text = "Leave";
                }
                Debug.Log("Suceeded");
            }
        }
    }

    public void OnEndDrag(PointerEventData e)
    {
    }

    public static Vector3 NearestPointOnFiniteLine(Vector3 start, Vector3 end, Vector3 point)
    {
        var line = (end - start);
        var length = line.magnitude;
        line.Normalize();

        var v = point - start;
        var d = Vector3.Dot(v, line);
        if((point - (start + line * d)).magnitude > 50f)
        {
            return start;
        }
        d = Mathf.Clamp(d, 0f, length);

        return start + line * d;
    }

    public void startNewPage()
    {
        i = 0;
        for (int i = 1; i < allDots.Length; ++i)
        {
            DestroyImmediate(dots.GetComponentsInChildren<Image>()[1].transform.gameObject);
        }
        Debug.Log("DotsDeleted");
        if (sheetNumber + 1 < answers.Count)
        {
            ++sheetNumber;
            plotDots();
        }
        else
        {
            foreach(RectTransform rectTransform in transform.parent.transform.parent.transform.parent.transform)
            {
                rectTransform.gameObject.SetActive(false);
            }
        }
    }

    private void plotDots()
    {
        List<string> marked = new List<string>();
        for (int index = 0; index < answers[sheetNumber].Length; ++index)
        {
            if (char.IsLetterOrDigit(answers[sheetNumber][index]))
            {
                if (answers[sheetNumber].Length >= (index + 3) && answers[sheetNumber].Substring(index, 3).ToUpper() == "YES" && (index == 0 || (index != 0 && answers[sheetNumber][index - 1] == ' ')) && (index == answers[sheetNumber].Length - 4 || (index < answers[sheetNumber].Length - 4 && answers[sheetNumber][index + 3] == ' ')))
                {
                    Debug.Log("YESLogged");
                    index++; index++;
                    Image temp = Object.Instantiate(dotPrefab, dots);
                    temp.transform.localPosition = new Vector3(temp.transform.parent.GetComponent<RectTransform>().rect.width * (((Vector2)OuijaCoords.LatinOuijaCoords["YES"]).x / 1920f - 0.5f), temp.transform.parent.GetComponent<RectTransform>().rect.height * (((Vector2)OuijaCoords.LatinOuijaCoords["YES"]).y / 1080f - 0.5f), 0f);
                    foreach (string str in marked)
                    {
                        if (str == "YES")
                        {
                            temp.transform.Rotate(new Vector3(0, 0, 30));
                        }
                    }
                    marked.Add("YES");
                }
                else if (answers[sheetNumber].Length >= (index + 2) && answers[sheetNumber].Substring(index, 2).ToUpper() == "NO" && (index == 0 || (index != 0 && answers[sheetNumber][index - 1] == ' ')) && (index == answers[sheetNumber].Length - 3 || (index < answers[sheetNumber].Length - 3 && answers[sheetNumber][index + 2] == ' ')))
                {
                    Debug.Log("NOLogged");
                    index++;
                    Image temp = Object.Instantiate(dotPrefab, dots);
                    temp.transform.localPosition = new Vector3(temp.transform.parent.GetComponent<RectTransform>().rect.width * (((Vector2)OuijaCoords.LatinOuijaCoords["NO"]).x / 1920f - 0.5f), temp.transform.parent.GetComponent<RectTransform>().rect.height * (((Vector2)OuijaCoords.LatinOuijaCoords["NO"]).y / 1080f - 0.5f), 0f);
                    foreach (string str in marked)
                    {
                        if (str == "NO")
                        {
                            temp.transform.Rotate(new Vector3(0, 0, 30));
                        }
                    }
                    marked.Add("NO");
                }
                else
                {
                    Debug.Log(answers[sheetNumber][index].ToString().ToUpper() + "Logged");
                    Image temp = Object.Instantiate(dotPrefab, dots);
                    Debug.Log(answers[sheetNumber][index].ToString().ToUpper());
                    temp.transform.localPosition = new Vector3(temp.transform.parent.GetComponent<RectTransform>().rect.width * (((Vector2)OuijaCoords.LatinOuijaCoords[answers[sheetNumber][index].ToString().ToUpper()]).x / 1920f - 0.5f), temp.transform.parent.GetComponent<RectTransform>().rect.height * (((Vector2)OuijaCoords.LatinOuijaCoords[answers[sheetNumber][index].ToString().ToUpper()]).y / 1080f - 0.5f), 0f);
                    foreach (string str in marked)
                    {
                        if (str == answers[sheetNumber][index].ToString().ToUpper())
                        {
                            temp.transform.Rotate(new Vector3(0, 0, 30));
                        }
                    }
                    marked.Add(answers[sheetNumber][index].ToString().ToUpper());
                }
            }
        }
        //dots = transform.parent.transform.GetComponentsInChildren<RectTransform>()[1];

        // number labelling (Ring has to be the last element for it to be on top of the dots)
        Image temp1 = Object.Instantiate(dotPrefab, dots);
        int numOfDots = dots.GetComponentsInChildren<Image>().Length;
        allDots = dots.GetComponentsInChildren<Image>();
        temp1.transform.position = allDots[i].transform.position;
        temp1.transform.Rotate(new Vector3(0, 0, 30));
        for (int j = 0; j < numOfDots; ++j)
        {
            allDots[j].GetComponentInChildren<Text>().text = j.ToString();
        }

        startPoint = allDots[i].transform.position;
        endPoint = allDots[i + 1].transform.position;

        rectTransform = GetComponent<RectTransform>();
        destination = rectTransform.position = startPoint;

        OuijaPiece.localPosition = new Vector3(-transform.localPosition.x / transform.parent.GetComponent<RectTransform>().rect.width, OuijaPiece.localPosition.y, -transform.localPosition.y / transform.parent.GetComponent<RectTransform>().rect.height);
    }
}
