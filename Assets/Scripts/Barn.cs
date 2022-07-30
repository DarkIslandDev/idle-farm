using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Barn : MonoBehaviour
{
    public static double Money;

    public Transform animatedCoin;
    public Transform target;
    public Transform holder;
    [Range(0.5f, 0.9f)] public float minAnimDuration;
    [Range(0.9f, 2f)] public float maxAnimDuration;

    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = target.position;
    }
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag(("Player")))
        {
            StartCoroutine(SellBlocks());
        }
    }

    private IEnumerator SellBlocks()
    {
        if (PlayerManager.instance.whBlocks != null)
        {
            for (int i = PlayerManager.instance.whBlocks.Count - 1; i >= 0; i--)
            {
                PlayerManager.instance.whBlocks[i].DOMove(transform.position, 0.3f, false);
                
                yield return new WaitForSeconds(0.15f);
                
                Destroy(PlayerManager.instance.whBlocks[i].gameObject);
                
                PlayerManager.instance.whBlocks.Remove(PlayerManager.instance.whBlocks[i]);

                Transform coin = Instantiate(animatedCoin, holder.transform);
                coin.SetParent(holder.transform);
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.DOMove(targetPosition, duration)
                    .SetEase(Ease.InOutBack)
                    .OnComplete(() =>
                    {
                        Destroy(coin.gameObject);
                        target.DOShakePosition(1f,2f, 10, 2f, false, true);
                        Money += 15;
                    });

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}