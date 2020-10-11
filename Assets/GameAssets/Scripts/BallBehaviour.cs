using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;

    [Header("Next Balls")]
    [SerializeField] GameObject nextBall;

    [Header("Physics")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Rigidbody2D hook;

    [Header("Prefs")]
    [SerializeField] float releaseTime = .15f;
    [SerializeField] float maxDragDistance = 2f;
    [SerializeField] float timeToResetBall = 2f;

    [Header("Conditions")]
    private bool isPressed = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(isPressed)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Verificar a distância entre o Hook e a bola
            if (Vector3.Distance(mousePosition, hook.position) > maxDragDistance)
            {
                //Normalizar a direção e colocá-la na mesma direção que está sendo puxada
                rb.position = hook.position + (mousePosition - hook.position).normalized * maxDragDistance;
            }
            else
            {
                //Seguir o mouse quando estiver pressionado
                rb.position = mousePosition;
            }
                
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;

        //Soltar a bola
        StartCoroutine(Release());
    }

    //Ao soltar a bola
    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        //Liberar a bola do spring
        GetComponent<SpringJoint2D>().enabled = false;

        this.enabled = false;

        //Tempo para colcoar a próxima bola
        yield return new WaitForSeconds(timeToResetBall);

        if(nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else
        {
            //Perder
            gameManager.EndGame();
        }
    }
}
