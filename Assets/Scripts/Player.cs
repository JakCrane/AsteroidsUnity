using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    Rigidbody2D _rigidbody;
    private bool _thrusting;
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    private float _turnDirection;
    //called once when the script starts up
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // update is called every frame
    private void Update() {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            _turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            _turnDirection = -1.0f;
        } else {
            _turnDirection = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }
    // fixed update is called relative to time passing so physics isnt fps dependent
    private void FixedUpdate() {
        if (_thrusting) {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }
        if (_turnDirection != 0.0f) {
            _rigidbody.AddTorque(_turnDirection*this.turnSpeed);
        }
    }

    private void Shoot() {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }
}
