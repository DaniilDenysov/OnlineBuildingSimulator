using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IDropHandler
{
    public Build objectData;
    private RectTransform rectTransform;
    public Canvas canvas;
    private Vector3 lastPosition;
    private CanvasGroup canvasGroup;
    private BuildManager buildManager;
    public TMP_Text itemName;
    bool EnoughResources = true;

    void Start ()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        lastPosition = rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
        buildManager = FindObjectOfType<BuildManager>();
        itemName.text = objectData.name;
        gameObject.name = objectData.name;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //  rectTransform.position = Curso
        int[] resourceList = new int [buildManager.ResourcesData.Count];
       
        //int[] playerResourceList = new int[buildManager.ResourcesData.Count];
        for (int i = 0; i < objectData.resource.Length - 1; i++)
        {
            resourceList[objectData.resource[i].ID] += 1;
            Debug.Log("Res: " + resourceList[objectData.resource[i].ID]);
        }

    
        for (int i = 0; i <= buildManager.ResourcesData.Count - 1; i++)
        {
            if (buildManager.ResourcesData[i].ResourceCount >= resourceList[i])
            {
                Debug.Log(buildManager.ResourcesData[i].ResourceCount + " - " + resourceList[i]);
                EnoughResources = true;
                continue;
            }
            else
            {
                Debug.Log("Not enough!");
                EnoughResources = false;
                break;
            }
        }
        if (EnoughResources)
        {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (EnoughResources)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider.gameObject.layer == 3 && FindObjectOfType<BuildManager>().temp == null)
                {
                    GameObject reference = Instantiate(objectData.Reference, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                    FindObjectOfType<BuildManager>().temp = reference;
                    reference.GetComponent<Foundation>().Properties = objectData;
                    if (!reference) BackOnSpot();
                    else
                    {
                        GetComponent<Image>().enabled = false;
                        itemName.enabled = false;
                    }
                }
                /*  else
                  {
                      BackOnSpot();
                  }*/
            }
        }
        else
        {
            BackOnSpot();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        BackOnSpot();
        GetComponent<Image>().enabled = true;
        itemName.enabled = true;
        //     if (buildManager.temp != null) Destroy(this.gameObject);

        //  canvasGroup.blocksRaycasts = true;
    }



    public void OnDrop(PointerEventData eventData)
    {
       // BackOnSpot();
        //GetComponent<Image>().enabled = true;
        //rectTransform.anchoredPosition = eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition;
    }

    public void BackOnSpot ()
    {
        rectTransform.anchoredPosition = lastPosition;
    }

}
