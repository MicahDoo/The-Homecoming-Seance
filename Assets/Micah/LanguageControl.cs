using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Fully Encapsulated
//Every function related to changing fonts should be done here; the results will vary according to which languages are turned on
public class LanguageControl{

    //variables indicating which language is chosen
    static int NumOfLanguages = 2;
    public static bool[] LanguagesOn = new bool[NumOfLanguages];

    enum Languages
    {
        English,
        Chinese,
        //Add more languages here...
    };

    //hashtables
    static public Hashtable[] descriptionHash = new Hashtable[NumOfLanguages];
    static public Hashtable[] noteHash = new Hashtable[NumOfLanguages];
    static public Hashtable[] promptHash = new Hashtable[NumOfLanguages];
    static public Hashtable[] displayNameHash = new Hashtable[NumOfLanguages];
    static public Hashtable[] buttonTextHash = new Hashtable[NumOfLanguages];
    //Add more hashtables here...

    //references to all the texts here
    static public Text gamePrompt;
    static public Text itemDescriptionInPanel;
    static public Text lowerRightButton;
    //Add more text objects here...

    static public void setUpHashTables()
    {
        LanguagesOn[(int)Languages.English] = false;
        LanguagesOn[(int)Languages.Chinese] = true;
        //Set more languages here...

        //This makes sure the number of selected languages doesn't surpass 2
        for(int i = 0, j = 0; i < LanguagesOn.Length; ++i)
        {
            if(j > 2)
            {
                LanguagesOn[i] = false;
            }
            else
            {
                if (LanguagesOn[i]) j++;
            }
        }

        //set up hashtables:
        descriptionHash[(int)Languages.English] = new Hashtable();
        descriptionHash[(int)Languages.English].Add("TopMagnet", "This seems like what the spirits use along with the ouija board to talk to us...");
        descriptionHash[(int)Languages.English].Add("BottomMagnet", "This fell from under the table when I removed the top magnet, what could it have been there for...?");
        descriptionHash[(int)Languages.English].Add("Tripod", "I could use this to fix a camcorder into place... if ever I want to record anything...");
        descriptionHash[(int)Languages.English].Add("Camcorder", "Do I need this to record any video?");
        descriptionHash[(int)Languages.English].Add("FirstKey", "This seems like the key to the healthroom...");

        descriptionHash[(int)Languages.Chinese] = new Hashtable();
        descriptionHash[(int)Languages.Chinese].Add("TopMagnet", "碟仙就是用這塊碟片和通靈版跟我們溝通的...");
        descriptionHash[(int)Languages.Chinese].Add("BottomMagnet", "我把桌上的磁鐵拿起來，這塊磁鐵就從桌底下掉下來，它的作用是...?");
        descriptionHash[(int)Languages.Chinese].Add("Tripod", "如果我要錄影的話，可以用這個固定住相機...");
        descriptionHash[(int)Languages.Chinese].Add("Camcorder", "我有需要用這個路任何東西嗎?");
        descriptionHash[(int)Languages.Chinese].Add("FirstKey", "感覺是進入保健室的鑰匙...");
        descriptionHash[(int)Languages.Chinese].Add("ExitKey", "這是打開門把上的蓋子的鑰匙嗎...");

        noteHash[(int)Languages.English] = new Hashtable();

        noteHash[(int)Languages.Chinese] = new Hashtable();

        promptHash[(int)Languages.English] = new Hashtable();
        promptHash[(int)Languages.English].Add("NoUse", "This doesn't seem like any use here...");
        promptHash[(int)Languages.English].Add("TripodByTable", "Tripod set up by the table!");
        promptHash[(int)Languages.English].Add("CamcorderOnTripod", "Camera mounted and rolling, now what...?");
        promptHash[(int)Languages.English].Add("Reminder", "Right click on mouse to toggle control panel...");
        promptHash[(int)Languages.English].Add("HealthroomCan'tAccess", "This door seems to be locked from inside...");

        promptHash[(int)Languages.Chinese] = new Hashtable();
        promptHash[(int)Languages.Chinese].Add("NoUse", "感覺在這裡沒有用處...");
        promptHash[(int)Languages.Chinese].Add("TripodByTable", "三腳架立在了桌子旁!");
        promptHash[(int)Languages.Chinese].Add("CamcorderOnTripod", "相機上好腳架開錄了，所以...?");
        promptHash[(int)Languages.Chinese].Add("Reminder", "按滑鼠右鍵以開啟或關閉控制選項...");
        promptHash[(int)Languages.Chinese].Add("HealthroomCan'tAccess", "門從裡面鎖起來了...");
        promptHash[(int)Languages.Chinese].Add("LidOpened", "終於可以轉門把出去了...");

        displayNameHash[(int)Languages.English] = new Hashtable();
        displayNameHash[(int)Languages.English].Add("TopMagnet", "OuijaPiece");
        displayNameHash[(int)Languages.English].Add("BottomMagnet", "Magnet");
        displayNameHash[(int)Languages.English].Add("Tripod", "Tripod");
        displayNameHash[(int)Languages.English].Add("Camcorder", "Camera");
        displayNameHash[(int)Languages.English].Add("FirstKey", "Key");
        displayNameHash[(int)Languages.English].Add("ExitKey", "Key");

        displayNameHash[(int)Languages.Chinese] = new Hashtable();
        displayNameHash[(int)Languages.Chinese].Add("TopMagnet", "通靈片");
        displayNameHash[(int)Languages.Chinese].Add("BottomMagnet", "磁鐵");
        displayNameHash[(int)Languages.Chinese].Add("Tripod", "腳架");
        displayNameHash[(int)Languages.Chinese].Add("Camcorder", "錄影機");
        displayNameHash[(int)Languages.Chinese].Add("FirstKey", "鑰匙");
        displayNameHash[(int)Languages.Chinese].Add("ExitKey", "鑰匙");

        buttonTextHash[(int)Languages.English] = new Hashtable();
        buttonTextHash[(int)Languages.English].Add("OK", "OK");
        buttonTextHash[(int)Languages.English].Add("Leave", "Leave");

        buttonTextHash[(int)Languages.Chinese] = new Hashtable();
        buttonTextHash[(int)Languages.Chinese].Add("OK", "確認");
        buttonTextHash[(int)Languages.Chinese].Add("Leave", "離開");
    }

