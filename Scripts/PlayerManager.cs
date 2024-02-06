using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Rigidbody RB;//Give RigidBody

    [SerializeField]
    GameObject BallWinPanel, BallLosePanel;//Lose And Win Panel

    [SerializeField]
    float Speed;//Speed For Ball Jump

    bool flag;//Flag For Ball Jump

    public static GameObject ThisObject;//Make A Static Player Object

    public static PlayerManager instancePlayer;//Make A Public Script

    int counter  = 0;//Counter For Sprite Name

    string Count;//Count For Sprite Name

    GameObject NewSpriteObject;

    //[SerializeField]
    //GameObject ParentObject;//NewSpriteObject For Generate A Sprite

    [SerializeField]
    float power, Radius;//Explosion Method


    [SerializeField]
    Sprite[] SpriteArray;

    [SerializeField]
    ParticleSystem MyParticle1;
    

 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        
        ThisObject = this.gameObject;
        //Give RigidBody To Player
        RB = GetComponent<Rigidbody>();
    }
 /////////////////////////////////////////////  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void OnCollisionEnter(Collision collision)
    {
        MyParticle1.Play();
        ///////////////////////////////////////
        //         Player Jump Method        //
        ///////////////////////////////////////
        if (!flag)
        {

            //RB.AddForce(Vector3.up * Speed, ForceMode.Impulse);
            transform.DOMove(new Vector3(this.transform.position.x , this.transform.position.y + 1f , this.transform.position.z) , 0.4f);
            flag = true;
            Invoke("WaitingMovement", 0.5f);
        }
        ///////////////////////////////////////
        //         Player Lose Method        //
        ///////////////////////////////////////
        if (collision.gameObject.tag == "BadBlock")
        {
            Destroy(RB);
            BallLosePanel.SetActive(true);
            BallWinPanel.SetActive(false);
        }
        ///////////////////////////////////////
        //         Player Win Method        //
        ///////////////////////////////////////
        if (collision.gameObject.tag == "LastBlock")
        {
            Destroy(RB);

            BallWinPanel.SetActive(true);
            BallLosePanel.SetActive(false);
        }
        ///////////////////////////////////////
        //     Player Sprite Give Method     //
        ///////////////////////////////////////
       
            //Give No To Sprites
            counter++;
            Count = counter.ToString();
            NewSpriteObject = new GameObject(Count);
            // Generate Random Value For Sprite
            int Temp = Random.Range(0, SpriteArray.Length);
            //Position Of Sprite
            NewSpriteObject.transform.position = new Vector3(0, this.gameObject.transform.position.y - 0.4462633f, this.gameObject.transform.position.z + 1.65f);
            NewSpriteObject.gameObject.transform.Translate(0.064f, 0.114f, -1.641f);
            //Selected Random Sprite
            NewSpriteObject.AddComponent<SpriteRenderer>().sprite = SpriteArray[Temp];
        ////Color To Sprite
        //NewSpriteObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        NewSpriteObject.GetComponent<SpriteRenderer>().color = new Color(0.1215686f, 0.6039216f, 0.5490196f);
        //Scale of Sprite
        NewSpriteObject.gameObject.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
            NewSpriteObject.gameObject.transform.DOScale(new Vector3(0.005f, 0.005f, 0.005f), 0.1f);
            // Rotation Of Sprite
            NewSpriteObject.gameObject.transform.Rotate(90f, 0, 0);
            NewSpriteObject.gameObject.transform.parent = collision.gameObject.transform;
            //Wait For Destroyed
            Invoke("WaitingMovement", 0.6f);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //for (int i = 1; i <= Counter3; i++)
            //{
            //    if (i == 1) 
            //    {
            //        transform.DOMoveY(this.gameObject.transform.position.y + 2.2f, 03f);
            //        GameObject SP = Instantiate(Sprite1, new Vector3(0, 0.3f, gameObject.transform.position.z), Quaternion.Euler(90, 0, 0), collision.gameObject.transform);
            //        Destroy(SP, 0.45f);
            //    }
            //    if (i == 2)
            //    {
            //        transform.DOMoveY(this.gameObject.transform.position.y + 2.2f, 03f);
            //        GameObject SP = Instantiate(Sprite2, new Vector3(0, 0.3f, gameObject.transform.position.z), Quaternion.Euler(90, 0, 0), collision.gameObject.transform);
            //        Destroy(SP, 0.45f);
            //    }
            //    if (i == 3)
            //    {
            //        transform.DOMoveY(this.gameObject.transform.position.y + 2.2f, 03f);
            //        GameObject SP = Instantiate(Sprite3, new Vector3(0, 0.3f, gameObject.transform.position.z), Quaternion.Euler(90, 0, 0), collision.gameObject.transform);
            //        Destroy(SP, 0.45f);
            //    }
            //}
    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void OnTriggerEnter(Collider other)
    {
        foreach (Transform child in other.transform)
        {
            child.transform.gameObject.GetComponent<MeshCollider>().convex = true;
            child.transform.gameObject.AddComponent<Rigidbody>();
            child.transform.gameObject.GetComponent<Rigidbody>().AddExplosionForce(power, child.transform.position, Radius, 0.1f);
            Destroy(child.transform.gameObject, 0.5f);

        }
    } 
 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void RetryButton()
    {
        ///////////////////////////////////////
        //       New Scene Load Method       //
        ///////////////////////////////////////
        SceneManager.LoadScene(1);
    }
 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WaitingMovement()
    {
        ///////////////////////////////////////
        //          Waiting Method           //
        ///////////////////////////////////////
        flag = false;
        Destroy(NewSpriteObject);
    }
 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Update()
    {
        ///////////////////////////////////////
        //          Camera Method            //
        ///////////////////////////////////////
        float cameraOffset = Camera.main.transform.position.y - 4;
            float PlayerOffset = (this.gameObject.transform.position.y );
            if (PlayerOffset < cameraOffset)
            {
                Vector3 Position = new Vector3(Camera.main.transform.position.x, transform.position.y + 4, Camera.main.transform.position.z);
                Camera.main.transform.position = Position;
            }
       
    }
 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
