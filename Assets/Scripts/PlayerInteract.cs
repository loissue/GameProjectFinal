using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Transform gunHolder; // Gunholder của nhân vật
    private GameObject equippedWeapon; // Vũ khí được trang bị
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EquipWeapon(GameObject weapon)
    {
        weapon.GetComponent<Shoot>().enabled = true;
        // Đặt vũ khí làm con của gunHolder và đặt lại vị trí và góc quay
        weapon.transform.SetParent(gunHolder);
        weapon.transform.localPosition = gunHolder.localPosition;
        weapon.transform.localRotation = Quaternion.identity;

        // Kích hoạt vũ khí nếu nó bị vô hiệu hóa
        weapon.SetActive(true);

        // Gán vũ khí được trang bị
        equippedWeapon = weapon;
    }
}
