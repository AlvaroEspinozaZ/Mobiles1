using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerController _playerController;
    public GameObject Puntero;
    public GameObject[] shapes;
    public Button[] btns;
    private Vector3 position;
    private float width;
    private float height;
    private int myid = 0;
    public List<GameObject> mylist;
    public float timer=0;
    bool isTouch = false;
    void Awake()
    {
        mylist = new List<GameObject>();
        for (int i = 0; i < btns.Length; i++)
        {
            int id = i; 
            btns[i].onClick.AddListener( () => Cuadrado(id));
        }
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        // Position used for the cube.
        position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);

        GUI.Label(new Rect(20, 20, width, height * 0.25f),
            "x = " + position.x.ToString("f2") +
            ", y = " + position.y.ToString("f2"));
    }

    void Update()
    {
        timer += (1*Time.deltaTime);
        // Handle screen touches.
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Halve the size of the cube.
                //Debug.Log("Un click");
                //transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);           
                                 
               if (isTouch)
                {
                    if (timer > -0.1f && timer < 1.2f)
                    {
                        Debug.Log("Estoy destrullendo");
                        if (mylist[mylist.Count - 1] != null || mylist.Count != 0)
                        {
                            Destroy(mylist[mylist.Count - 1]);
                            mylist.Remove(mylist[mylist.Count - 1]);
                            
                        }
                        isTouch = false;

                    }
                }
                else
                {
                    timer = 0;                    
                    isTouch = true;
                    if (timer > 1.2f)
                    {
                        //GameObject tmp = Instantiate(shapes[myid], position, Quaternion.identity);
                        //mylist.Add(tmp);
                        
                    }
                    
                }
                
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (timer > 1.2f)
                {
                   GameObject tmp = Instantiate(shapes[myid], position, Quaternion.identity);
                    mylist.Add(tmp);
                    isTouch = false;
                }
            }


                // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
                {
                //Vector2 pos = touch.position;
                //pos.x = (pos.x - width) / (width/10);
                //pos.y = (pos.y - height) / (height/10);
                //position = new Vector3(pos.x, pos.y, 0.0f);
                //position = Camera.main.ScreenToViewportPoint(pos);
                // Position the cube.
               
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
                position = new Vector3(pos.x, pos.y, 0.0f);

                Puntero.transform.position = position;
                }

            if (Input.touchCount == 2)
            {
                touch = Input.GetTouch(1);

                if (touch.phase == TouchPhase.Began)
                {
                    // Halve the size of the cube.
                    transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    // Restore the regular size of the cube.
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
            }
        }
       
    }


    public void Cuadrado(int id)
    {
        switch (id)
        {
            case 0:
                myid=id;
                break;
            case 1:
                myid = id;
                break;
            case 2:
                myid = id;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().color = btns[id].GetComponent<Image>().color;
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().color = btns[id].GetComponent<Image>().color;
                break;
            case 5:
                gameObject.GetComponent<SpriteRenderer>().color = btns[id].GetComponent<Image>().color;
                break;
        }
    }



}
