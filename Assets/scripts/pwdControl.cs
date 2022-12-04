using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class pwdControl : MonoBehaviour {
    public List<Image> pwd;
    static int[] ans = new int[6] { 0, 0, 0, 0 ,0,0};
    public Sprite[] pic = new Sprite[10];
    private readonly int[] final1 = new int[4] { 5, 0, 5, 2 };
    private readonly int[] final2 = new int[2] { 4, 3 };
    private readonly int[] final3 = new int[6] { 5, 2, 6, 9, 8, 3 };
    public GameObject box;
    public GameObject box2;
    public GameObject drawer;
    private bool box_isopen = false;
    private bool box2_isopen = false;
    private bool drawer_isopen = false;

	// Use this for initialization
	void Start () {
        pwd = new List<Image>();
        foreach(Transform trs in transform)
        {
            foreach(Transform digit in trs)
            {
                foreach(Transform middle in digit)
                {
                    pwd.Add(middle.GetComponent<Image>());
                    break;
                }
            }
            break;
        }
	}

	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < pwd.Count; i++)
        {
            pwd[i].sprite = pic[ans[i]];
        }
	}

    public void pwd_up1()
    {
        ans[0]++;
        if (ans[0] > 9)
            ans[0] = 0;
        if (ans[0] == 1)
        {
            Debug.Log("test");
            //pwd[0].overrideSprite = Resources.Load("Assets/resources/sprites/按鈕/1",typeof(Sprite)) as Sprite;
            pwd[0].sprite = pic[1];
        }
        string s = "";
        for(int i = 0; i < 4; i++)
        {
            s += ans[i];
        }
        Debug.Log(s);
    }

    public void pwd_down1()
    {
        ans[0]--;
        if (ans[0] < 0)
            ans[0] = 9;
        string s = "";
        for (int i = 0; i < 4; i++)
        {
            s += ans[i];
        }
        Debug.Log(s);
    }

    public void pwd_up2()
    {
        ans[1]++;
        if (ans[1] > 9)
            ans[1] = 0;
        string s = "";
        for (int i = 0; i < 4; i++)
        {
            s += ans[i];
        }
        Debug.Log(s);
    }

    public void pwd_down2()
    {
        ans[1]--;
        if (ans[1] < 0)
            ans[1] = 9;
        string s = "";
        for (int i = 0; i < 4; i++)
        {
            s += ans[i];
        }
        Debug.Log(s);
    }
    public void pwd_up3()
    {
        ans[2]++;
        if (ans[2] > 9)
            ans[2] = 0;
        string s = "";
        for (int i = 0; i < 4; i++)
        {
            s += ans[i];
        }
        Debug.Log(s);
    }

    public void pwd_down3()
    {
        ans[2]--;
        if (ans[2] < 0)
            ans[2] = 9;
        string s = "";
        for (int i = 0; i < 4; i++)
        {
            s += ans[i];
        }
        Debug.Log(s);
    }

    public void pwd_up4()
    {
        ans[3]++;
        if (ans[3] > 9)
            ans[3] = 0;
        string s = "";
        for (int i = 0; i < 4; i++)
        {
            s += ans[i];
        }
        Debug.Log(s);
    }

    public void pwd_down4()
    {
        ans[3]--;
        if (ans[3] < 0)
            ans[3] = 9;
        string s = "";
        for (int i = 0; i < 4; i++)
        {
            s += ans[i];
        }
        Debug.Log(s);
    }

    public void pwd_up5()
    {
        ans[4]++;
        if (ans[4] > 9)
            ans[4] = 0;
        string s = "";
        for (int i = 0; i < 6; i++)
        {
            s += ans[i];
        }
        Debug.Log(s);
    }

    public void pwd_down5()
    {
        ans[4]--;
        if (ans[4] < 0)
            ans[4] = 9;
    }
    public void pwd_up6()
    {
        ans[5]++;
        if (ans[5] > 9)
            ans[5] = 0;
        string s = "";
    }

    public void pwd_down6()
    {
        ans[5]--;
        if (ans[5] < 0)
            ans[5] = 9;
    }

    public void OK()
    {
        int correct = 0;
        if (pwd.Count == 2) {
            for (int i = 0; i < pwd.Count; i++)
            {
                if (ans[i] == final2[i])
                    correct++;
            }
        }
        else if(pwd.Count == 4)
        {
            for(int i = 0; i < pwd.Count; i++)
            {
                if (ans[i] == final1[i])
                    correct++;
            }
        }
        else if (pwd.Count == 6)
        {
            for (int i = 0; i < pwd.Count; i++)
            {
                if (ans[i] == final3[i])
                    correct++;
            }
        }
        if (correct == pwd.Count)
        {
            Debug.Log("congratulations");

            if (pwd.Count == 4 && !box_isopen)
            {
                box.transform.Rotate(0, 90f, 0);
                box_isopen = true;
            }
            else if (pwd.Count == 6 && !drawer_isopen)
            {
                drawer.transform.localPosition = new Vector3(drawer.transform.localPosition.x, drawer.transform.localPosition.y, drawer.transform.localPosition.z + 15.1f);
                drawer_isopen = true;
            }
            else if (pwd.Count == 2 && !box2_isopen)
            {
                box2.transform.Rotate(0, 90f, 0);
                box2_isopen = true;
            }
            TimersOrganizer.reminderCountdown = true;
            InventoryControl.crossHairs.gameObject.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
        }
    }

    public void exit()
    {
        TimersOrganizer.reminderCountdown = true;
        InventoryControl.crossHairs.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}
