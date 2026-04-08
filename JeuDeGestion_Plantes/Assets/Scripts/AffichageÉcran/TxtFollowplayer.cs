using DG.Tweening;
using UnityEngine;

public class TxtFollowplayer : MonoBehaviour
{
    public GameObject canvasFollowPlayer;
    public float detectionDistance = 5f;

    private bool isShown = false;
    private Transform mainCameraTransform;

    void Awake()
    {
        if (Camera.main != null)
            mainCameraTransform = Camera.main.transform;
    }

    void Start()
    {
        canvasFollowPlayer.transform.localScale = Vector3.zero;
        canvasFollowPlayer.SetActive(false);
    }

    void Update()
    {
        if (canvasFollowPlayer == null || mainCameraTransform == null)
            return;

        float distance = Vector3.Distance(mainCameraTransform.position, transform.position);

        Debug.Log(distance);

        if (distance <= detectionDistance)
        {
            if (!isShown)
            {
                isShown = true;
                canvasFollowPlayer.SetActive(true);

                canvasFollowPlayer.transform.DOKill(); 
                canvasFollowPlayer.transform.DOScale(1f, 1f).SetEase(Ease.OutBack);
            }

            Vector3 targetPosition = new Vector3(mainCameraTransform.position.x, canvasFollowPlayer.transform.position.y, mainCameraTransform.position.z);
            canvasFollowPlayer.transform.LookAt(targetPosition);
            canvasFollowPlayer.transform.Rotate(0, 180, 0);
        }
        else
        {
            if (isShown)
            {
                isShown = false;

                canvasFollowPlayer.transform.DOKill();
                canvasFollowPlayer.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack).OnComplete(() =>
                    {
                        if (!isShown)
                            canvasFollowPlayer.SetActive(false);
                    });
            }
        }
    }

    void OnDestroy()
    {
        if (canvasFollowPlayer != null)
        {
            canvasFollowPlayer.transform.DOKill();
        }
    }
}
