using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour {

    public GameObject directoryLine;
    public GameObject responseLine;

    public InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect sr;
    public GameObject messageList;
    public GameObject terminalParent;
    public GameObject teleport;

    public Button watchNotification;
    public Button watch;

    private int maxCharNum = 70;

    private float timeTillDisplay;
    private string stringToDisplay;
    private bool autoContinue;
    private string patternToMatch;


    private List<messageAndTime> messageQueue;
    class messageAndTime
    {
        public messageAndTime(string message, float time, bool autoContinue)
        {
            this.message = message;
            this.time = time;
            this.autoContinue = autoContinue;
        }
        public messageAndTime(string message, float time, bool autoContinue, string patternToMatch, string mismatchMessage)
        {
            this.message = message;
            this.time = time;
            this.autoContinue = autoContinue;
            this.patternToMatch = patternToMatch;
            this.mismatchMessage = mismatchMessage;
        }
        public string message;
        public float time;
        public bool autoContinue;
        public string patternToMatch = null;
        public string mismatchMessage = null;
    };

    private void OnGUI()
    {
        if(terminalInput.isFocused && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            Debug.Log("Input.GetKeyDown");
            string userInput = "";

            if (terminalInput.text != "")
            {
                //Store whatever the user typed
                userInput = terminalInput.text;

                //Clear the input field
                ClearInputField();

                //Instantiate a gameObject wth a directory prefix
                AddDirectoryLine(userInput);

                if (!autoContinue && timeTillDisplay == -1f)
                {
                    if (patternToMatch == userInput)
                    {
                        nextMessage();
                    }
                    else
                    {
                        Debug.Log("PrintMismatchMessage");
                        AddResponseLine(messageQueue[0].mismatchMessage);
                    }
                }
            }else if (!autoContinue && timeTillDisplay == -1f && patternToMatch == null)
            {
                nextMessage();
            }

            setFocus();

        }
    }

    void ClearInputField()
    {
        terminalInput.text = "";
    }

    void AddDirectoryLine(string userInput)
    {
        //Resizing the command line container so the scrollREct doesn't throw a fit
        Vector2 messageListSize = messageList.GetComponent<RectTransform>().sizeDelta;
        messageList.GetComponent<RectTransform>().sizeDelta = new Vector2(messageListSize.x, messageListSize.y + 35.0f);

        //Instantiate the directory line
        GameObject message = Instantiate(directoryLine, messageList.transform);

        //Set its child index
        message.transform.SetSiblingIndex(messageList.transform.childCount - 2);

        //Set the text of this new gameobject
        message.GetComponentsInChildren<Text>()[1].text = userInput;

        //Move the user input line to the end
        //userInputLine.transform.SetAsLastSibling();

        //Refocus the input field
        terminalInput.ActivateInputField();
        terminalInput.Select();

        sr.normalizedPosition = new Vector2(0, 0);
    }

    void AddResponseLine(string outputMessage)
    {

        if (outputMessage.Contains("\n"))
        {
            string[] lines = outputMessage.Split('\n');
            foreach (string line in lines)
            {
                AddResponseLine(line);
            }
        }else if(outputMessage.Length > maxCharNum)
        {
            Debug.Log("Length > 80");
            AddResponseLine(outputMessage.Substring(0, maxCharNum));
            AddResponseLine(outputMessage.Substring(maxCharNum, outputMessage.Length - maxCharNum));
        }
        else
        {
            //Resizing the command line container so the scrollREct doesn't throw a fit
            Vector2 messageListSize = messageList.GetComponent<RectTransform>().sizeDelta;
            messageList.GetComponent<RectTransform>().sizeDelta = new Vector2(messageListSize.x, messageListSize.y + 35.0f);

            //Instantiate the directory line
            GameObject message = Instantiate(responseLine, messageList.transform);

            //Set its child index
            message.transform.SetSiblingIndex(messageList.transform.childCount - 2);

            //Set the text of this new gameobject
            message.GetComponentInChildren<Text>().text = outputMessage;

            //Refocus the input field
            terminalInput.ActivateInputField();
            terminalInput.Select();
        }

        sr.normalizedPosition = new Vector2(0, 0);
    }

	// Use this for initialization
	void Start ()
    {
        messageQueue = new List<messageAndTime>();

        messageQueue.Add(new messageAndTime("記憶讀取中…", 5f, true));
        messageQueue.Add(new messageAndTime("剩下3秒…", 1f, true));
        messageQueue.Add(new messageAndTime("剩下2秒…", 1f, true));
        messageQueue.Add(new messageAndTime("剩下1秒…", 1f, true));
        messageQueue.Add(new messageAndTime("記憶讀取發生錯誤！！！！！\n", 1f, false));

        messageQueue.Add(new messageAndTime("錯誤代碼101 穩定性錯誤\n錯誤訊息：\n記憶主體目前精神不穩定，嘗試在其中漫遊或可導致不可逆轉之後果，將為您自動導入登出程序……\n", 0f, true));
        messageQueue.Add(new messageAndTime("系統登出中，請稍候……\n", 5f, true));
        messageQueue.Add(new messageAndTime("登出失敗!!!\n錯誤代碼105 定序錯誤\n記憶主體目前處於瀕死狀態，頭腦混亂，造成機器無法判別記憶片段之順序。若要登出記憶，機器穿梭於腦海中的方向必由深入淺（較久遠之記憶處在記憶深層，較近期之記憶則位於淺層），否則有陷入深層，迷失遭困之風險。\n", 3f, false));

        messageQueue.Add(new messageAndTime("解決辦法：\n為了方便記憶探索者幫助機器對記憶做排序，本機器已選出八個記憶片段，擷取成相片，顯示在分頁中。請探索者迅速檢視全八個片段，並尋找能得到的線索，將記憶片段之順序排出。\n", 0.5f, false));

        messageQueue.Add(new messageAndTime("繼續嘗試定序中…\n請繼續探索記憶場景，尋找您需要的線索，並檢視八個記憶片段。\n", 0.5f, true));
        messageQueue.Add(new messageAndTime("記憶讀取發生錯誤！！！！！\n", 1f, false));

        messageQueue.Add(new messageAndTime("錯誤代碼201 聽覺配對錯誤\n錯誤訊息：\n記憶主體目前神經傳導錯亂，導致不同記憶片段之感官內容彼此可能重疊。接下來探索者可能聽到來自其他記憶片段的聲音。探索者或可忽略，或可作為線索記下。\n", 0.5f, false));

        messageQueue.Add(new messageAndTime("繼續嘗試定序中……\n請繼續探索記憶場景，尋找您需要的線索，並檢視八個記憶片段。\n按Enter繼續……", 1f, false));

        messageQueue.Add(new messageAndTime("請探索者輸入記憶排放順序 範例格式：12345678\n", 0.5f, false, "62351874", "登出程序錯誤。\n該記憶順序並非正確順序，無法將記憶探索者帶往淺層記憶之方向，請重新輸入。\n"));

        messageQueue.Add(new messageAndTime("登出成功！記憶出口已打開，請儘速離開。", 0f, false));

        stringToDisplay = messageQueue[0].message;
        timeTillDisplay = messageQueue[0].time;
        autoContinue = messageQueue[0].autoContinue;

        setFocus();
    }
	
	// Update is called once per frame
	void Update () {
        if(timeTillDisplay >= 0f)
        {
            timeTillDisplay -= Time.deltaTime;
            if (timeTillDisplay <= 0f)
            {

                Debug.Log(timeTillDisplay + "LeftTillDisplaying" + stringToDisplay);
                timeTillDisplay = -1f;
                AddResponseLine(stringToDisplay);
                if (stringToDisplay.Contains("!!!"))
                {
                    //Play warning sound
                }
                if (!terminalParent.activeSelf)
                {
                    setNotification();
                }
                if (autoContinue)
                {
                    nextMessage();
                }
            }
        }
    }

    void nextMessage()
    {
        messageQueue.RemoveAt(0);
        stringToDisplay = messageQueue[0].message;
        timeTillDisplay = messageQueue[0].time;
        autoContinue = messageQueue[0].autoContinue;
        patternToMatch = messageQueue[0].patternToMatch;
        //mismatchMessage = messageQueue[0].mismatchMessage;
        if(stringToDisplay == "登出成功！記憶出口已打開，請儘速離開。")
        {
            teleport.SetActive(true);
        }
    }

    void setNotification()
    {
        watchNotification.gameObject.SetActive(true);
        watch.gameObject.SetActive(true);
    }

    public void setFocus()
    {
        terminalInput.ActivateInputField();
        terminalInput.Select();
    }
}
