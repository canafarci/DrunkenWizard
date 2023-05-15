using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DieRaycaster : MonoBehaviour
{
    [SerializeField] Transform[] _raycastOrigins, _faces;
    [SerializeField] LayerMask _layer;
    SpellHandler _spellHandler;
    private void Awake()
    {
        _spellHandler = FindObjectOfType<SpellHandler>();
    }

    private void OnEnable()
    {
        FindObjectOfType<CastSpell>().CastSpellHandler += OnCastSpell;
    }
    private void OnDisable()
    {
        CastSpell cast = FindObjectOfType<CastSpell>();
        if (cast != null)
            cast.CastSpellHandler -= OnCastSpell;
    }
    private void OnCastSpell(int index)
    {
        PickFace(index);
    }
    public void PickFace(int index)
    {
        List<float> faceDistList = new List<float>();
        foreach (Transform f in _faces)
        {
            faceDistList.Add(Vector3.Distance(f.position, _raycastOrigins[index].position));
        }

        Transform face = _faces[faceDistList.IndexOf(faceDistList.Min())];

        Ray ray = new Ray(_raycastOrigins[index].position, Vector3.down);
        _spellHandler.OnCastSpell(face.GetComponent<DieFace>().FaceId);
    }
}
