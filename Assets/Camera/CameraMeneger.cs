using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMeneger : MonoBehaviour
{
    public float speed = 5f; //скорость перемещания 
    public float borderEdges = 20f; // поля для прокрутки
    public Vector2 limitations; //Вектор лимита

    public float scrollSpeed; //скорость скрола
    public float scrollSize; //размер скрола


    // Start is called before the first frame update
    void Start()
    {
        //scrollSize = GetComponent<Camera>().orthographicSize = 12f; //начальная позиция скроал
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = transform.position;
        if (Input.mousePosition.y >= Screen.height - borderEdges) // вверх
        {
            cameraPosition.y += speed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= borderEdges) // вниз
        {
            cameraPosition.y -= speed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - borderEdges) // вправо
        {
            cameraPosition.x += speed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= borderEdges) // влево
        {
            cameraPosition.x -= speed * Time.deltaTime;
        }
        

        //scroll = Input.mouseScrollDelta();
        //GetComponent<Camera>().sensorSize = scroll * scrollSpeed * Time.deltaTime;
        //scrollSize += Input.mouseScrollDelta.x * 0.1f; 
        
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -limitations.x, limitations.x);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, -limitations.y, limitations.y);
        //scrollSize = Mathf.Clamp(scrollSize, 5f, 12f);
        
        transform.position = cameraPosition;
    }
}
