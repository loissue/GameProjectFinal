using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public BulletList[] BulletLists;
    public GameObject inventoryCanvas; // Tham chiếu đến Canvas Inventory
    // public GameObject inventorySlotPrefab; // Prefab cho ô inventory
    // public Transform inventoryGrid; // Grid Layout Group trong Panel

    [System.Serializable]
    public class BulletList
    {
        public GameObject Bullet;
    }

    public List<BulletList> inventory = new List<BulletList>(); // Danh sách chứa các item được nhặt

    private void Start()
    {
        inventoryCanvas.SetActive(false);
    }

    void Update()
    {
        
        // UpdateInventoryUI();
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();

        }
    }

    void ToggleInventory()
    {
        if (inventoryCanvas != null)
        {
            
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
            if (inventoryCanvas.activeSelf)
            {
                
                // UpdateInventoryUI();
                
            }
        }
        else
        {
            Debug.LogWarning("Inventory Canvas is not assigned in the Inspector.");
        }
    }
    
    
}
