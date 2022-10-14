using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script to manage all of the things a planet needs. Namely:

 */


public class PlanetScript : MonoBehaviour
{
    //Tile positioning magic numbers
    public static Vector3[] Positions={new Vector3(0f, -0.7423442429410712f, -0.28355026945067996f), new Vector3(0f, 0.7423442429410713f, 0.28355026945067996f), new Vector3(0f, -0.7423442429410712f, 0.28355026945067996f), new Vector3(0f, 0.7423442429410713f, -0.28355026945067996f), new Vector3(-0.4587939734903912f, -0.4587939734903912f, -0.4587939734903912f), new Vector3(-0.4587939734903912f, 0.45879397349039114f, 0.45879397349039114f), new Vector3(-0.4587939734903912f, -0.4587939734903912f, 0.45879397349039114f), new Vector3(-0.4587939734903912f, 0.45879397349039114f, -0.4587939734903912f), new Vector3(0.45879397349039114f, -0.4587939734903912f, -0.4587939734903912f), new Vector3(0.45879397349039114f, 0.45879397349039114f, 0.45879397349039114f), new Vector3(0.45879397349039114f, -0.4587939734903912f, 0.45879397349039114f), new Vector3(0.45879397349039114f, 0.45879397349039114f, -0.4587939734903912f), new Vector3(-0.7423442429410712f, -0.28355026945067996f, 0f), new Vector3(-0.7423442429410712f, 0.28355026945067996f, 0f), new Vector3(0.742344242941071f, 0.28355026945067996f, 0f), new Vector3(0.742344242941071f, -0.28355026945067996f, 0f), new Vector3(-0.28355026945067996f, 0f, -0.7423442429410712f), new Vector3(-0.28355026945067996f, 0f, 0.7423442429410713f), new Vector3(0.28355026945067996f, 0f, 0.7423442429410713f), new Vector3(0.28355026945067996f, 0f, -0.7423442429410712f)};
    public static Vector3[] Axes={new Vector3(-0.7011660745664998f, -0.129353282733804f, 0.7011660745664997f), new Vector3(0.17850776156415388f, -0.967610437171236f, -0.1785077615641539f), new Vector3(0.7011660745664997f, 0.129353282733804f, 0.7011660745664997f), new Vector3(-0.1785077615641539f, 0.967610437171236f, -0.1785077615641539f), new Vector3(-0.18968891176618907f, -0.27551029871528887f, 0.9423970458648367f), new Vector3(0.38882530909340995f, -0.8842963045612028f, -0.25852451479079264f), new Vector3(0.18968891176618907f, 0.27551029871528887f, 0.9423970458648367f), new Vector3(-0.38882530909340995f, 0.8842963045612028f, -0.25852451479079264f), new Vector3(-0.9002613029620533f, -0.3958451229339379f, 0.18120768482181326f), new Vector3(0.5173574880726357f, -0.35620076405202444f, -0.7781145450535715f), new Vector3(0.9002613029620533f, 0.3958451229339379f, 0.18120768482181326f), new Vector3(-0.5173574880726357f, 0.35620076405202444f, -0.7781145450535715f), new Vector3(0.8236571160905155f, -0.5670881369888239f, 0f), new Vector3(0f, 0f, 1f), new Vector3(-0.40457128008121684f, -0.5876123869812331f, -0.700738012383846f), new Vector3(0.7438355887604035f, 0.5121309948225667f, -0.42945367740364265f), new Vector3(0.129353282733804f, -0.7011660745664997f, 0.7011660745664998f), new Vector3(-0.12935328273380398f, 0.7011660745664997f, 0.7011660745664997f), new Vector3(0.26626442709775217f, -0.6055589577428532f, -0.7499343995041801f), new Vector3(-0.4721894467940722f, -0.6858231950679057f, 0.5537902774906376f)};
    public static float[] Angles={165.25905848199864f, 91.88616366096159f, 165.25905848199864f, 91.88616366096159f, 134.9844825892755f, 159.81476509489744f, 134.9844825892755f, 159.81476509489744f, 150.50923099225025f, 58.940212151671034f, 150.50923099225025f, 58.940212151671034f, 180f, 69.0948425521107f, 88.99059282741865f, 147.0563196031679f, 165.25905848199864f, 165.25905848199864f, 125.38266963381925f, 152.6294221633374f};
    public static int[,] Adjacency=new int[,]{{3, 5, 9}, {4, 6, 10}, {1, 7, 11}, {2, 8, 12}, {1, 13, 17}, {2, 14, 18}, {3, 13, 18}, {4, 14, 17}, {1, 16, 20}, {2, 15, 19}, {3, 16, 19}, {4, 15, 20}, {5, 7, 14}, {6, 8, 13}, {10, 12, 16}, {9, 11, 15}, {5, 8, 20}, {6, 7, 19}, {10, 11, 18}, {9, 12, 17}};

