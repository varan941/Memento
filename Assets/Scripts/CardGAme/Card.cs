using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] float hitDistance;
    [SerializeField] AField currField;

    private void Start()
    {
        currField = transform.parent.GetComponent<AField>();
    }


    private void FixedUpdate()
    {
       

    }

    

    public void TrySetField()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection((Vector3.forward) * hitDistance), Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, hitDistance))
        {
            Debug.Log("Did Hit");
            var field = hit.transform.GetComponent<AField>();
            
            if (field != null)
            {
                currField = field;
                field.SetCard(this);
                field.AlignCards();
            }
            else
            {
                field.AlignCards();
            }            
        }
        else
            currField?.AlignCards();

    }
}
