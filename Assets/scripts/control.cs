using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class control : DATA
{

    // Use this for initialization
    Transform t;
    bool doorOpen = false;
    bool teleported = false;
    Transform grab;
    bool[] cabinet_open = new bool[4] { false, false, false, false };
    bool[] drawer_open = new bool[6] { false, false, false, false, false, false };
    bool[] window_open = new bool[2] { false, false };
    Vector3 player_pos_origin;
    Vector3 player_pos_begin = new Vector3(-5.493271f, 120.8466f, -15.88688f);

    public GameObject terminalController;

    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit ray_hit;

            if (Physics.Raycast(ray, out ray_hit))
            {
                Transform o = ray_hit.collider.gameObject.transform;
                if (o.parent != null)
                {
                    if (o.tag == "switch")
                    {
                        if (!lightOn)
                        {
                            lightOn = true;
                            o.parent.Rotate(Vector3.forward * -30);
                        }
                        else
                        {
                            lightOn = false;
                            o.parent.Rotate(Vector3.forward * 30);
                        }
                    }
                }

                if (!EventSystem.current.IsPointerOverGameObject() && !GetComponent<InventoryControl>().isInFreezeMode())
                {
                    if (o.tag == "cabinet_door_right")
                    {
                        if ((o.parent.parent.name == "door" && !cabinet_open[0]) || (o.parent.parent.name == "door (3)" && !cabinet_open[3]))
                        {
                            //o.parent.parent.parent.transform.localPosition = new Vector3(o.parent.parent.parent.transform.localPosition.x - 11.75f, o.parent.parent.parent.transform.localPosition.y, o.parent.parent.parent.transform.localPosition.z);
                            o.parent.parent.parent.Rotate(Vector3.up * -30);
                            //o.parent.parent.transform.localPosition = new Vector3(o.parent.parent.transform.localPosition.x + 0.5f, o.parent.parent.transform.localPosition.y, o.parent.parent.transform.localPosition.z);
                            if (o.parent.parent.name == "door")
                                cabinet_open[0] = true;
                            else
                                cabinet_open[3] = true;
                        }
                        else if ((o.parent.parent.name == "door" && cabinet_open[0]) || (o.parent.parent.name == "door (3)" && cabinet_open[3]))
                        {
                            //o.parent.parent.parent.transform.localPosition = new Vector3(o.parent.parent.parent.transform.localPosition.x + 11.75f, o.parent.parent.parent.transform.localPosition.y, o.parent.parent.parent.transform.localPosition.z);
                            o.parent.parent.parent.Rotate(Vector3.up * 30);
                            //o.parent.parent.transform.localPosition = new Vector3(o.parent.parent.transform.localPosition.x - 0.5f, o.parent.parent.transform.localPosition.y, o.parent.parent.transform.localPosition.z);
                            if (o.parent.parent.name == "door")
                                cabinet_open[0] = false;
                            else
                                cabinet_open[3] = false;
                        }
                    }
                    if (o.tag == "cabinet_door_left")
                    {
                        if ((o.parent.parent.name == "door (1)" && !cabinet_open[1]) || (o.parent.parent.name == "door (2)" && !cabinet_open[2]))
                        {
                            //o.parent.parent.parent.transform.localPosition = new Vector3(o.parent.parent.parent.transform.localPosition.x + 11.75f, o.parent.parent.parent.transform.localPosition.y, o.parent.parent.parent.transform.localPosition.z);
                            o.parent.parent.parent.Rotate(Vector3.up * 30);
                            //o.parent.parent.transform.localPosition = new Vector3(o.parent.parent.transform.localPosition.x - 0.5f, o.parent.parent.transform.localPosition.y, o.parent.parent.transform.localPosition.z);

                            if (o.parent.parent.name == "door (1)")
                                cabinet_open[1] = true;
                            else
                                cabinet_open[2] = true;

                        }
                        else if ((o.parent.parent.name == "door (1)" && cabinet_open[1]) || (o.parent.parent.name == "door (2)" && cabinet_open[2]))
                        {
                            //o.parent.parent.parent.transform.localPosition = new Vector3(o.parent.parent.parent.transform.localPosition.x - 11.75f, o.parent.parent.parent.transform.localPosition.y, o.parent.parent.parent.transform.localPosition.z);
                            o.parent.parent.parent.Rotate(Vector3.up * -30);
                            //o.parent.parent.transform.localPosition = new Vector3(o.parent.parent.transform.localPosition.x + 0.5f, o.parent.parent.transform.localPosition.y, o.parent.parent.transform.localPosition.z);

                            if (o.parent.parent.name == "door (1)")
                                cabinet_open[1] = false;
                            else
                                cabinet_open[2] = false;
                        }
                    }
                    if (o.tag == "drawer")
                    {
                        if (o.parent.parent.name == "desk(drawer)")
                        {
                            if (o.parent.name == "drawer(big)" && !drawer_open[0])
                            {
                                drawer_open[0] = true;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z + 15.1f);
                            }
                            else if (o.parent.name == "drawer(big)" && drawer_open[0])
                            {
                                drawer_open[0] = false;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z - 15.1f);
                            }
                            if (o.parent.name == "drawer(small)1" && !drawer_open[1])
                            {
                                drawer_open[1] = true;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z + 15.1f);
                            }
                            else if (o.parent.name == "drawer(small)1" && drawer_open[1])
                            {
                                drawer_open[1] = false;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z - 15.1f);
                            }

                            /*if (o.parent.name == "drawer(small)2" && !drawer_open[2])
                            {
                                drawer_open[2] = true;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z + 15.1f);
                            }
                            else if (o.parent.name == "drawer(small)2" && drawer_open[2])
                            {
                                drawer_open[2] = false;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z - 15.1f);
                            }*/
                        }
                        else if (o.parent.parent.name == "desk(drawer) (1)")
                        {
                            if (o.parent.name == "drawer(big)" && !drawer_open[3])
                            {
                                drawer_open[3] = true;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z + 15.1f);
                            }
                            else if (o.parent.name == "drawer(big)" && drawer_open[3])
                            {
                                drawer_open[3] = false;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z - 15.1f);
                            }
                            if (o.parent.name == "drawer(small)1" && !drawer_open[4])
                            {
                                drawer_open[4] = true;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z + 15.1f);
                            }
                            else if (o.parent.name == "drawer(small)1" && drawer_open[4])
                            {
                                drawer_open[4] = false;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z - 15.1f);
                            }
                            if (o.parent.name == "drawer(small)2" && !drawer_open[5])
                            {
                                drawer_open[5] = true;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z + 15.1f);
                            }
                            else if (o.parent.name == "drawer(small)2" && drawer_open[5])
                            {
                                drawer_open[5] = false;
                                o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z - 15.1f);
                            }
                        }
                    }
                    if (o.name == "handle" && o.parent.name == "window")
                    {
                        if (!window_open[0])
                        {
                            o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x - 25f, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z);
                            window_open[0] = true;
                        }
                        else
                        {
                            o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x + 25f, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z);
                            window_open[0] = false;
                        }
                    }
                    if (o.name == "handle (1)" && o.parent.name == "window (1)")
                    {
                        if (!window_open[1])
                        {
                            o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x + 25f, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z);
                            window_open[1] = true;
                        }
                        else
                        {
                            o.parent.transform.localPosition = new Vector3(o.parent.transform.localPosition.x - 25f, o.parent.transform.localPosition.y, o.parent.transform.localPosition.z);
                            window_open[1] = false;

                        }
                    }
                    if (o.tag == "teleport")
                    {
                        if (!teleported)
                        {
                            player_pos_origin = t.position;
                            t.position = player_pos_begin;
                            teleported = true;
                            terminalController.SetActive(true);
                        }
                        else
                        {
                            t.position = player_pos_origin;
                            teleported = false;
                        }
                    }
                }

            }
        }
    }
}
