
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FakeGenerator : UdonSharpBehaviour
{
    public GameObject Template;
    public Transform contentParent;
    public int amountToGenerate;
    void Start()
    {
        GenerateList();
    }

    public void GenerateList()
    {
        for (int i = 0; i < amountToGenerate; i++)
        {
            GameObject newThing = VRCInstantiate(Template);

            newThing.transform.position = Template.transform.position;
            newThing.transform.rotation = Template.transform.rotation;
            newThing.transform.SetParent(contentParent);
            newThing.transform.localScale = Template.transform.localScale;
            newThing.SetActive(true);
        }
    }
}
