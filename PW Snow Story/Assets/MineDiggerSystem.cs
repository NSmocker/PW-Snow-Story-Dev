using UnityEngine;

public class MineDiggerSystem : MonoBehaviour
{

    public CharacterController characterController;
    public Animator animator;
    public CharacterMovement characterMovement;
    public AnimationSystem animationSystem;
    public InventorySystem inventorySystem;
    public float radius = 5.0f;
    public GameObject mineItem;
    public bool canMine,isMining;
    public float miningProgress;
    public float miningStartTime;
    public float miningEndTime;
    public GameObject tooltip;

    public KeyCode mineKey = KeyCode.E;
    public GameObject walkingCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void CheckItemsForMineUpdate()
    {
        if(isMining)
        {
            tooltip.SetActive(false); 
            return;
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
           if(hitCollider.gameObject.tag == "Mine" && !animationSystem.weaponIsOn)
            {
                
                mineItem = hitCollider.gameObject;
                canMine = true;
                tooltip.SetActive(true);
                return;
                  
            }
            else
            {
                 mineItem=null;
                 canMine = false;
                 tooltip.SetActive(false);
            }
           
        }
    }
    void StartMining()
    {   
        print("TODO Interface Display");
        canMine = false;
        isMining = true;
        characterMovement.enabled = false;
        animationSystem.enabled = false;
        animator.Play("Mining");
        var mineItemBihaviour = mineItem.GetComponent<MineItemBihaviour>();
        miningEndTime = mineItemBihaviour.miningTime;
        walkingCamera.SetActive(false);
        mineItemBihaviour.cameraLink.SetActive(true);
        characterController.enabled = false;
        characterController.gameObject.transform.position = mineItemBihaviour.digPositioin.transform.position;
        characterController.gameObject.transform.rotation = mineItemBihaviour.digPositioin.transform.rotation;
        


    }
    void EndMining()
    {
        canMine = false;
        isMining = false;
        miningProgress = 0;
        miningEndTime = 0;
        miningStartTime = 0;
        characterMovement.enabled = true;
        animationSystem.enabled=true;
        characterController.enabled = true;
        mineItem.GetComponent<MineItemBihaviour>().cameraLink.SetActive(false);
        walkingCamera.SetActive(true);
        var inventoryItemToAdd = mineItem.GetComponent<MineItemBihaviour>().inventoryItem;
        AddMineItemToInventory(inventoryItemToAdd);
        Destroy(mineItem.gameObject);

    }
    void AddMineItemToInventory(GameObject item)
    {   
        inventorySystem.AddItem(item);
        print("CheckInventory");
    }
    void MineProccesUpdate()
    {
        if (isMining)
        {
            miningProgress += Time.deltaTime;
            if(miningProgress >= miningEndTime)
            {
                EndMining();
            }
        }
    }
    void UserInputBingingUpdate()
    {
        if (Input.GetKeyDown(mineKey) && canMine)
        {
            StartMining();
        }
    }
    // Update is called once per frame
    void Update()
    {
      canMine = animationSystem.weaponIsOn;
      CheckItemsForMineUpdate();
      UserInputBingingUpdate();
      MineProccesUpdate();
    }
}
