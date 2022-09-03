
using UnityEngine;
[CreateAssetMenu(fileName = "Building Parameters", menuName = "Build")]
public class Build : ScriptableObject
{
    public int ID;
    public GameObject Prefarb;
    public GameObject Reference;
    public Resource [] resource; 
}
