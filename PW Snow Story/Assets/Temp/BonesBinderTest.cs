using UnityEngine;

public class BonesBinderTest : MonoBehaviour
{
      // Встанови посилання на root bone (наприклад, "Hips" або "Armature")
    public Transform rootBone;

    // Масив усіх кісток, до яких має бути прив'язаний меш
    public Transform[] bones;

    // Сам меш (Mesh object)
    public Mesh skinnedMesh;

    void Start()
    {
        SkinnedMeshRenderer smr = gameObject.AddComponent<SkinnedMeshRenderer>();
        
        // Призначаємо меш
        smr.sharedMesh = skinnedMesh;

        // Призначаємо root bone
        smr.rootBone = rootBone;

        // Призначаємо всі кістки
        smr.bones = bones;

        // За бажанням: включити оновлення при паузі
        smr.updateWhenOffscreen = true;
    }
}
