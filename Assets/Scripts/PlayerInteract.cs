using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Transform gunHolder; // Gunholder của nhân vật
    private GameObject equippedWeapon; // Vũ khí được trang bị
    public GunInven guninven;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void EquipWeapon(GameObject weapon)
    {
        // Đặt vũ khí làm con của gunHolder và đặt lại vị trí và góc quay
        weapon.transform.SetParent(gunHolder);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        // Kích hoạt vũ khí nếu nó bị vô hiệu hóa
        weapon.SetActive(true);
        weapon.GetComponent<Shoot>().enabled = true;

        // Chuyển đổi List<GameObject> thành GameObject[]
        GameObject[] bulletsArray = weapon.GetComponent<Magazin>().Bullets.ToArray();
        guninven.GetFirstGun(bulletsArray, weapon);
        
        // Gán vũ khí được trang bị
        equippedWeapon = weapon;
    }

    public void UnequipCurrentWeapon()
    {
        if (equippedWeapon != null)
        {
            // Tắt component Shoot nếu có
            equippedWeapon.GetComponent<Shoot>().enabled = false;

            // Xóa vũ khí khỏi gunHolder và thiết lập lại vị trí và góc quay ban đầu
            equippedWeapon.transform.SetParent(null);
            equippedWeapon.SetActive(false);

            // Đặt lại vũ khí được trang bị
            equippedWeapon = null;
        }
    }

    public GameObject GetEquippedWeapon()
    {
        return equippedWeapon;
    }
}
