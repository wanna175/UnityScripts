using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer = null;
    private Archer _player = null;

    private void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        _player = this.transform.parent.GetChild(1).GetComponent<Archer>();
    }
    private void Update()
    {
        this.transform.localPosition += (spriteRenderer.flipX) ? Vector3.right*-2000 * Time.deltaTime : Vector3.right*2000 * Time.deltaTime;
    }
    public void SetArrow()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(timer());
    }
    private void OnTriggerEnter(Collider other)
    {
        _player.Return_object(this);
        if (other.transform.CompareTag("Enemy"))
        {
            _player.Return_object(this);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Enemy"))
        {
            _player.Return_object(this);
        }
    }
    private IEnumerator timer()
    {
        yield return new WaitForSeconds(1.0f);
        _player.Return_object(this);
    }
}