    //Distance from center of planet -> bottom of tile. 
    //The tile heights are 0.4 so the player can be placed 0.4 above that.
    public float size = 1.64728f;
    
    //prefab list should be populated by potential tiles
    public GameObject[] prefabList;

    private bool exploded = false;
    //privately managed tile GameObjects
    private GameObject[] tiles;

    //rumble 0 means don't rumble, rumble 1 means max rumble.
    //This probably should be done with a tweening library or a coroutine,
    //but this works.
    private bool rumbling = false;
    private float rumble = 0.0f;
    private float rumbleTime = 0.0f;
    private float rumbleElapsed = 0.0f;
    public void triggerRumble(float rumbleBuildup)
    {
        rumbling = true;
        rumble = 0.0f;
        rumbleTime = rumbleBuildup; //counter (constant)
        rumbleElapsed = 0.0f; //time passed in the rumble anim
    }
    public float rumbleTween(float t)
    {
        if (t < 0.3f) 
            return 0.0f;
        else if (t > 1.0f)
            return 1.0f;
        //quadratic function that's 0 when t=0.3, 1 when t=1.
        return (t - 0.3f) * (t - 0.3f) / (0.7f*0.7f);
    }
    private void updateRumble()
    {
        if (!exploded && rumbling)
        {
            //advance time and add random positions to transformations.
            rumbleElapsed += Time.deltaTime;
            rumble = rumbleTween(rumbleElapsed / rumbleTime);
            if (rumble > 0.0f)
            {
                float sc = rumble * 0.1f; //scale for position rumbling
                for (var i = 0; i < 20; i++)
                {
                    if (tiles[i])
                    {
                        tiles[i].transform.localPosition = (Positions[i]) * size + new Vector3(Random.Range(-sc, sc), Random.Range(-sc, sc), Random.Range(-sc, sc));
                        tiles[i].transform.rotation = Quaternion.AngleAxis(Angles[i], Axes[i]);
                    }
                }
            }
            if(rumbleElapsed/rumbleTime > 1.0f)
            {
                explode();
            }
        }
    }

    private void destroyTiles()
    {
        for(var i = 0; i < tiles.Length; i++)
        {
            if (tiles[i])
            {
                Destroy(tiles[i]);
            }
        }
    }
    private void instantiateTiles()
    {
        tiles = new GameObject[20];
        for (var i = 0; i < 20; i++)
        {
            //Instantiate random tiles and rotate them into place.
            GameObject obj = Instantiate(prefabList[Random.Range(0, prefabList.Length)], transform);
            obj.transform.localPosition = (Positions[i]) * size;
            obj.transform.rotation = Quaternion.AngleAxis(Angles[i], Axes[i]);
            tiles[i] = obj;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        instantiateTiles();
        rumble = 1.0f;
        exploded = false;
        triggerRumble(25.0f);
    }


    // Update is called once per frame
    void Update()
    {
        updateRumble();
    }

    public void explode()
    {
        foreach (GameObject t in tiles)
        {
            if (t)
            {
                Rigidbody rb = t.AddComponent<Rigidbody>();
                Vector3 dir = (t.transform.position - transform.position).normalized;
                const float sc = 0.5f;
                Vector3 randomPosition = transform.position + new Vector3(Random.Range(-sc, sc), Random.Range(-sc, sc), Random.Range(-sc, sc));
                rb.AddForceAtPosition(dir * 100f, randomPosition);
                rb.useGravity = false;
            }
        }
        exploded = true;
        rumbling = false;
    }
}
