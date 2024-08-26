using MyUtils.Functions;
using MyUtils.Interfaces;
using UnityEngine;

public class Chest : MonoBehaviour, IDamageable {
    public Transform _spawnParticle;
    public Transform _openParticle;
    public bool _chestWithKey;
    void Awake() {
        _ = Instantiate(_spawnParticle, transform.position, Quaternion.identity);
    }
    public void Damage(float v) {
        Open();
    }
    public void Open() {
        _ = Instantiate(_openParticle, transform.position, Quaternion.identity);
        if (_chestWithKey) _chestWithKey = Random.Range(0f, 1f) > 0.66f;
        if (!_chestWithKey) {
            _ = Instantiate(MyRandom.GetFromArray<Transform>(GameDataManager._I._weaponsPrefab), transform.position + new Vector3(Random.Range(1f, 3f), 0), Quaternion.identity);
            _ = Instantiate(MyRandom.GetFromArray<Transform>(GameDataManager._I._specialItemPrefab), transform.position + new Vector3(Random.Range(1f, -3f), 0), Quaternion.identity);
            _ = Instantiate(MyRandom.GetFromArray<Transform>(GameDataManager._I._UtilityItemPrefab), transform.position + new Vector3(Random.Range(1f, -3f), 2), Quaternion.identity);
            _ = Instantiate(MyRandom.GetFromArray<Transform>(GameDataManager._I._UtilityItemPrefab), transform.position + new Vector3(Random.Range(1f, -3f), 2), Quaternion.identity);
        } else {
            _ = Instantiate(GameDataManager._I._bossKeyPrefab, transform.position, Quaternion.identity);
        }
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
