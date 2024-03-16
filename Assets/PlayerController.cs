using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject[] shapes;
    [SerializeField] Button[] btns;
    [SerializeField] GameObject myTrailRender;
    private int myid = 0;
    private Color _colorShape = Color.white;
    private Vector3 position;
    private bool isMoving = false;
    private GameObject ToMove;
    public float timer = 0;
    public float timerTrail = 0;
    public bool isTouch =false;
    public bool canCreateShape = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            int id = i;
            btns[i].onClick.AddListener(() => OnPressGetID(id));
        }
    }

    public void OnPressGetID(int id)
    {
        canCreateShape = true;
        switch (id)
        {
            case 0:
                myid = id;
                break;
            case 1:
                myid = id;
                break;
            case 2:
                myid = id;
                break;
            case 3:
                _colorShape = btns[id].GetComponent<Image>().color;
                break;
            case 4:
                _colorShape = btns[id].GetComponent<Image>().color;
                break;
            case 5:
                _colorShape = btns[id].GetComponent<Image>().color;
                break;
        }
    }
        // Update is called once per frame
    void Update()
    {
        timer += (1 * Time.deltaTime);
        timerTrail += (1 * Time.deltaTime);
        if (Input.touchCount > 0 )
        {      
            Touch touch = Input.GetTouch(0);
            MoverSiTocasUnObjeto(touch);
            CrearUnObjeto(touch);
            EliminarUnObejto(touch);
            EliminarVariosObjetos(touch);
        }
    }

    void CrearUnObjeto(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            Vector3 touchposition = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y);
            RaycastHit2D hit = Physics2D.Raycast(touchposition, Vector3.forward);
            if (hit.collider == null && canCreateShape)
            {
                GameObject tmp = Instantiate(shapes[myid], touchposition,Quaternion.identity);
                tmp.GetComponent<SpriteRenderer>().color = _colorShape;
                canCreateShape = false;
            }
        }
    }
    void EliminarUnObejto(Touch touch)
    {
        if (timer > 0.8f)
        {
            isTouch = false;
        }
        if (touch.phase == TouchPhase.Began)
        {
            Vector3 touchposition = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y);
            RaycastHit2D hit = Physics2D.Raycast(touchposition, Vector3.forward);
            if (hit.collider != null && hit.collider.gameObject.tag == "Shape")
            {
                if (isTouch)
                {
                    if (timer > -0.1f && timer < 0.8f)
                    {
                        Destroy(hit.collider.gameObject);
                        isTouch = false;
                    }                   
                }
                else
                {
                    timer = 0;
                    isTouch = true;                   
                }
            }
        }
    }
    void MoverSiTocasUnObjeto(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            Vector3 touchposition = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y);
            RaycastHit2D hit = Physics2D.Raycast(touchposition, Vector3.forward);
            if (hit.collider != null && hit.collider.gameObject.tag == "Shape")
            {
                ToMove = hit.collider.gameObject;
                isMoving = true;                
            }
            
        }

        if (touch.phase == TouchPhase.Ended)
        {
            isMoving = false;
        }
        if (touch.phase == TouchPhase.Moved)
        {
            if (isMoving)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
                position = new Vector3(pos.x, pos.y, 0.0f);

                ToMove.transform.position = position;
            }            
        }
        
    }
    void EliminarVariosObjetos(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            
            Vector3 touchposition = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y);
            RaycastHit2D hit = Physics2D.Raycast(touchposition, Vector3.forward);
            if (hit.collider == null )
            {
                timerTrail = 0;
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
                position = new Vector3(pos.x, pos.y, 0.0f);
                myTrailRender.transform.position = position;
                myTrailRender.SetActive(true);
                myTrailRender.GetComponent<TrailRenderer>().startColor = _colorShape;
            }

        }

        if (touch.phase == TouchPhase.Ended)
        {
            myTrailRender.SetActive(false);
        }
        if (touch.phase == TouchPhase.Moved)
        {
            if (timerTrail < 0.6f)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
                position = new Vector3(pos.x, pos.y, 0.0f);
                myTrailRender.transform.position = position;
                Vector3 touchposition = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y);
                RaycastHit2D hit = Physics2D.Raycast(touchposition, Vector3.forward);
                if (hit.collider != null && hit.collider.gameObject.tag == "Shape")
                {
                    Destroy(hit.collider.gameObject);
                }
            }            
        }
    }
}
