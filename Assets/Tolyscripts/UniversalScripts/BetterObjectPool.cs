
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class BetterObjectPool : UdonSharpBehaviour
{
    [SerializeField] private GameObject[] managedGameObjects;
    [SerializeField] private GameObject[] clientScripts;

    [Header("set these to have the same size as the pool")]
    [UdonSynced][SerializeField] private bool[] isActive;
    [UdonSynced][SerializeField] private int[] PlayerIDs;
    [UdonSynced][SerializeField] private int[] ScriptIDs;

    private void Start()
    {
        //find the udonbehaviour on every clientScript object and give them an ID starting from 1
        for (int i = 0; i < clientScripts.Length; i++)
        {
            clientScripts[i].GetComponent<UdonBehaviour>().SetProgramVariable("ScriptID", i + 1);
        }
        //set every gameobject to not be active
        for (int i = 0; i < managedGameObjects.Length; i++)
        {
            managedGameObjects[i].SetActive(false);
            isActive[i] = false;
        }
    }
    public void PlayerCancelPreOrders()
    {
        int fakePlayerID = Networking.LocalPlayer.playerId + 1;
        //find every instance of the fake player ID in playerIDs and set it to 0 if the object is not active already
        for (int i = 0; i < PlayerIDs.Length; i++)
        {
            if (PlayerIDs[i] == fakePlayerID && !isActive[i])
            {
                PlayerIDs[i] = 0;
            }
        }
        //send changes to the network
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        RequestSerialization();

    }
    public GameObject[] PlayerPreOrderMultiple(int amount)
    {
        GameObject[] returnArray = new GameObject[amount];
        int fakePlayerID = Networking.LocalPlayer.playerId + 1;
        //find every instance of the fakePlayerID in PlayerIDs and set it to 0
        for (int i = 0; i < PlayerIDs.Length; i++)
        {
            if (PlayerIDs[i] == fakePlayerID && !isActive[i])
            {
                PlayerIDs[i] = 0;
            }
        }
        //find an unclaimed gameobject
        for (int j = 0; j < returnArray.Length; j++)
        {
            for (int i = 0; i < managedGameObjects.Length; i++)
            {
                if (PlayerIDs[i] == 0 && ScriptIDs[i] == 0)
                {
                    //object is unclaimed
                    
                    returnArray[j] = managedGameObjects[i];
                    PlayerIDs[i] = fakePlayerID;
                    break;
                }
            }
        }
        //validate the contents of the return array
        for (int i = 0; i < returnArray.Length; i++)
        {
            if (returnArray[i] == null)
            {
                Debug.LogError("Not enough objects in the pool to satisfy request");
                //clear the fake player ID from PlayerIDs again
                for (int j = 0; j < PlayerIDs.Length; j++)
                {
                    if (PlayerIDs[j] == fakePlayerID)
                    {
                        PlayerIDs[j] = 0;
                    }
                }
                return null;
            }
        }
        Debug.Log("Requested " + amount + " objects");
        //send changes to the network
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        RequestSerialization();
        return returnArray;
    }
    
    public GameObject PlayerPreOrderSingle()
    {
        int fakePlayerID = Networking.LocalPlayer.playerId + 1;
        //find an unclaimed gameobject
        for (int i = 0; i < managedGameObjects.Length; i++)
        {
            if (PlayerIDs[i] == 0 && ScriptIDs[i] == 0)
            {
                //object is unclaimed
                PlayerIDs[i] = fakePlayerID;
                //send changes to the network
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
                RequestSerialization();
                return managedGameObjects[i];
            }
        }
        Debug.Log("not enough objects in the pool to satisfy request");
        return null;
    }
    public void PlayerReturnAllOrderedObjects()
    {
        int fakePlayerID = Networking.LocalPlayer.playerId + 1;
        //find all managed objects that are active and claimed by the player and return them
        for (int i = 0; i < PlayerIDs.Length; i++)
        {
            if (PlayerIDs[i] == fakePlayerID)
            {
                managedGameObjects[i].SetActive(false);
                isActive[i] = false;
                PlayerIDs[i] = 0;
                //send changes to network
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
                RequestSerialization();
            }
        }
    }
    public GameObject[] ScriptPreOrderMultiple(int scriptID, int amount)
    {
        GameObject[] returnArray = new GameObject[amount];
        
        //find every instance of the fakeScriptID in ScriptIDs and set it to 0
        for (int i = 0; i < ScriptIDs.Length; i++)
        {
            if (ScriptIDs[i] == scriptID && !isActive[i])
            {
                ScriptIDs[i] = 0;
            }
        }
        //find an unclaimed gameobject
        for (int j = 0; j < amount; j++)
        {
            for (int i = 0; i < managedGameObjects.Length; i++)
            {
                if (PlayerIDs[i] != 0 && ScriptIDs[i] != 0)
                {
                    //object is unclaimed
                    returnArray[j] = managedGameObjects[i];
                    ScriptIDs[i] = scriptID;
                }
            }
        }
        //validate the contents of the return array
        for (int i = 0; i < returnArray.Length; i++)
        {
            if (returnArray[i] == null)
            {
                Debug.LogError("Not enough objects in the pool to satisfy request");
                //clear the fake script ID from ScriptIDs again
                for (int j = 0; j < ScriptIDs.Length; j++)
                {
                    if (ScriptIDs[j] == scriptID)
                    {
                        ScriptIDs[j] = 0;
                    }
                }
                return null;
            }
        }
        //send changes to the network
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        RequestSerialization();
        return returnArray;
    }
    public GameObject ScriptPreOrderSinguler(int scriptID)
    {
        //find an unclaimed gameobject
        for (int i = 0; i < managedGameObjects.Length; i++)
        {
            if (PlayerIDs[i] == 0 && ScriptIDs[i] == 0 && !isActive[i])
            {
                //object is unclaimed
                ScriptIDs[i] = scriptID;
                //send changes to the network
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
                RequestSerialization();
                return managedGameObjects[i];
            }
        }
        Debug.Log("not enough objects in the pool to satisfy request");
        return null;
    }
    
    
    public void FulfillAllPreOrders()
    {
        //find a claimed gameobject
        for (int i = 0; i < managedGameObjects.Length; i++)
        {
            if (PlayerIDs[i] != 0)
            {
                //object is claimed by a player
                //get the player's API and check if they're valid
                VRCPlayerApi player = VRCPlayerApi.GetPlayerById(PlayerIDs[i]-1);
                if(Utilities.IsValid(player))
                {
                    //transfer ownership to the other player
                    Networking.SetOwner(player, managedGameObjects[i]);
                    isActive[i] = true;
                    managedGameObjects[i].SetActive(true);
                }
            }
            if (ScriptIDs[i] != 0)
            {
                //object is claimed by a script
                isActive[i] = true;
                managedGameObjects[i].SetActive(true);
            }
        }


        //make remove duplicate numbers from scriptIDs and put the result in a seperate array
        int[] uniqueScriptIDs = new int[ScriptIDs.Length];
        int uniqueScriptIDCount = 0;
        for (int i = 0; i < ScriptIDs.Length; i++)
        {
            if (ScriptIDs[i] != 0)
            {
                bool isUnique = true;
                for (int j = 0; j < uniqueScriptIDCount; j++)
                {
                    if (uniqueScriptIDs[j] == ScriptIDs[i])
                    {
                        isUnique = false;
                    }
                }
                if (isUnique)
                {
                    uniqueScriptIDs[uniqueScriptIDCount] = ScriptIDs[i];
                    uniqueScriptIDCount++;
                }
            }
        }
        //for each unique scriptID, find the corresponding udonbehaviour and send an event telling it to recieve the objects
        for (int i = 0; i < uniqueScriptIDCount; i++)
        {
            for (int j = 0; j < managedGameObjects.Length; j++)
            {
                if (ScriptIDs[j] == uniqueScriptIDs[i])
                {
                    //find the script and call its spawn function
                    clientScripts[j].GetComponent<UdonBehaviour>().SendCustomEvent("OrderFulfilled");
                }
            }
        }
        //send changes to the network
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        RequestSerialization();
        
    }
    public GameObject PlayerTryToSpawn()
    {
        int fakePlayerID = Networking.LocalPlayer.playerId + 1;
        //find an unclaimed gameobject
        for (int i = 0; i < managedGameObjects.Length; i++)
        {
            if (PlayerIDs[i] == 0 && ScriptIDs[i] == 0)
            {
                //object is unclaimed
                PlayerIDs[i] = fakePlayerID;
                isActive[i] = true;
                managedGameObjects[i].SetActive(true);
                //send changes to the network
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
                RequestSerialization();
                return managedGameObjects[i];
            }
        }
        return null;
    }
    public GameObject ScriptTryToSpawn(int ScriptID)
    {
        //find an unclaimed gameobject
        for (int i = 0; i < managedGameObjects.Length; i++)
        {
            if (PlayerIDs[i] == 0 && ScriptIDs[i] == 0)
            {
                //object is unclaimed
                ScriptIDs[i] = ScriptID;
                isActive[i] = true;
                managedGameObjects[i].SetActive(true);
                //send changes to the network
                Networking.SetOwner(Networking.LocalPlayer, gameObject);
                RequestSerialization();
                return managedGameObjects[i];
            }
        }
        return null;
    }

    public void ReturnObject(GameObject objectToReturn)
    {
        //find the index of the object in the managedGameObjects array
        int index = -1;
        for (int i = 0; i < managedGameObjects.Length; i++)
        {
            if (managedGameObjects[i] == objectToReturn)
            {
                index = i;
                break;
            }
        }
        //if the index is not found, return
        if (index == -1)
        {
            Debug.LogError("Object not found in the pool");
            return;
        }
        //set the object to inactive
        isActive[index] = false;
        //set the playerID to 0
        PlayerIDs[index] = 0;
        //set the scriptID to 0
        ScriptIDs[index] = 0;
        //send changes to the network
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        RequestSerialization();
    }
    
    public void ReturnAllObjects()
    {
        //set all objects to inactive
        for (int i = 0; i < managedGameObjects.Length; i++)
        {
            isActive[i] = false;
            PlayerIDs[i] = 0;
            ScriptIDs[i] = 0;
        }
        //send changes to the network
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        RequestSerialization();
    }
    public void OnDeserialization()
    {
        for (int i = 0; i < managedGameObjects.Length; i++)
        {
            managedGameObjects[i].SetActive(isActive[i]);
        }
    }
}
