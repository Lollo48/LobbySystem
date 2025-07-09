using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class MenuSwipe : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static bool CanSlide = true;

    private Vector3 _panelLocation;

    [Tooltip("Negative on the Left site of the MainMenu")] [Range(-3, 0)] [SerializeField]
    private int totalLeftPages;

    [Range(0, 3)] [Tooltip("Positive on the right site of the MainMenu")] [SerializeField]
    private int totalRightPages;

    private int _currentPageIndex;

    [Range(0, 1)] [Tooltip("Threshold for page swipe")] [SerializeField]
    private float percentThreshold;

    [Range(0, 1)] [Tooltip("Swipe velocity")] [SerializeField]
    private float swipeTime;


    private void Awake()
    {
        _panelLocation = transform.position;

    }


    public void OnDrag(PointerEventData eventData)
    {
        if (!CanSlide) return;

        float xDifference = eventData.pressPosition.x - eventData.position.x;

        if (!IsSwipeAllowed(xDifference)) return;

        transform.position = _panelLocation - new Vector3(xDifference, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!CanSlide) return;

        float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;
        float xDifference = percentage * Screen.width;

        if (!IsSwipeAllowed(xDifference))
        {
            StartCoroutine(SmoothMove(transform.position, _panelLocation, swipeTime));
            return;
        }

        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = _panelLocation;
            if (percentage > 0)
            {
                newLocation += new Vector3(-Screen.width, 0, 0);
                _currentPageIndex++;
            }
            else if (percentage < 0)
            {
                newLocation += new Vector3(Screen.width, 0, 0);
                _currentPageIndex--;
            }

            StartCoroutine(SmoothMove(transform.position, newLocation, swipeTime));
            _panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, _panelLocation, swipeTime));
        }
    }

    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
    
    private bool IsSwipeAllowed(float xDifference)
    {
        if (xDifference > 0 && _currentPageIndex >= totalRightPages)
            return false;

        if (xDifference < 0 && _currentPageIndex <= totalLeftPages)
            return false;

        return true;
    }
}
