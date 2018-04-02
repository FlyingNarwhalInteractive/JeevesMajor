using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayWonPanel : MonoBehaviour {

    public float displayDuration = 5.0f;
    float displayStart;
    bool isOpening = false;
    bool isClosing = false;
    public Vector2 openState;
    public Vector2 closedState;
    public Text m_topLine;
    public Text m_bottomLine;
    float timeOpen = 0;
    float movementDuration = 1.0f;
    bool sleep = false;

    private void Start()
    {
        closedState += (Vector2)transform.localPosition;
        openState += (Vector2)transform.localPosition;
    }

    // Update is called once per frame
    void Update ()
    {
        if (sleep)
            return;

        if (!isOpening && !isClosing)
            timeOpen += Time.deltaTime;

        if (timeOpen > displayDuration)
        {
            isOpening = false;
            isClosing = true;
        }
        
		if(isOpening)
        {
            Vector2 amountToMove = (openState - closedState) * Time.deltaTime / movementDuration;
            transform.localPosition = transform.localPosition + (Vector3)amountToMove;
            if(Mathf.Abs(Vector2.Distance(openState, transform.localPosition)) < Mathf.Abs(Vector2.Distance(amountToMove, Vector2.zero)))
            {
                // we are within one tick of finished, push to final pos
                transform.localPosition = (Vector3)openState;
                isOpening = false;
            }
        }

        if (isClosing)
        {
            Vector2 amountToMove = (closedState - openState) * Time.deltaTime / movementDuration;
            transform.localPosition = transform.localPosition + (Vector3)amountToMove;
            if (Mathf.Abs(Vector2.Distance(closedState, transform.localPosition)) < Mathf.Abs(Vector2.Distance(amountToMove, Vector2.zero)))
            {
                // we are within one tick of finished, push to final pos
                transform.localPosition = (Vector3)closedState;
                isClosing = false;
                sleep = true;
            }
        }
    }

    public void Reveal(string topLine, string bottomLine, Color topColour, Color bottomColour, float duration)
    {
        // update info on panel
        m_topLine.text = topLine;
        m_bottomLine.text = bottomLine;
        m_topLine.color = topColour;
        m_bottomLine.color = bottomColour;
        displayDuration = duration;

        // reveal panel
        if((Vector2)transform.localPosition != openState)
            isOpening = true;
        isClosing = false;
        sleep = false;
        timeOpen = 0;
    }
}
