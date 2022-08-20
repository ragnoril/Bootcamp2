using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Agent : MonoBehaviour
{

    MeshRenderer rend;

    DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> tweener;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material.DOColor(Color.red, 3f);

        tweener = transform.DOMove(new Vector3(0f, 5f, 0f), 3f).SetLoops(-1, LoopType.Yoyo);
        //DOTween.To(() => rend.material.color, x => rend.material.color = x, Color.red, 3f).SetEase(Ease.InElastic);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            tweener.Kill();
            //transform.DOKill();
            //DOTween.Kill(transform);
            //DOTween.KillAll();

        }
    }
}
