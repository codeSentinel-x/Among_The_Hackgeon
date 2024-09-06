using MyUtils.Functions;
using MyUtils.Interfaces;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable {

    public bool _chestWithKey;
    void Awake() {
        ParticleAssetManager._I.InstantiateParticles(ParticleType.ChestSpawn, transform.position);
    }
    public void Interact() {
        Open();
    }
    public void Open() {
        ParticleAssetManager._I.InstantiateParticles(ParticleType.ChestOpen, transform.position);
        AudioManager._I.PlaySoundEffect(AudioType.DoorOpen, transform.position);
        if (!PlayerController._hasKey) _chestWithKey = Random.Range(0f, 1f) > 0.66f;
        if (!_chestWithKey) {
            _ = Instantiate(MyRandom.GetFromArray<Transform>(AssetManager._I._weaponsPrefab), transform.position + new Vector3(Random.Range(1f, 3f), 0), Quaternion.identity);
            _ = Instantiate(MyRandom.GetFromArray<Transform>(AssetManager._I._specialItemPrefab), transform.position + new Vector3(Random.Range(1f, -3f), 0), Quaternion.identity);
            _ = Instantiate(MyRandom.GetFromArray<Transform>(AssetManager._I._UtilityItemPrefab), transform.position + new Vector3(Random.Range(1f, -3f), 2), Quaternion.identity);
            _ = Instantiate(MyRandom.GetFromArray<Transform>(AssetManager._I._UtilityItemPrefab), transform.position + new Vector3(Random.Range(1f, -3f), 2), Quaternion.identity);
        } else {
            _ = Instantiate(AssetManager._I._bossKeyPrefab, transform.position, Quaternion.identity);
        }
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
