using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solution
{
    public class OOPMapGenerator : MonoBehaviour
    {
        [Header("Set MapGenerator")]
        public int X;
        public int Y;

        [Header("Set Player")]
        public OOPPlayer player;
        public Vector2Int playerStartPos;

        [Header("Set NPC")]
        public NPC Npc;
        public NPCSkill NpcSkill;

        [Header("Set Exit")]
        public OOPExit Exit;

        [Header("Set Wall")]
        public Identity Wall;

        [Header("Set Prefab")]
        public GameObject[] floorsPrefab;
        public GameObject[] wallsPrefab;
        public GameObject[] demonWallsPrefab;

        // Prefab สำหรับวัตถุโต้ตอบ 5 ชนิด
        public GameObject[] statuesPrefab;
        public GameObject[] boxesPrefab;
        public GameObject[] fermentersPrefab;
        public GameObject[] chestsPrefab;
        public GameObject[] coffinsPrefab;

        [Header("Set Transform")]
        public Transform floorParent;
        public Transform wallParent;
        public Transform itemParent;

        [Header("Set object Count")]
        public int obsatcleCount;

        public Identity[,] mapdata;

        [HideInInspector] public string empty = "";
        [HideInInspector] public string demonWall = "demonWall";
        [HideInInspector] public string exit = "exit";
        [HideInInspector] public string playerOnMap = "player";
        [HideInInspector] public string npc = "Npc";

        private void Awake()
        {
            // --- สุ่มที่ซ่อนกุญแจ ---
            List<string> possibleLocations = new List<string>
            {
                "Statue", "Box", "Fermenter", "Chest", "Coffin"
            };

            int index1 = Random.Range(0, possibleLocations.Count);
            GameState.KeyPart1_Location = possibleLocations[index1];
            possibleLocations.RemoveAt(index1);

            int index2 = Random.Range(0, possibleLocations.Count);
            GameState.KeyPart2_Location = possibleLocations[index2];

            GameState.KeyPart1_Found = false;
            GameState.KeyPart2_Found = false;

            CreateMap();
        }

        void Start()
        {
            StartCoroutine(SetUPMap());
        }

        IEnumerator SetUPMap()
        {
            SetUpPlayer();
            SetUpExit();
            SetUpNpc();

            PlaceItemsOnMap(obsatcleCount, demonWallsPrefab, wallParent, demonWall);

            PlaceItemsOnMap(1, statuesPrefab, itemParent, "Statue");
            PlaceItemsOnMap(1, boxesPrefab, itemParent, "Box");
            PlaceItemsOnMap(1, fermentersPrefab, itemParent, "Fermenter");
            PlaceItemsOnMap(1, chestsPrefab, itemParent, "Chest");
            PlaceItemsOnMap(1, coffinsPrefab, itemParent, "Coffin");

            yield return null;
        }

        private void PlaceItemsOnMap(int count, GameObject[] prefab, Transform parent, string itemType, System.Action onComplete = null)
        {
            int placedCount = 0;
            int preventInfiniteLoop = 1000;

            while (placedCount < count)
            {
                if (--preventInfiniteLoop < 0)
                {
                    Debug.LogWarning($"Could not place all items ({itemType}). Map may be too full.");
                    break;
                }

                int x = UnityEngine.Random.Range(0, X);
                int y = UnityEngine.Random.Range(0, Y);

                if (mapdata[x, y] == null)
                {
                    SetUpItem(x, y, prefab, parent, itemType);
                    placedCount++;
                }
            }
            onComplete?.Invoke();
        }

        private void SetUpNpc()
        {
            if (Npc != null)
            {
                Npc.mapGenerator = this;
                mapdata[Npc.positionX, Npc.positionY] = Npc;
                Npc.transform.position = new Vector3(Npc.positionX, Npc.positionY, 0);
            }
            if (NpcSkill != null)
            {
                NpcSkill.mapGenerator = this;
                mapdata[NpcSkill.positionX, NpcSkill.positionY] = NpcSkill;
                NpcSkill.transform.position = new Vector3(NpcSkill.positionX, NpcSkill.positionY, 0);
            }
        }

        private void SetUpExit()
        {
            if (Exit == null) return;
            mapdata[X - 1, Y - 1] = Exit;
            Exit.transform.position = new Vector3(X - 1, Y - 1, 0);
        }

        private void SetUpPlayer()
        {
            if (player == null) return;
            player.mapGenerator = this;
            player.positionX = playerStartPos.x;
            player.positionY = playerStartPos.y;
            player.transform.position = new Vector3(playerStartPos.x, playerStartPos.y, -0.1f);
            mapdata[playerStartPos.x, playerStartPos.y] = player;
        }

        private void CreateMap()
        {
            mapdata = new Identity[X, Y];
            for (int x = -1; x < X + 1; x++)
            {
                for (int y = -1; y < Y + 1; y++)
                {
                    if (x == -1 || x == X || y == -1 || y == Y)
                    {
                        if (wallsPrefab.Length == 0) continue;
                        int r = Random.Range(0, wallsPrefab.Length);
                        GameObject obj = Instantiate(wallsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
                        obj.transform.parent = wallParent;
                        obj.name = "Wall_" + x + ", " + y;
                    }
                    else
                    {
                        if (floorsPrefab.Length == 0) continue;
                        int r = Random.Range(0, floorsPrefab.Length);
                        GameObject obj = Instantiate(floorsPrefab[r], new Vector3(x, y, 1), Quaternion.identity);
                        obj.transform.parent = floorParent;
                        obj.name = "floor_" + x + ", " + y;
                        mapdata[x, y] = null;
                    }
                }
            }
        }

        public void SetUpItem(int x, int y, GameObject[] _itemsPrefab, Transform parrent, string _name)
        {
            if (_itemsPrefab.Length == 0) return;
            int r = Random.Range(0, _itemsPrefab.Length);

            // ************************************************************
            // 🌟 1. ส่วนที่เพิ่ม: ตั้งค่าตำแหน่ง (Z และ Y Offset) 🌟
            // ************************************************************
            float zPos = -0.05f; // ให้อยู่หน้ากำแพง
            float yOffset = 0f;

            // ถ้าเป็นวัตถุโต้ตอบ ให้ขยับลงมานิดนึงจะได้ดูไม่ลอย
            if (_name == "Statue" || _name == "Box" || _name == "Coffin" ||
                _name == "Chest" || _name == "Fermenter")
            {
                yOffset = -0.5f;
            }

            // ใช้ yOffset ที่คำนวณมาในการสร้าง
            GameObject obj = Instantiate(_itemsPrefab[r], new Vector3(x, y + yOffset, zPos), Quaternion.identity);
            // ************************************************************

            obj.transform.parent = parrent;

            // ************************************************************
            // 🌟 2. ส่วนที่เพิ่ม: ตั้งค่า Sorting Order 🌟
            // ************************************************************
            if (_name == "Statue" || _name == "Box" || _name == "Fermenter" || _name == "Coffin" || _name == "Chest")
            {
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = 1; // วาดทับพื้นและกำแพง
                }
            }
            // ************************************************************

            Identity identityComponent = obj.GetComponent<Identity>();
            if (identityComponent == null)
            {
                Debug.LogError($"Prefab for {_name} at {x},{y} is missing the Identity component!");
                Destroy(obj);
                return;
            }

            mapdata[x, y] = identityComponent;
            mapdata[x, y].positionX = x;
            mapdata[x, y].positionY = y;
            mapdata[x, y].mapGenerator = this;

            mapdata[x, y].Name = _name;

            obj.name = $"Object_{mapdata[x, y].Name} {x}, {y}";
        }

        public bool HasPlacement(int x, int y)
        {
            if (x >= 0 && x < X && y >= 0 && y < Y)
            {
                return true;
            }
            return false;
        }

        public Identity GetMapData(int x, int y)
        {
            if (x >= 0 && x < X && y >= 0 && y < Y)
            {
                return mapdata[x, y];
            }
            return Wall;
        }
    }
}