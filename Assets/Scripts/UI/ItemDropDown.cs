using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public class ItemDropDown : StaticUI
{
  public Item Item { set { _item = value; }get { return _item; } }
  public GameObject buttonPrefab;
  public float padding;

  private UIItem uIItem;
  private Item _item;
  private RectTransform rectTransform;

  protected override void Awake()
  {
    base.Awake();
    uIItem = GetComponentInParent<UIItem>();
 
    rectTransform = GetComponent<RectTransform>();
    transition = Transition.None;
  }

  // Start is called before the first frame update
  protected override void Start(){
    base.Start();

    InitSize();
    InitUsageButtons();

    GetAllGraphics();
    SetUIgraphics(false);
  }

  // Update is called once per frame
  void Update()
  {

  }

  #region Init

  private void InitSize() {

    RectTransform brect = buttonPrefab.GetComponent<RectTransform>();
    float bWidth = brect.sizeDelta.x;
    float bHeight = brect.sizeDelta.y;

    Vector2 newSize =  new Vector2();
    newSize.x = bWidth + (2 * padding);
    newSize.y = (bHeight + (2 * padding)) * _item.usages.Count;
  
    rectTransform.sizeDelta = newSize;
  }

  private void InitUsageButtons() {

    int i = 0;

    foreach (KeyValuePair<string,Item.Usage> ud in _item.usages) {

      Item.Usage u = ud.Value;
      string name = ud.Key;

      InitButton(u,name, i);
      i++;
    }

  }

  private void InitButton(Item.Usage u, string name, int i) {
    GameObject b = Instantiate(buttonPrefab);
    RectTransform brect = b.GetComponent<RectTransform>();
    Button button = b.GetComponent<Button>(); 

    //Set button transform
    brect.SetParent(rectTransform);
    Vector3 newPos = new Vector3(0, 0, brect.localPosition.z);
    newPos.x += padding;
    newPos.y -= padding + (i * brect.sizeDelta.y);

    brect.transform.localPosition = newPos;


    //Set Button onClick listener
    button.onClick.AddListener(() => u(new object[] {null,uIItem}));

    button.GetComponentInChildren<Text>().text = name;

  }

  #endregion

  #region Public Methods

  public override void OnPointerExit(PointerEventData data){
    base.OnPointerExit(data);

    SetUIgraphics(false);
  }

  #endregion

}
