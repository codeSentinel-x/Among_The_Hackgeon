using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Transform _bulletDisplayContent;
    public Transform _weaponDisplayContent;
        public static PlayerUI _I;

    void Awake() {
        _I = this;
        _bulletDisplayContent = transform.Find("BULLET_DISPLAY");
        _weaponDisplayContent = transform.Find("WEAPON_DISPLAY");
    }
    public void DecaresBullet(int val = 1) {
        List<GameObject> toDestr = new();
        for (int i = 0; i < val; i++) {
            toDestr.Add(_bulletDisplayContent.GetChild(i).gameObject);
        }
        if (toDestr.Count > 0) {
            foreach (GameObject obj in toDestr) {
                Destroy(obj);
            }
        }
    }
    public void IncreaseBullet(int val = 1) {
        for (int i = 0; i < val; i++) {
            RectTransform g = new GameObject("bullet", typeof(RectTransform)).GetComponent<RectTransform>();
            g.SetParent(_bulletDisplayContent, false);
            g.gameObject.AddComponent<Image>().sprite = GameDataManager._I._bulletSprite;
        }
    }
    public void ResetBullets(int val) {
        DecaresBullet(_bulletDisplayContent.childCount);
        IncreaseBullet(val);
    }
    public void ChangeWeapon(Sprite sprite){
        _weaponDisplayContent.Find("WEAPON_IMAGE").GetComponent<Image>().sprite = sprite; 
    }
    public IEnumerator DisplayReload(float time){
        for(int i = 0; i < time; i += 40){
            yield return new  WaitForSeconds(time/40);
            i 
        }
    }
}
