using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject _weaponHolder;
    [SerializeField] GameObject _weapon = null;
    [SerializeField] GameObject _weaponSheath;

    //GameObject _currentWeaponInHand;
    //GameObject _currentWeaponInSheath;

    public GameObject MyWeapon { get => _weapon; set => _weapon = value; }
    //public GameObject MyCurrentWeaponInHand { get => _currentWeaponInHand; set => _currentWeaponInHand = value; }
    //public GameObject MyCurrentWeaponInSheath { get => _currentWeaponInSheath; set => _currentWeaponInSheath = value; }

    private void Start()
    {
        
    }

    public void EquipWeapon(GameObject weapon)
    {
        _weapon = Instantiate(weapon, _weaponHolder.transform);
    }
    //public void DrawCurrentWeapon()
    //{
        
        
    //        if (_currentWeaponInHand == null)
    //        {
    //            _currentWeaponInHand = Instantiate(_weapon, _weaponHolder.transform);
    //            Destroy(_currentWeaponInSheath);
    //        }
        
        
    //}

    //public void SheathCurrentWeapon()
    //{
    //    if (_currentWeaponInSheath == null)
    //    {
           
    //            _currentWeaponInSheath = Instantiate(_weapon, _weaponSheath.transform);
    //            Destroy(_currentWeaponInHand);
           
            
    //    }
    //}
    public void DestroyWeapon()
    {
        Destroy(_weapon);
       
    }
}
