using UnityEngine;
using UnityEngine.Events;

public class FlaskPickup : MonoBehaviour
{
    #region Variables
    public string flaskName;
    public Sprite flaskSprite;
    public UnityEvent onPlayerEnterTrigger;
    public UnityEvent onPlayerExitTrigger;
    public PlayerInventory inventory;
    public GameObject ePanel;

    [SerializeField] private InventoryBarToggler inventoryBarToggler;

    private bool _isPlayerInRange = false;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
            onPlayerEnterTrigger.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
            onPlayerExitTrigger.Invoke();
        }
    }

    private void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E) && inventory != null)
        {
            FlaskPickup flaskData = GetComponent<FlaskPickup>();
            if (flaskData != null)
            {
                bool added = inventory.AddFlask(gameObject, flaskData.flaskSprite);
                if (added)
                {
                    Debug.Log($"{flaskData.flaskName} picked up and added to inventory.");
                }
                else
                {
                    Debug.Log("Inventory is full!");
                }
            }

            if (!inventoryBarToggler.inventoryBarOpen)
            {
                inventoryBarToggler.UpdateInventoryBarState(true);
                Invoke("HideInventoryBar", 3f); // Schedule hiding the inventory bar after 3 seconds
            }
        }
    }

    #region Public Functions
    public void EForPickupToggler()
    {
        if (ePanel != null)
        {
            ePanel.SetActive(!ePanel.activeSelf);
        }
    }

    public void HideInventoryBar()
    {
        if (inventoryBarToggler.inventoryBarOpen)
        {
            inventoryBarToggler.UpdateInventoryBarState(false); // Hide the inventory bar
        }
    }
    #endregion
}
