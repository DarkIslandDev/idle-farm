using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class Components
{
    public Transform wheatTransform;
    public Transform[] wheats;
    
    public Transform WheatBlock;

    [HideInInspector] public Transform myTransform;
    
    [HideInInspector] public InputHandler inputHandler;
    [HideInInspector] public PlayerManager playerManager;
}

public class Wheat : MonoBehaviour
{
    public Components components;
    public float timer = 3;
    public bool isGrowingUp = false;
    public bool isCatched = true;
    
    private Transform wt;
    private Transform wh;
    private Transform wb;


    private void Start()
    {
        components.myTransform = transform;
        components.playerManager = PlayerManager.instance;

        StartCoroutine(Growing());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isGrowingUp && components.playerManager.whBlocks.Count < 40)
            {
                components.inputHandler = collision.GetComponent<InputHandler>();

                StartCoroutine(GetWheatDown());                
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isCatched == false && components.playerManager.whBlocks.Count <= 40)
            {
                GetWheatBlock();
            }
        }
    }

    private IEnumerator Growing()
    {
        if (!isGrowingUp)
        {
            wh = Instantiate(components.wheatTransform,
                components.myTransform.position,
                Quaternion.identity);
            wh.SetParent(components.myTransform);

            wt = transform.GetChild(1);

            components.wheats = wt.GetComponentsInChildren<Transform>();
            
            yield return new WaitForSeconds(timer);
            isGrowingUp = true;
        }
    }

    private IEnumerator GetWheatDown()
    {
        for (int i = 1; i < components.wheats.Length; i++)
        {
            components.inputHandler.HandleAttack();
            yield return new WaitForSeconds(0.5f);
            Destroy(components.wheats[i].transform.gameObject);
            yield return new WaitForSeconds(1f);
        }
        
        Destroy(wh.gameObject);
        
        isGrowingUp = false;
        
        wb = Instantiate(components.WheatBlock, components.myTransform);
        wb.SetParent(components.myTransform);
        
        isCatched = false;
    }
    
    private void GetWheatBlock()
    {
        wb.DOMove(components.playerManager.wheatHolder.position, 0f,false);
        wb.SetParent(components.playerManager.wheatHolder);
        wb.localRotation = Quaternion.identity;
        
        isCatched = true;
        
        components.playerManager.whBlocks.Add(wb);
        components.playerManager.SetupPositionForBlock();
        
        StartCoroutine(Growing());
    }
}
