using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {

    private bool m_mouseSelected = false;
    public GameObject m_connectedPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Mouse0) && WithinArea(Input.mousePosition) && !m_mouseSelected)
        {
            //print("detected");
            m_mouseSelected = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && m_mouseSelected)
            m_mouseSelected = false;
        //print(Input.mousePosition);

        if (m_mouseSelected)
            this.transform.position = Input.mousePosition;

        if (WithinArea(m_connectedPosition.transform.position))
        {
            print("collided Manual!!!!");
        }
    }

    bool WithinArea(Vector3 _test)
    {
        Vector2 pos = GetComponent<RectTransform>().position;
        float width = GetComponent<RectTransform>().rect.width;
        float height = GetComponent<RectTransform>().rect.height;
        Vector2 bottomLeft = new Vector2();
        Vector2 topRight = new Vector2();

        bottomLeft.x = pos.x - width  / 2;
        bottomLeft.y = pos.y - height / 2;
        topRight.x = pos.x + width  / 2;
        topRight.y = pos.y + height / 2;

        bool isInside = false;

        if (_test.x > bottomLeft.x && _test.x < topRight.x &&
            _test.y > bottomLeft.y && _test.y < topRight.y)
            isInside = true;

        return isInside;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collided");
        if (collision.name == "00")
            print("detected");
    }
}
