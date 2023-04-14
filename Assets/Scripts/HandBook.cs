using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandBook : MonoBehaviour
{

    RectTransform rectTransform;
     Image bookImage;
    Collider2D bookCollider;
    Vector3 bookPosition;

    private bool _moving = false;
    private bool _resetting = false;
    [SerializeField] private Vector2 _initialPosition = Vector2.zero;
    [SerializeField][Tooltip("Time in seconds until the obstacle goes back to it's original position")] private float _respawnTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        bookImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        bookCollider = GetComponent<Collider2D>();
        GameManager.Instance.OnMouseMove += OnMouseMove;
    }

    private void OnMouseMove(Vector2 pos)
    {
        //Get mouse's position on viewport
        var mousePosOnScreen = Camera.main.ScreenToWorldPoint(pos);   
        mousePosOnScreen.z = rectTransform.position.z;
        var distance = Vector2.Distance(mousePosOnScreen, rectTransform.position);
        var mouseOver = (distance >= -0.6f && distance <= 0.6f);
        if (mouseOver)
        {
            bookImage.color = Color.yellow;
        }
        else
        {
            bookImage.color = Color.white;
        }
        if (mouseOver && GameManager.Instance.IsInteracting)
        {
            bookImage.color = Color.red;
            rectTransform.position = mousePosOnScreen;


            if (bookCollider != null)
            {
                bookCollider.enabled = true;
                bookCollider.isTrigger = true;
            }

            if (_resetting)
            {
                _resetting = false;
                StopAllCoroutines();


            }
             _moving = true;
        }
        else if (!GameManager.Instance.IsInteracting && _moving)
        {
            bookImage.color = Color.white;
            _moving = false;

            if (bookCollider != null)
            {
                bookCollider.enabled = false;
                bookCollider.isTrigger = false;
            }
            if (rectTransform.position.ToVector2() != _initialPosition && _moving)
            {
                _resetting = true;
                StartCoroutine(ResetPosition());
            }
        } 
 

    }
    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(_respawnTime);
        _resetting = false;
        rectTransform.position = _initialPosition;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
