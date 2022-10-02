using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcoSpawnwPrefabList : MonoBehaviour
{
    //So stupid
    public static Vector3[] Positions = { new Vector3(0f, -0.7423442429410712f, -0.28355026945067996f), new Vector3(0f, 0.7423442429410713f, 0.28355026945067996f), new Vector3(0f, -0.7423442429410712f, 0.28355026945067996f), new Vector3(0f, 0.7423442429410713f, -0.28355026945067996f), new Vector3(-0.4587939734903912f, -0.4587939734903912f, -0.4587939734903912f), new Vector3(-0.4587939734903912f, 0.45879397349039114f, 0.45879397349039114f), new Vector3(-0.4587939734903912f, -0.4587939734903912f, 0.45879397349039114f), new Vector3(-0.4587939734903912f, 0.45879397349039114f, -0.4587939734903912f), new Vector3(0.45879397349039114f, -0.4587939734903912f, -0.4587939734903912f), new Vector3(0.45879397349039114f, 0.45879397349039114f, 0.45879397349039114f), new Vector3(0.45879397349039114f, -0.4587939734903912f, 0.45879397349039114f), new Vector3(0.45879397349039114f, 0.45879397349039114f, -0.4587939734903912f), new Vector3(-0.7423442429410712f, -0.28355026945067996f, 0f), new Vector3(-0.7423442429410712f, 0.28355026945067996f, 0f), new Vector3(0.742344242941071f, 0.28355026945067996f, 0f), new Vector3(0.742344242941071f, -0.28355026945067996f, 0f), new Vector3(-0.28355026945067996f, 0f, -0.7423442429410712f), new Vector3(-0.28355026945067996f, 0f, 0.7423442429410713f), new Vector3(0.28355026945067996f, 0f, 0.7423442429410713f), new Vector3(0.28355026945067996f, 0f, -0.7423442429410712f) };
    public static Vector3[] axes = { new Vector3(-0.7011660745664997f, -0.12935328273380398f, 0.7011660745664996f), new Vector3(0.17850776156415388f, -0.9676104371712362f, -0.17850776156415393f), new Vector3(0.7011660745664997f, 0.129353282733804f, 0.7011660745664997f), new Vector3(-0.1785077615641539f, 0.967610437171236f, -0.1785077615641539f), new Vector3(-0.18968891176618907f, -0.2755102987152889f, 0.9423970458648369f), new Vector3(0.38882530909341f, -0.8842963045612028f, -0.25852451479079264f), new Vector3(0.18968891176618907f, 0.2755102987152889f, 0.9423970458648369f), new Vector3(-0.38882530909341f, 0.8842963045612028f, -0.25852451479079264f), new Vector3(-0.9002613029620534f, -0.395845122933938f, 0.18120768482181332f), new Vector3(0.5173574880726358f, -0.3562007640520245f, -0.7781145450535715f), new Vector3(0.9002613029620534f, 0.395845122933938f, 0.18120768482181332f), new Vector3(-0.5173574880726358f, 0.3562007640520245f, -0.7781145450535715f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 1f), new Vector3(-0.40457128008121684f, -0.5876123869812331f, -0.700738012383846f), new Vector3(0.7438355887604036f, 0.5121309948225667f, -0.42945367740364276f), new Vector3(0.12935328273380398f, -0.7011660745664996f, 0.7011660745664997f), new Vector3(-0.12935328273380398f, 0.7011660745664997f, 0.7011660745664997f), new Vector3(0.26626442709775217f, -0.6055589577428532f, -0.7499343995041801f), new Vector3(-0.47218944679407215f, -0.6858231950679057f, 0.5537902774906376f) };
    public static float[] angles = { 165.25905848199864f, 91.88616366096159f, 165.25905848199864f, 91.88616366096159f, 134.9844825892755f, 159.81476509489744f, 134.9844825892755f, 159.81476509489744f, 150.50923099225025f, 58.94021215167102f, 150.50923099225025f, 58.940212151671034f, 0f, 69.0948425521107f, 88.99059282741865f, 147.0563196031679f, 165.25905848199864f, 165.2590584819987f, 125.38266963381925f, 152.6294221633374f };

    public static int[,] Adjacency = new int[,] { { 3, 5, 9 }, { 4, 6, 10 }, { 1, 7, 11 }, { 2, 8, 12 }, { 1, 13, 17 }, { 2, 14, 18 }, { 3, 13, 18 }, { 4, 14, 17 }, { 1, 16, 20 }, { 2, 15, 19 }, { 3, 16, 19 }, { 4, 15, 20 }, { 5, 7, 14 }, { 6, 8, 13 }, { 10, 12, 16 }, { 9, 11, 15 }, { 5, 8, 20 }, { 6, 7, 19 }, { 10, 11, 18 }, { 9, 12, 17 } };
    public float size = 2;

    public Vector3 offset = new Vector3(0, -10, 0);

    //https://answers.unity.com/questions/11363/converting-matrix4x4-to-quaternion-vector3.html
    public static Quaternion QuaternionFromMatrix(Matrix4x4 m) { return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1)); }
    public GameObject[] prefab2 = new GameObject[20];

    public Rocket rocketPrefab;

    public GameObject[] tiles = new GameObject[20];
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < 20; i++)
        {
            // Manual offset given because the slices are spawned off center for some reason
            int random = Random.Range(0, 3);
            GameObject obj = Instantiate(prefab2[i], transform);
            obj.transform.localPosition = (Positions[i] + offset) * size;
            obj.transform.rotation = Quaternion.AngleAxis(angles[i], axes[i]);

            tiles[i] = obj;
        }
        
        // Spawn rocket
        Rocket rok = Instantiate(rocketPrefab, tiles[0].transform);
        rok.transform.localPosition = Vector3.up * 0.5f;
        rok.transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
