using UnityEngine;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;

    [SerializeField] private LayerMask playerMask;

    private Mesh viewMesh;
    private Transform player;
    private float distance;
    Observer observer;

    public void SetUp()
    {
        player = Player.Instance.transform;
        observer = GetComponentInParent<Observer>();
    }

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        GetComponent<MeshFilter>().mesh = viewMesh;
        GetComponent<MeshRenderer>().enabled = true;
        meshResolution = 4;
    }

    private void Update()
    {
        if (observer == null)
        {
            SetUp();
        }
        if (observer.bIsAlive)
            CheckSeeingPlayer();
    }
    bool bIsOffMesh = false;
    private void LateUpdate()
    {
        if (observer == null)
        {
            SetUp();
        }
        if (observer.bIsAlive)
            DrawFieldOfView();

    }
    public void OffView()
    {
        if (!bIsOffMesh)
        {
            bIsOffMesh = true;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
    private void CheckSeeingPlayer()
    {
        if (!GameController.Instance.IsPlaying) return;
        if (Player.Instance.isHiding) return;
        distance = (player.position - transform.position).magnitude;

        if (distance < viewRadius)
        {
            Vector3 playerPos = new Vector3(player.position.x, 0f, player.position.z);
            Vector3 dir = (playerPos - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dir) < viewAngle / 2)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, player.position - transform.position, out hit, viewRadius, viewCastLayer))
                {
                    if (hit.transform.CompareTag("Player") && Player.Instance.bIsAlive)
                    {
                        //GameController.Instance.LoseLevel();
                        SoundManage.Instance.Play_LevelFail();
                        Player.Instance.Kill(tag);
                        observer.DoAttack();
                    }
                    //if (0 != (playerMask.value & 1 << hit.collider.gameObject.layer))
                    //{
                    //    GameController.Instance.LoseLevel();
                    //    SoundManage.Instance.Play_LevelFail();
                    //    Player.Instance.Kill(tag);
                    //    observer.DoAttack();
                    //}
                }
            }
        }
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
    [SerializeField] LayerMask viewCastLayer;
    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, viewCastLayer))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}