using UnityEngine;

[ExecuteInEditMode]
public class FieldGenerator : MonoBehaviour {

    
    int Width = 18;
    int Height = 10;

   /* void Awake()
    {
        
        for (int y = -Height / 2; y <= Height / 2 - 1; y++)
            for (int x = -Width / 2; x <= Width / 2 - 1; x++)
            {
                GameObject field = new GameObject { name = "Field " + (x + Width / 2).ToString() + " " + (y + Height / 2).ToString() };
                field.transform.SetParent(transform);
                Field fieldScript = field.AddComponent<Field>();

            }

    }*/
}
