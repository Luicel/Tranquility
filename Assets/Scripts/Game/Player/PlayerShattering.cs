using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShattering : MonoBehaviour {
    [SerializeField] private GameObject shatterEffect;
    [SerializeField] private GameObject shieldObject;

    public static GameObject checkpoint;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Damager"))
            Shatter();
    }

    // public for external classes to call (quite ugly, I know)
    public void Shatter() {
        shatterEffect.transform.position = transform.position;
        shatterEffect.GetComponent<ParticleSystem>().Play();

        SetVisibility(false);
        // really poor way to solve an annoying bug - may refactor later
        shieldObject.SetActive(false);

        StartCoroutine("TeleportToCheckpoint");
    }

    private IEnumerator TeleportToCheckpoint() {
        yield return new WaitForSeconds(1.5f);
        transform.position = checkpoint.transform.position;
        SetVisibility(true);
    }

    private void SetVisibility(bool visibility) {
        // This looks disgusting, but I genuienly think that it's the best solution (given that I forgot to rely on states early on)
        transform.GetChild(0).gameObject.SetActive(visibility);
        transform.GetComponent<Rigidbody2D>().simulated = visibility;
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.GetComponent<SpriteRenderer>().enabled = visibility;
        transform.GetComponent<PlayerController>().enabled = visibility;
        transform.GetComponent<PlayerShield>().enabled = visibility;
        transform.GetComponent<PlayerAnimations>().enabled = visibility;
    }
}