    public static string getDescription(string hashkey)
    {
        string result = null;
        for(int i = 0; i < LanguagesOn.Length; ++i)
        {
            if (LanguagesOn[i])
            {
                if(result != null)
                {
                    result = "\r\n" + result;
                }
                result = descriptionHash[i][hashkey] + result;
            }
        }
        return result;
    }

    public static void setPromptText(string hashkey)
    {
        TimersOrganizer.timerForPromptFadeOn = false;
        TimersOrganizer.startPrompt();
        string result = null;
        for (int i = 0; i < LanguagesOn.Length; ++i)
        {
            if (LanguagesOn[i])
            {
                if (result != null)
                {
                    result = "\r\n" + result;
                }
                result = promptHash[i][hashkey] + result;
            }
        }
        gamePrompt.text = result;
        gamePrompt.gameObject.SetActive(true);
    }

    public static string getDisplayName(string hashkey)
    {
        string result = null;
        for (int i = 0; i < LanguagesOn.Length; ++i)
        {
            if (LanguagesOn[i])
            {
                if (result != null)
                {
                    result = "\r\n" + result;
                }
                result = displayNameHash[i][hashkey] + result;
            }
        }
        return result;
    }

    public static string getButtonText(string hashkey)
    {
        string result = null;
        for (int i = 0; i < LanguagesOn.Length; ++i)
        {
            if (LanguagesOn[i])
            {
                Debug.Log("Added" + buttonTextHash[i][hashkey]);
                result = buttonTextHash[i][hashkey] + result;
                break;
            }
        }
        return result;
    }
}
