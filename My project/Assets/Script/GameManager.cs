using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI clearText;

    // Item‚ð”z—ñ‚ÅŠÇ—
    public Item[] items;

    private int collectedItems = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        clearText.gameObject.SetActive(false);
        UpdateUI();
    }

    public void CollectItem()
    {
        collectedItems++;

        UpdateUI();

        if (collectedItems >= items.Length)
        {
            clearText.gameObject.SetActive(true);
        }
    }

    void UpdateUI()
    {
        countText.text = collectedItems + " / " + items.Length;
    }
}