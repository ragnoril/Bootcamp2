using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemChecker : MonoBehaviour
{
    public List<SelectItem> Items;

    public int MaxCoupleCount;
    public int CurrentCoupleCount;

    public GameObject GameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        Items = new List<SelectItem>();
        CurrentCoupleCount = MaxCoupleCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        SelectItem item = other.gameObject.GetComponent<SelectItem>();
        if (item != null)
        {
            Items.Add(item);

            if (Items.Count > 1)
                CheckItems();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SelectItem item = other.gameObject.GetComponent<SelectItem>();
        if (item != null)
        {
            Items.Remove(item);
        }
    }


    private void CheckItems()
    {
        SelectItem item0 = Items[0];
        SelectItem item1 = Items[1];

        if (item0.ItemType == item1.ItemType)
        {
            Items.Remove(item0);
            Items.Remove(item1);

            Destroy(item0.gameObject);
            Destroy(item1.gameObject);

            CurrentCoupleCount -= 1;
            CheckGameState();
        }
    }

    private void CheckGameState()
    {
        if (CurrentCoupleCount == 0)
        {
            GameOverScreen.SetActive(true);
        }
    }

    public void PlayAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
